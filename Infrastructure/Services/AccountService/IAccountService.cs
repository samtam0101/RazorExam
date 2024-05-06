using Domain.DTOs.AccountDTO;
using Domain.Filters;
using Domain.Responses;

namespace Infrastructure.Services.AccountService;

public interface IAccountService
{
    Task<Response<string>> CreateAccountAsync(CreateAccountDto createAccountDto);
    Task<Response<string>> UpdateAccountAsync(UpdateAccountDto updateAccountDto);
    Task<PagedResponse<List<GetAccountsDto>>> GetAccountsAsync(AccountFilter filter);
    Task<Response<bool>> DeleteAccountAsync(int id);
}
