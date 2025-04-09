namespace PlanningTool.Models;

public class ReleaseVersion
{
    public int Id { set; get; } = 0;
    public String Name { set; get; } = string.Empty;
    public int State { set; get; } = 0;

    public List<Feature>? Features { set; get; }
}

