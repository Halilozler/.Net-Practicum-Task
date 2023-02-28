using System.Reflection;
using Microsoft.EntityFrameworkCore;
using Net_Core_Patika.DbOperations;
using Net_Core_Patika.Middlewares;
using Net_Core_Patika.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

//DB ye Contextimizi gösterdik.
builder.Services.AddDbContext<BookStoreDbContext>(opt => opt.UseInMemoryDatabase(databaseName: "BookStoreDB"));

//AutoMapper Ekliyoruz.
builder.Services.AddAutoMapper(Assembly.GetExecutingAssembly());

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//Logger service burada tanımlıyoruz.(bağımlılığı yok uygulama ilk açılınca oluşsun)
builder.Services.AddSingleton<ILoggerService, ConsoleLogger>();

var app = builder.Build();

//dataGenerator classını çalıştırmak için.
using(var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    DataGenerator.Initialize(services);
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

//custom Middlewera burada tanımlayıp çalıştırıyoruz.
app.UseCustomExceptionMiddle();

app.MapControllers();

app.Run();

