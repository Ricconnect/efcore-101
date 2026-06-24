namespace MedicineManager.App.Data.Models;

public class Medicine : IEntity
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public double Dosage { get; set; }
    public string DosageUnit { get; set; } = "mg";
}
