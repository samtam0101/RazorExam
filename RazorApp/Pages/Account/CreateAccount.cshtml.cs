using Domain.DTOs.AccountDTO;
using Infrastructure.Services.AccountService;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace RazorApp.Pages.Account
{
    [IgnoreAntiforgeryToken]
    public class CreateAccountModel : PageModel
    {
        private readonly IAccountService _accountService;
        public CreateAccountModel(IAccountService accountService)
        {
            _accountService = accountService;
        }
        [BindProperty] public CreateAccountDto CreateAccountDto { get; set; }
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }
            await _accountService.CreateAccountAsync(CreateAccountDto);
            return RedirectToPage("/Account/GetAccounts");
        }
    }
}