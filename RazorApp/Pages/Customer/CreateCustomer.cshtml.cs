using Domain.DTOs.CustomerDTO;
using Infrastructure.Services.CustomerService;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace RazorApp.Pages.Customer
{
    [IgnoreAntiforgeryToken]
    public class CreateCustomerModel : PageModel
    {
        private readonly ICustomerService _customerService;
        public CreateCustomerModel(ICustomerService customerService)
        {
            _customerService = customerService;
        }
        [BindProperty] public CreateCustomerDto CreateCustomerDto { get; set; }
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }
            await _customerService.CreateCustomerAsync(CreateCustomerDto);
            return RedirectToPage("/Customer/GetCustomers");
        }
    }
}