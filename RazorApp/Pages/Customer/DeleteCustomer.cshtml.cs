using Infrastructure.Services.CustomerService;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace RazorApp.Pages.Customer
{
    public class DeleteCustomerModel : PageModel
    {
        private readonly ICustomerService _customerService;

        public DeleteCustomerModel(ICustomerService customerService)
        {
            _customerService = customerService;
        }

        [BindProperty(SupportsGet = true)]
        public int Id { get; set; }

        public async Task<IActionResult> OnPostAsync()
        {
            await _customerService.DeleteCustomerAsync(Id);
            return RedirectToPage("/Customer/GetCustomers");
        }
    }
}
