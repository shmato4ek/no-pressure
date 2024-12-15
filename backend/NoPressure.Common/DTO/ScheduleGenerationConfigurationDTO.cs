namespace NoPressure.Common.DTO;

public class ScheduleGenerationConfigurationDTO
{
    public int IterationsAmount { get; set; }
    public bool IsCrossowerEnabled { get; set; }
    public bool IsMutationEnabled { get; set; }
    
    public int UserId { get; set; }
}