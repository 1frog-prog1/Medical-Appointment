using data;
using data.repository;
using domain.models.user;
using domain.models.user.usecase;
using domain.models.doctor;
using domain.models.sheldue;
using domain.models.appointment;
using domain.models.specialisation;

using Microsoft.EntityFrameworkCore;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDbContext<ApplicationContext>(options => 
options.UseNpgsql($"Host=localhost;Port=5432;Database=MedAppointment;Username=evkima;Password=Qwerty132")
);

builder.Services.AddTransient<IUserRepository, UserRepository>();
builder.Services.AddTransient<UserUsecases>();
builder.Services.AddTransient<IDoctorRepository, DoctorRepository>();
builder.Services.AddTransient<DoctorUsecases>();
builder.Services.AddTransient<ISheldueRepository, SheldueRepository>();
builder.Services.AddTransient<SheldueUsecases>();
builder.Services.AddTransient<ISpecialisationRepository, SpecialisationRepository>();
builder.Services.AddTransient<IAppointmentRepository, AppointmentRepository>();
builder.Services.AddTransient<AppointmentUsecases>();

builder.Services.AddControllers();
builder.Services.AddSwaggerGen();
builder.Services.AddEndpointsApiExplorer();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.Run();
