using Microsoft.EntityFrameworkCore;

using data.models;

namespace data;
public class ApplicationContext : DbContext
{
    public DbSet<UserModel> Users { get; set; }
    public DbSet<DoctorModel> Doctors {get; set;}
    public DbSet<SpecialisationModel> Specialisations {get; set;}
    public DbSet<AppointmentModel> Appointments {get; set;}
    public DbSet<SheldueModel> Sheldues {get; set;}

    public ApplicationContext(DbContextOptions options) : base(options)
    {
        // check if we have db. if we dont, the methid will create one.
        Database.EnsureCreated();

        // something for DataTime i think
        AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);

        AppContext.SetSwitch("Npgsql.DisableDateTimeInfinityConversions", true);
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder) {
        base.OnModelCreating(modelBuilder);
        modelBuilder.Entity<UserModel>().HasIndex(model => model.login);
        
    }

    
}
