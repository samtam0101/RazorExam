using System.Net;
using AutoMapper;
using Domain.DTOs.AccountDTO;
using Domain.Entities;
using Domain.Filters;
using Domain.Responses;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Services.AccountService;

public class AccountService(DataContext context, IMapper mapper) : IAccountService
{
    public async Task<Response<string>> CreateAccountAsync(CreateAccountDto createAccountDto)
    {

        try
        {
            var existing = await context.Accounts.AnyAsync(e => e.AccountNumber == createAccountDto.AccountNumber);
            if (existing) return new Response<string>(HttpStatusCode.BadRequest, "Account Number already exists");
            var newAccount = mapper.Map<Account>(createAccountDto);
            await context.Accounts.AddAsync(newAccount);
            await context.SaveChangesAsync();
            return new Response<string>("Successfully Added!");
        }
        catch (Exception ex)
        {
            return new Response<string>(HttpStatusCode.InternalServerError, ex.Message);
        }
    }
    public async Task<Response<bool>> DeleteAccountAsync(int id)
    {
        try
        {
            var existing = await context.Accounts.Where(e => e.Id == id).ExecuteDeleteAsync();
            if (existing == 0) return new Response<bool>(HttpStatusCode.BadRequest, "Account was not founded");
            return new Response<bool>(true);
        }
        catch (Exception ex)
        {
            return new Response<bool>(HttpStatusCode.InternalServerError, ex.Message);
        }
    }

    public async Task<PagedResponse<List<GetAccountsDto>>> GetAccountsAsync(AccountFilter filter)
    {
        try
        {
            var accounts = context.Accounts.AsQueryable();
            if (!string.IsNullOrEmpty(filter.AccountNumber))
                accounts = accounts.Where(e => e.AccountNumber.ToLower().Contains(filter.AccountNumber.ToLower()));
            var result = await accounts.Skip((filter.PageNumber - 1) * filter.PageSize).Take(filter.PageSize).ToListAsync();
            var total = await accounts.CountAsync();

            var response = mapper.Map<List<GetAccountsDto>>(result);
            return new PagedResponse<List<GetAccountsDto>>(response, total, filter.PageNumber, filter.PageSize);
        }
        catch (Exception ex)
        {
            return new PagedResponse<List<GetAccountsDto>>(HttpStatusCode.InternalServerError, ex.Message);
        }
    }

    public async Task<Response<string>> UpdateAccountAsync(UpdateAccountDto updateAccountDto)
    {
        try
        {
            var existing = await context.Accounts.AnyAsync(e => e.Id == updateAccountDto.Id);
            if (!existing) return new Response<string>(HttpStatusCode.BadRequest, "Account not found");
            var mapped = mapper.Map<Account>(updateAccountDto);
            context.Accounts.Update(mapped);
            await context.SaveChangesAsync();
            return new Response<string>("Updated successfully!");
        }
        catch (Exception ex)
        {
            return new Response<string>(HttpStatusCode.InternalServerError, ex.Message);
        }
    }
}
