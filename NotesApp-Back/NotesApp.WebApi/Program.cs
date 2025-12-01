using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using NotesApp.BLL;
using NotesApp.BLL.Behavior;
using NotesApp.BLL.Interfaces.Logging;
using NotesApp.BLL.Services.Logging;
using NotesApp.DAL.Data;
using NotesApp.DAL.Repositories.Interfaces.Base;
using NotesApp.DAL.Repositories.Realizations.Base;
using Scalar.AspNetCore;
using Serilog;

var builder = WebApplication.CreateBuilder(args);



builder.Services.AddControllers();
builder.Services.AddOpenApi();


builder.Host.UseSerilog((ctx, lc) =>
{
    lc.WriteTo.Console();
});

builder.Services.AddAutoMapper(cfg =>
{
    cfg.AddMaps(typeof(BllAssemblyMarker).Assembly);
});
builder.Services.AddMediatR(cfg =>
{
    cfg.RegisterServicesFromAssembly(typeof(BllAssemblyMarker).Assembly);
    cfg.AddOpenBehavior(typeof(ValidationBehaviour<,>));
});

builder.Services.AddValidatorsFromAssemblyContaining<BllAssemblyMarker>();

builder.Services.AddScoped<IRepositoryWrapper, RepositoryWrapper>();
builder.Services.AddScoped<ILoggerService, LoggerService>();


var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

builder.Services.AddDbContext<NotesAppDbContext>(options =>
    options.UseSqlServer(connectionString));

builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(policy =>
    {
        policy.WithOrigins("http://localhost:5173", "https://localhost:5173")
              .AllowAnyHeader()
              .AllowAnyMethod();
    });
});


var app = builder.Build();


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.MapScalarApiReference();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.UseCors();

app.MapControllers();

app.Run();
