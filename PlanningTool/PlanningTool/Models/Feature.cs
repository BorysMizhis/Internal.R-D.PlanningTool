namespace PlanningTool.Models;

public class Feature
{
    public int Id { set; get; } = 0;
    public int VersionId { set; get; } = 0;
    public String Name { set; get; } = string.Empty;
    public int Estimation { set; get; } = 0;
    public int State { set; get; } = 0;
    public List<Chapter>? Chapters { set; get; }
    public List<Employee>? Employees { set; get; }
    public List<Planning> Plannings { set; get; } = new List<Planning>();

    public int? GetPlanningValue(int employeeId, int month, int year) {
        if(employeeId==2) {
            System.Console.WriteLine("Found");
        }
        if(Plannings.Any(x => x.EmployeeId == employeeId && x.Month == month && x.Year == year)) {
            return Plannings.FirstOrDefault(x => x.EmployeeId == employeeId && x.Month == month && x.Year == year)?.Value;
        } else {
            return 0;
        }
    }
}

