using System;

namespace PlanningTool.Models;

public class Chapter
{
    public int Id { set; get; } = 0;
    public String Name { set; get; } = string.Empty;

    public List<Employee>? Employees { set; get;}
}
