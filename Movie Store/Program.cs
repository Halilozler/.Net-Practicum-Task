using System.Reflection;
using Microsoft.EntityFrameworkCore;
using Movie_Store.DbOperations;
using Movie_Store.Extension;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

//Servisi ekledik.
builder.Services.AddServiceDI();

//------------------------------------------------------//
//Db Ye bağlantı yaptık
//builder.Services.AddDbContext<MovieDbContext>(opt => opt.UseInMemoryDatabase(databaseName: "MovieDB"));
builder.Services.AddDbContextDI(builder.Configuration);
//MovieDbContext oluşturualım.
//builder.Services.AddScoped<IMovieDbContext>(provider => provider.GetService<MovieDbContext>());
//------------------------------------------------------//

//------------------------------------------------------//
//AutoMapper Ekliyoruz.
builder.Services.AddAutoMapper(Assembly.GetExecutingAssembly());
//------------------------------------------------------//

//------------------------------------------------------//

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

