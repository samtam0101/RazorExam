using Infrastructure.Services.AccountService;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace RazorApp.Pages.Account
{
    public class DeleteAccountModel : PageModel
    {
        private readonly IAccountService _accountService;

        public DeleteAccountModel(IAccountService accountService)
        {
            _accountService = accountService;
        }

        [BindProperty(SupportsGet = true)]
        public int Id { get; set; }

        public async Task<IActionResult> OnPostAsync()
        {
            await _accountService.DeleteAccountAsync(Id);
            return RedirectToPage("/Account/GetAccounts");
        }
    }
}
