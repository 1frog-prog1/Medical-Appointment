using Microsoft.EntityFrameworkCore;

using data.models;

namespace data;
public class ApplicationContext : DbContext
{
    public DbSet<UserModel> UserDb { get; set; }
    public DbSet<DoctorModel> DoctorDb {get; set;}
    public DbSet<SpecialisationModel> SpecialisationsDb {get; set;}
    public DbSet<AppointmentModel> AppointmentDb {get; set;}
    public DbSet<SheldueModel> SheldueDb {get; set;}

    public ApplicationContext(DbContextOptions options) : base(options)
    {
        // check if we have db. if we dont, the methid will create one.
        // Database.EnsureCreated();

        // something for DataTime i think
        // AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);

        // AppContext.SetSwitch("Npgsql.DisableDateTimeInfinityConversions", true);
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder) {
        base.OnModelCreating(modelBuilder);
        modelBuilder.Entity<UserModel>().HasIndex(model => model.login);
        
    }

    
}
