using Domain.DTOs.CustomerDTO;
using Domain.Filters;
using Domain.Responses;

namespace Infrastructure.Services.CustomerService;

public interface ICustomerService
{
    Task<Response<string>> CreateCustomerAsync(CreateCustomerDto createCustomerDto);
    Task<Response<string>> UpdateCustomerAsync(UpdateCustomerDto updateCustomerDto);
    Task<PagedResponse<List<GetCustomersDto>>> GetCustomersAsync(CustomerFilter filter);
    Task<Response<bool>> DeleteCustomerAsync(int id);
}
