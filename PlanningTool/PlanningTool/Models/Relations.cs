using System;

namespace PlanningTool.Models;

public class Relations
{
    public required int Id { set; get; }
    public int? ChapterId { set; get; }
    public int? EmployeeId { set; get; }
    public int? FeatureId { set; get; }
    public int? ReleaseVersionId { set; get; }
}
