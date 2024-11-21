namespace NoPressure.DAL.Entities;

public class ScheduleGenerationConfiguration
{
    public int Id { get; set; }
    public int IterationsAmount { get; set; }
    public bool IsCrossowerEnabled { get; set; }
    public bool IsMutationEnabled { get; set; }
    
    public int UserId { get; set; }
}