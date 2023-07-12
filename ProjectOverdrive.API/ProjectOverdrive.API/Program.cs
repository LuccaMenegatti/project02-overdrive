using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using MySqlConnector;
using ProjectOverdrive.API.Config;
using ProjectOverdrive.API.Data;
using ProjectOverdrive.API.Repository;
using ProjectOverdrive.API.Repository.Interfaces;

var builder = WebApplication.CreateBuilder(args);

var connection = builder.Configuration["MySqlConnection:MySqlConnectionString"];
builder.Services.AddDbContext<ApiDbContext>(options =>

    {
        options.UseMySql(connection,
            new MySqlServerVersion(new Version(8, 0, 32)));
        options.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
    });

IMapper mapper = MappingConfig.RegisterMaps().CreateMapper();
builder.Services.AddSingleton(mapper);
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

builder.Services.AddScoped<IPeopleRepository, PeopleRepository>();
builder.Services.AddScoped<ICompanyRepository, CompanyRepository>();


// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddCors();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors(c =>
{
    c.AllowAnyHeader();
    c.AllowAnyMethod();
    c.AllowAnyOrigin();
});

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
