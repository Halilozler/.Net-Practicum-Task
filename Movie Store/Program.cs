using System.Reflection;
using Microsoft.EntityFrameworkCore;
using Movie_Store.DbOperations;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

//------------------------------------------------------//
//Db Ye bağlantı yaptık
builder.Services.AddDbContext<MovieDbContext>(opt => opt.UseInMemoryDatabase(databaseName: "MovieDB"));
//MovieDbContext oluşturualım.
builder.Services.AddScoped<IMovieDbContext>(provider => provider.GetService<MovieDbContext>());
//------------------------------------------------------//

//------------------------------------------------------//
//AutoMapper Ekliyoruz.
builder.Services.AddAutoMapper(Assembly.GetExecutingAssembly());
//------------------------------------------------------//

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

