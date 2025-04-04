using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PlanningTool.Models;

namespace PlanningTool.Pages;

[BindProperties]
public class IndexModel : PageModel
{
    public string Message = "NoMessage";
    public List<Employee> employees;

    private readonly ILogger<IndexModel> _logger;

    public IndexModel(ILogger<IndexModel> logger)
    {
        _logger = logger;
    }

    public async Task OnGet()
    {
        await GetData();
    }    

    public async Task GetData(){
        EmployeeDb employeDb = new EmployeeDb();        
        employees = await employeDb.GetAll();
    }

    public async Task<IActionResult> OnPostTimeButton()
    {
        Message = $"{DateTime.Now:HH\\:mm\\:ss.fff} button click";
        await GetData();
        return Page();
    }

    public async Task<IActionResult> OnPostAddEmployee()
    {
        Employee e = new Employee(){Name = "Someone", DefaultCapacity = 2};
        EmployeeDb edb = new EmployeeDb();
        await edb.Add(e);
        Message = "Employee Added";

        await GetData();
        return Page();
    }
}
