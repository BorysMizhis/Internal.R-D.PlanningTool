using System;

namespace PlanningTool.Models;

public class Relation
{
    public int Id { set; get; } = 0;
    public int ChapterId { set; get; } = 0;
    public int EmployeeId { set; get; } = 0;
    public int FeatureId { set; get; } = 0;
    public int ReleaseVersionId { set; get; } = 0;
}
