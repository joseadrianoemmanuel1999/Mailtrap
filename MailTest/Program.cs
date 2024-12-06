using System.Net;
using System.Net.Mail;
using MailTest.Domain;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.OpenApi.Models;
using MimeKit;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();
builder.Services.AddSwaggerGen();
builder.Services.AddSwaggerGen(x =>
{
x.SwaggerDoc("v1",new OpenApiInfo
{
    Title = "MailTest",
    Version = "v1",
    Description = "An Example of an ASP .net Core API",
    Contact = new OpenApiContact
    {
        Name = "Example Contact",
        Email = "Example@email.com",
        Url = new Uri("https://example.com/contact")
    },
});
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

var summaries = new[]
{
    "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
};

app.MapGet("/weatherforecast", () =>
    {
        var forecast = Enumerable.Range(1, 5).Select(index =>
                new WeatherForecast
                (
                    DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
                    Random.Shared.Next(-20, 55),
                    summaries[Random.Shared.Next(summaries.Length)]
                ))
            .ToArray();
        return forecast;
    })
    .WithName("GetWeatherForecast");
app.MapPost("/post/", async ( MailData request) =>
{
    MailAddress to = new MailAddress("colin.jakubowski62@ethereal.email");
    MailAddress from = new MailAddress("colin.jakubowski62@ethereal.email");

    MailMessage email = new MailMessage(from, to);
    email.Subject = request.EmailSubject;
    email.Body = request.EmailBody;

    SmtpClient smtp = new SmtpClient();
    smtp.Host = "smtp.ethereal.email";
    smtp.Port =  587;
    smtp.Credentials = new NetworkCredential("colin.jakubowski62@ethereal.email", "7Ktnbs1am21z6u6yyt");
    smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
    smtp.EnableSsl = true;
    try
    {
        
        smtp.Send(email);
    }
    catch (SmtpException ex)
    {
        Console.WriteLine(ex.ToString());
    }
    return ("funcionou");
    
    
});

app.Run();

record WeatherForecast(DateOnly Date, int TemperatureC, string? Summary)
{
    public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
}