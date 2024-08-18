using Microsoft.EntityFrameworkCore;
using TestCQRS.Application.Commands;
using TestCQRS.Application.Queries;
using TestCQRS.Application.Repository.Interface;
using TestCQRS.Infrastructure.Commands;
using TestCQRS.Infrastructure.Queries;
using TestCQRS.Infrastructure.Repository;
using TestCQRS.Infrastrucuture.Context;

var builder = WebApplication.CreateBuilder(args);

var services = builder.Services;
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
services.AddDbContext<ApplicationContext>(options =>
    options.UseSqlServer(connectionString, b => b.MigrationsAssembly("Test")));

services.AddScoped<IAnnouncementRepository, AnnouncementRepository>();
services.AddControllers();
services.AddEndpointsApiExplorer();
services.AddSwaggerGen();

builder.Services.AddMediatR(x =>
    x.RegisterServicesFromAssemblies(typeof(CreateAnnouncementHandler).Assembly, typeof(CreateAnnouncementCommand).Assembly));

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapControllers();

app.Run();