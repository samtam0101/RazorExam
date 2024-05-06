using System.Net;
using Domain.DTOs.CustomerDTO;
using Domain.Filters;
using Infrastructure.Services.CustomerService;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace RazorApp.Pages.Customer;
[IgnoreAntiforgeryToken]
public class GetCustomersModel : PageModel
{
    private readonly ICustomerService _customerService;
    public GetCustomersModel(ICustomerService customerService)
    {
        _customerService = customerService;
    }
    [BindProperty(SupportsGet = true)]
    public CustomerFilter Filter { get; set; }
    public List<GetCustomersDto> Customers { get; set; }
    public int TotalPages { get; set; }


    public async Task<IActionResult> OnGetAsync()
    {
        try
        {
            var response = await _customerService.GetCustomersAsync(Filter);
            Customers = response.Data;
            TotalPages = response.TotalPage;
            return Page();
        }
        catch (Exception ex)
        {
            return new StatusCodeResult((int)HttpStatusCode.InternalServerError);
        }
    }
}
