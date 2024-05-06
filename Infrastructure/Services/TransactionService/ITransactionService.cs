using Domain.DTOs.TransactionDTO;
using Domain.Filters;
using Domain.Responses;

namespace Infrastructure.Services.TransactionService;

public interface ITransactionService
{
    Task<Response<string>> CreateTransactionAsync(CreateTransactionDto createTransactionsDto);
    Task<Response<string>> UpdateTransactionAsync(UpdateTransactionDto updateTransactionsDto);
    Task<PagedResponse<List<GetTransactionsDto>>> GetTransactionsAsync(PaginationFilter filter);
    Task<Response<bool>> DeleteTransactionAsync(int id);
}
