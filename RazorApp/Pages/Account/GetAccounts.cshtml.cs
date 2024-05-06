using System.Net;
using Domain.DTOs.AccountDTO;
using Domain.Filters;
using Infrastructure.Services.AccountService;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace RazorApp.Pages.Account;
[IgnoreAntiforgeryToken]
public class GetAccountsModel : PageModel
{
    private readonly IAccountService _accountService;
    public GetAccountsModel(IAccountService accountService)
    {
        _accountService = accountService;
    }
    [BindProperty(SupportsGet = true)]
    public AccountFilter Filter { get; set; }
    public List<GetAccountsDto> Accounts { get; set; }
    public int TotalPages { get; set; }


    public async Task<IActionResult> OnGetAsync()
    {
        try
        {
            var response = await _accountService.GetAccountsAsync(Filter);
            Accounts = response.Data;
            TotalPages = response.TotalPage;
            return Page();
        }
        catch (Exception ex)
        {
            return new StatusCodeResult((int)HttpStatusCode.InternalServerError);
        }
    }
}
