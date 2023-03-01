using System.Reflection;
using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Net_Core_Patika.DbOperations;
using Net_Core_Patika.Middlewares;
using Net_Core_Patika.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

//DB ye Contextimizi gösterdik.
builder.Services.AddDbContext<BookStoreDbContext>(opt => opt.UseInMemoryDatabase(databaseName: "BookStoreDB"));
//BookStoreDbContext oluşturualım.
builder.Services.AddScoped<IBookStoreDbContext>(provider => provider.GetService<BookStoreDbContext>());

//AutoMapper Ekliyoruz.
builder.Services.AddAutoMapper(Assembly.GetExecutingAssembly());

//Authontication ekliyoruz.
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(opt =>
{
    //token nasıl çalışsın;
    opt.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateAudience = true,    //Kimler kullanabilir içinde bulunsun meslea Audience = "admin" var admin erişimi var gibi
        ValidateIssuer = true,      //Dağıtıcısı kim
        ValidateLifetime = true,    //Lifetime bak eğer zamanı dolduysa erişemez olsun
        ValidateIssuerSigningKey = true,    //Tokenı kripto yapacağımız anahtar olsun.
        ValidIssuer = builder.Configuration["Token:Issuer"],    //Tokenın kriptosu şudur
        ValidAudience = builder.Configuration["Token:Audience"],    //
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Token:SecurityKey"])),
        //SecurityKey al şifrele keyi oluştur.
        ClockSkew = TimeSpan.Zero   //sunucu ve clientların timezonu farklı olduğu durumlarda mesela client önde cliente erken biter bunun önüne geçer token içinde zamanı halederiz.
    };
});

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

app.UseAuthentication();

app.UseAuthorization();

//custom Middlewera burada tanımlayıp çalıştırıyoruz.
app.UseCustomExceptionMiddle();

app.MapControllers();

app.Run();

