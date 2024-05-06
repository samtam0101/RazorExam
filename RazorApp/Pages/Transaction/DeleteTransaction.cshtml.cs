using Infrastructure.Services.TransactionService;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace RazorApp.Pages.Transaction
{
    public class DeleteTransactionModel : PageModel
    {
        private readonly ITransactionService _transactionService;

        public DeleteTransactionModel(ITransactionService transactionService)
        {
            _transactionService = transactionService;
        }

        [BindProperty(SupportsGet = true)]
        public int Id { get; set; }

        public async Task<IActionResult> OnPostAsync()
        {
            await _transactionService.DeleteTransactionAsync(Id);
            return RedirectToPage("/Transaction/GetTransactions");
        }
    }
}
