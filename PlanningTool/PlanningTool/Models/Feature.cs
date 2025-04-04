namespace PlanningTool.Models;

public class Feature
{
    public required int Id { set; get; }
    public required int VersionId { set; get; }
    public required String Name { set; get; }
    public int? Estimation { set; get; }
    public required int State { set; get; }
}

