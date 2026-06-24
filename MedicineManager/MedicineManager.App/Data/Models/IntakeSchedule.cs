namespace MedicineManager.App.Data.Models;

/// <summary>
/// Represents when and how often a medicine should be taken.
/// Note: MedicineId is a foreign key — this relationship will be expressed
/// as a navigation property once Entity Framework Core is introduced.
/// </summary>
public class IntakeSchedule : IEntity
{
    public Guid Id { get; set; } = Guid.NewGuid();

    // Foreign key to Medicine — intentionally left without a navigation property
    // to motivate the introduction of EF Core relationships later.
    public Guid MedicineId { get; set; }
    public Medicine Medicine { get; set; }

    public TimeOnly Time { get; set; } = new TimeOnly(8, 0);
    public int FrequencyPerDay { get; set; } = 1;
    public string Notes { get; set; } = string.Empty;
}
