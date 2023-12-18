using LibearayManagementSystem.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.AspNetCore.Cors;
using Microsoft.Extensions.Configuration;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
    builder.Services.AddControllers();
    builder.Services.AddDbContext<LibDBContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("LMSConnection")));
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
app.UseCors(options => options.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());

app.Use(async (context, next) =>

{

    if (context.Request.Method == "OPTIONS")

    {

        context.Response.Headers.Add("Access-Control-Allow-Origin", "http://localhost:3000");

        context.Response.Headers.Add("Access-Control-Allow-Headers", "Content-Type,Accept,Authorization");

        context.Response.Headers.Add("Access-Control-Allow-Methods", "POST, PUT, GET, PATCH, OPTIONS, DELETE");

        context.Response.StatusCode = 200;

        return;

    }

    await next();

});

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
