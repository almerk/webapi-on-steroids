using LanguageExt.Common;
using MediatR;
using WeatherForecast.API.Commands;
using WeatherForecast.API.Middlewares;
using WeatherForecast.API.PipelineValidators;
using WeatherForecast.API.Queries;
using WeatherForecast.API.Services;
using Asp.Versioning;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddTransient<GuidGenerationService>();
builder.Services.AddSingleton<WeatherForecastService>();
builder.Services.AddMediatR(typeof(Program));
builder.Services.AddAutoMapper(typeof(Program));
builder.Services.AddApiVersioning(opt => 
{ 
    opt.DefaultApiVersion = new ApiVersion(1.2);
    opt.AssumeDefaultVersionWhenUnspecified = true;
    opt.UnsupportedApiVersionStatusCode = (int)System.Net.HttpStatusCode.NotImplemented;
    opt.ApiVersionReader = new QueryStringApiVersionReader("v");

});

builder.Services.AddTransient(//TODO: register in another way
        typeof(IPipelineBehavior<GetWeatherForecastByIdQuery, Result<WeatherForecast.API.Models.WeatherForecast>>),
        typeof(WeatherForecastExistPipelineValidator))
    .AddTransient(
        typeof(IPipelineBehavior<UpdateWeatherForecastCommand, Result<WeatherForecast.API.Models.WeatherForecast>>),
        typeof(WeatherForecastExistPipelineValidator)
    );

var app = builder.Build();

var apiVersionSet = app.NewApiVersionSet()
    .HasApiVersion(new ApiVersion(1, 1))
    .HasApiVersion(new ApiVersion(1, 2))
    .ReportApiVersions()
    .Build();

app.UseMiddleware<ExceptionHandlingMiddleware>();
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapGet("", (HttpContext c) => $"api v{c.GetRequestedApiVersion()} is obsolete")
    .WithApiVersionSet(apiVersionSet)
    .MapToApiVersion(1, 1);

app.MapControllers()
    .WithApiVersionSet(apiVersionSet)
    .MapToApiVersion(1, 2);



app.Run();
