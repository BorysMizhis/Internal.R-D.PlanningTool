using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PlanningTool.DBInterface;
using PlanningTool.Models;

namespace PlanningTool.Pages;

[BindProperties]
public class IndexModel : PageModel
{
    public string Message = "NoMessage";
    public List<Employee> employees;
    public List<Chapter> chapters;
    public List<Feature> features;
    public DateTime date;
    private readonly ILogger<IndexModel> _logger;

    public IndexModel(ILogger<IndexModel> logger)
    {
        _logger = logger;
    }

    public async Task OnGet()
    {
        date = DateTime.Now;
        await GetData();
    }    

    public async Task GetData(){
        EmployeeDb employeDb = new EmployeeDb();        
        employees = await employeDb.GetAll();

        ChapterDb chapterDb = new ChapterDb();        
        chapters = await chapterDb.GetAll();

        FeatureDb featureDb = new FeatureDb();        
        features = await featureDb.GetAll();
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

    
    public async Task<IActionResult> OnPostAddChapter()
    {
        Chapter c = new Chapter(){Name = "Some Chapter"};
        ChapterDb cdb = new ChapterDb();
        await cdb.Add(c);
        Message = "Chapter Added";

        await GetData();
        return Page();
    }

    public async Task<IActionResult> OnPostAddFeature()
    {
        Feature f = new Feature(){Name = "Some Feature"};
        FeatureDb fdb = new FeatureDb();
        await fdb.Add(f);
        Message = "Chapter Added";

        await GetData();
        return Page();
    }
}
