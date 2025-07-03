using ELLP_Project.Models;
using ELLP_Project.Services;
using ELLP_Project.Interfaces.InterfacesServices;
using ELLP_Project.Persistence.Interfaces.InterfacesRepositorio;
using ELLP_Project.Persistence.Interfaces.InterfacesServices;
using ELLP_Project.Persistence.Repositorios;
using Microsoft.EntityFrameworkCore;
using ELLP_Project.Persistence.DBContext;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection"))
           .UseLazyLoadingProxies());



builder.Services.AddSingleton<IAlunoRepositorio, AlunoRepositorio>();
builder.Services.AddSingleton<IMonitorRepositorio, MonitorRepositorio>();
builder.Services.AddSingleton<IProfessorRepositorio, ProfessorRepositorio>();
builder.Services.AddSingleton<IFaltaRepositorio, FaltaRepositorio>();

// Repositórios - Concretos (usados diretamente nos Services)
builder.Services.AddSingleton<AlunoRepositorio>();
builder.Services.AddSingleton<MonitorRepositorio>();
builder.Services.AddSingleton<ProfessorRepositorio>();
builder.Services.AddSingleton<FaltaRepositorio>();
builder.Services.AddSingleton<OficinaRepositorio>();

// Services - Interfaces
builder.Services.AddSingleton<IAlunoServices, AlunoServices>();
builder.Services.AddSingleton<IMonitorServices, MonitorServices>();
builder.Services.AddSingleton<IOficinaServices, OficinaServices>();
builder.Services.AddScoped<ILoginServices, LoginServices>();
builder.Services.AddSingleton<IProfessorServices, ProfessorServices>();

// Services - Concretos usados diretamente
builder.Services.AddSingleton<MonitorServices>();
builder.Services.AddSingleton<OficinaServices>();
builder.Services.AddSingleton<ProfessorServices>();
builder.Services.AddControllers();


// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();


