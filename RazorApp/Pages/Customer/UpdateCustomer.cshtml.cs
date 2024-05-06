using Domain.DTOs.CustomerDTO;
using Infrastructure.Services.CustomerService;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace RazorApp.Pages.Customer
{
    public class UpdateCustomerModel : PageModel
    {
        private readonly ICustomerService _customerService;

        public UpdateCustomerModel(ICustomerService customerService)
        {
            _customerService = customerService;
        }

        [BindProperty]
        public UpdateCustomerDto Customer { get; set; }

        public async Task<IActionResult> OnPostAsync(int id)
        {
            
            Customer.Id = id; 
            await _customerService.UpdateCustomerAsync(Customer);

            return RedirectToPage("/Customer/GetCustomers");
        }
    }
}
