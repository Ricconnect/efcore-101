using MedicineManager.App.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace MedicineManager.App.Data.Repositories
{
    public class MedicationDbContext : DbContext
    {
        public MedicationDbContext(DbContextOptions<MedicationDbContext> options) : base(options)
        {
        }
        public DbSet<Medicine> Medicines { get; set; }
        public DbSet<IntakeSchedule> IntakeSchedules { get; set; }
    }
}
