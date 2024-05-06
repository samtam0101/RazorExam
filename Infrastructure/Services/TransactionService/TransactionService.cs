using System.Net;
using System.Transactions;
using AutoMapper;
using Domain.DTOs.TransactionDTO;
using Domain.Filters;
using Domain.Responses;
using Domain.Entities;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Services.TransactionService;

public class TransactionService(DataContext context, IMapper mapper):ITransactionService
{
    public async Task<Response<string>> CreateTransactionAsync(CreateTransactionDto createTransactionDto)
    {
        try
        {
            var existing = await context.Transactions.AnyAsync(e => e.TransactionDate == createTransactionDto.TransactionDate);
            if (existing) return new Response<string>(HttpStatusCode.BadRequest, "Transaction in this date already exists");
            var newTransaction = mapper.Map<Domain.Entities.Transaction>(createTransactionDto);
            await context.Transactions.AddAsync(newTransaction);
            await context.SaveChangesAsync();
            return new Response<string>("Successfully Added!");
        }
        catch (Exception ex)
        {
            return new Response<string>(HttpStatusCode.InternalServerError, ex.Message);
        }
    }

    public async Task<Response<bool>> DeleteTransactionAsync(int id)
    {
        try
        {
            var existing = await context.Transactions.Where(e => e.Id == id).ExecuteDeleteAsync();
            if (existing == 0) return new Response<bool>(HttpStatusCode.BadRequest, "Transaction was not founded");
            return new Response<bool>(true);
        }
        catch (Exception ex)
        {
            return new Response<bool>(HttpStatusCode.InternalServerError, ex.Message);
        }
    }

    public async Task<PagedResponse<List<GetTransactionsDto>>> GetTransactionsAsync(PaginationFilter filter)
    {
        try
        {
            var transactions = context.Transactions.AsQueryable();
            var result = await transactions.Skip((filter.PageNumber - 1) * filter.PageSize).Take(filter.PageSize).ToListAsync();
            var total = await transactions.CountAsync();

            var response = mapper.Map<List<GetTransactionsDto>>(result);
            return new PagedResponse<List<GetTransactionsDto>>(response, total, filter.PageNumber, filter.PageSize);
        }
        catch (Exception ex)
        {
            return new PagedResponse<List<GetTransactionsDto>>(HttpStatusCode.InternalServerError, ex.Message);
        }
    }

    public async Task<Response<string>> UpdateTransactionAsync(UpdateTransactionDto updateTransactionDto)
    {
        try
        {
            var existing = await context.Transactions.AnyAsync(e => e.Id == updateTransactionDto.Id);
            if (!existing) return new Response<string>(HttpStatusCode.BadRequest, "Transaction not found");
            var mapped = mapper.Map<Domain.Entities.Transaction>(updateTransactionDto);
            context.Transactions.Update(mapped);
            await context.SaveChangesAsync();
            return new Response<string>("Updated successfully!");
        }
        catch (Exception ex)
        {
            return new Response<string>(HttpStatusCode.InternalServerError, ex.Message);
        }
    }
}
