using System.Reflection;
using _1.Odev.Extension;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

//SerialLog ***************************************
var config = new ConfigurationBuilder()
    .AddJsonFile("appsettings.json")
    .Build();

Log.Logger = new LoggerConfiguration().ReadFrom.Configuration(config).CreateLogger();
Log.Information("Application is starting.");

builder.Host.UseSerilog();
//*************************************************

//Db connection ***************************************
builder.Services.AddDbContextDI(builder.Configuration);
//*************************************************

builder.Services.AddServicesDI();

//AutoMapper için ***************************************
builder.Services.AddAutoMapper(Assembly.GetExecutingAssembly());
//*************************************************

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

//dataGenerator classını çalıştırmak için ***********
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    DataGenerator.Initialize(services);
}
//*************************************************

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

