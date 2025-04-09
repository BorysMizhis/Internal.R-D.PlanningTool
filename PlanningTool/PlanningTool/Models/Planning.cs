using System;

namespace PlanningTool.Models;

public class Planning
{
    public int Id { set; get; } = 0;
    public int FeatureId { set; get; } = 0;
    public int EmployeeId { set; get; } = 0;
    public int Month { set; get; } = 0;
    public int Year { set; get; } = 0;
    public int Value { set; get; } = 0;
}
