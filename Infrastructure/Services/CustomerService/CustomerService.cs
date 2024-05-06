using System.Net;
using AutoMapper;
using Domain.DTOs.CustomerDTO;
using Domain.Entities;
using Domain.Filters;
using Domain.Responses;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Services.CustomerService;

public class CustomerService(DataContext context, IMapper mapper):ICustomerService
{
    public async Task<Response<string>> CreateCustomerAsync(CreateCustomerDto createCustomerDto)
    {
        try
        {
            var existing = await context.Customers.AnyAsync(e => e.Name == createCustomerDto.Name);
            if (existing) return new Response<string>(HttpStatusCode.BadRequest, "Customer already exists");
            var newCustomer = mapper.Map<Customer>(createCustomerDto);
            await context.Customers.AddAsync(newCustomer);
            await context.SaveChangesAsync();
            return new Response<string>("Successfully Added!");
        }
        catch (Exception ex)
        {
            return new Response<string>(HttpStatusCode.InternalServerError, ex.Message);
        }
    }

    public async Task<Response<bool>> DeleteCustomerAsync(int id)
    {
        try
        {
            var existing = await context.Customers.Where(e => e.Id == id).ExecuteDeleteAsync();
            if (existing == 0) return new Response<bool>(HttpStatusCode.BadRequest, "Customer not found");
            return new Response<bool>(true);
        }
        catch (Exception ex)
        {
            return new Response<bool>(HttpStatusCode.InternalServerError, ex.Message);
        }
    }

    public async Task<PagedResponse<List<GetCustomersDto>>> GetCustomersAsync(CustomerFilter filter)
    {
        try
        {
            var customers = context.Customers.AsQueryable();
            if (!string.IsNullOrEmpty(filter.Name))
            customers = customers.Where(e => e.Name.ToLower().Contains(filter.Name.ToLower()));
            if (!string.IsNullOrEmpty(filter.Email))
            customers = customers.Where(e => e.Email.ToLower().Contains(filter.Email.ToLower()));
            var result = await customers.Skip((filter.PageNumber - 1) * filter.PageSize).Take(filter.PageSize).ToListAsync();
            var total = await customers.CountAsync();

            var response = mapper.Map<List<GetCustomersDto>>(result);
            return new PagedResponse<List<GetCustomersDto>>(response, total, filter.PageNumber, filter.PageSize);
        }
        catch (Exception ex)
        {
            return new PagedResponse<List<GetCustomersDto>>(HttpStatusCode.InternalServerError, ex.Message);
        }
    }
    public async Task<Response<string>> UpdateCustomerAsync(UpdateCustomerDto updateCustomerDto)
    {
        try
        {
            var existing = await context.Customers.AnyAsync(e => e.Id == updateCustomerDto.Id);
            if (!existing) return new Response<string>(HttpStatusCode.BadRequest, "Customer not found");
            var mapped = mapper.Map<Customer>(updateCustomerDto);
            context.Customers.Update(mapped);
            await context.SaveChangesAsync();
            return new Response<string>("Updated successfully!");
        }
        catch (Exception ex)
        {
            return new Response<string>(HttpStatusCode.InternalServerError, ex.Message);
        }
    }
}
