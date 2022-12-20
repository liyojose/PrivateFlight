using Microsoft.EntityFrameworkCore;
using PrivateFlight.Data;
using PrivateFlight.Repo;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<IMessageRepository, InMemoeryMessageRepository>();
var app = builder.Build();

//var connectionString = builder.Configuration.GetConnectionString("DBCoonection");
//builder.Services.AddDbContext<PrivateFlightContext>(x => x.UseSqlServer(connectionString));

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
