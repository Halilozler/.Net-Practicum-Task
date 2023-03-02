﻿using System.Reflection;
using Microsoft.EntityFrameworkCore;
using Movie_Store.DbOperations;
using Movie_Store.Extension;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.


//------------------------------------------------------//
//Servisi ekledik.
builder.Services.AddServiceDI();
builder.Services.AddDbContextDI(builder.Configuration);
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

