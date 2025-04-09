using Microsoft.Data.Sqlite;
using PlanningTool.DBInterface;

namespace PlanningTool.Models;

public class Employee
{
    public int Id { set; get; } = 0;
    public string Name { set; get; } = string.Empty;
    public int DefaultCapacity { set; get; } = 0;
    public List<Capacity> AvailableCapacity { set; get; } = new List<Capacity>();
    public List<Planning> Plannings { set; get; } = new List<Planning>();

    public int? GetAvailableCapacity(int month, int year) {
        if(AvailableCapacity.Any(x => x.Month == month && x.Year == year)) {
            return AvailableCapacity.FirstOrDefault(x => x.Month == month && x.Year == year)?.Value;
        } else {
            return DefaultCapacity;
        }
    }

    public int? GetPlannedCapacity(int month, int year) {
        if(Plannings.Any(x => x.Month == month && x.Year == year)) {
            return Plannings.FirstOrDefault(x => x.Month == month && x.Year == year)?.Value;
        } else {
            return 0;
        }
    }

    public string GetCapacity(int month, int year) 
    {
        return $"{GetPlannedCapacity(month, year)} / {GetAvailableCapacity(month, year)}";
    }
}
