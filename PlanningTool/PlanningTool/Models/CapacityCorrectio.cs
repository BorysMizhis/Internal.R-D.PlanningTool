namespace PlanningTool.Models;

public class CapacityCorrection
{
    public required int Id { set; get; }
    public required int EmployeeId { set; get; }
    public required int CorrectionMonth { set; get; }
    public required int CorrectionYear { set; get; }
    public required int CorrectionValue { set; get; }
}

