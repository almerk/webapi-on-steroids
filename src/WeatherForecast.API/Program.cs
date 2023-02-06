using LanguageExt.Common;
using MediatR;
using WeatherForecast.API.Commands;
using WeatherForecast.API.Middlewares;
using WeatherForecast.API.PipelineValidators;
using WeatherForecast.API.Queries;
using WeatherForecast.API.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddTransient<GuidGenerationService>();
builder.Services.AddSingleton<WeatherForecastService>();
builder.Services.AddMediatR(typeof(Program));

builder.Services.AddTransient(//TODO: register in another way
        typeof(IPipelineBehavior<GetWeatherForecastByIdQuery, Result<WeatherForecast.API.Models.WeatherForecast>>),
        typeof(WeatherForecastExistPipelineValidator))
    .AddTransient(
        typeof(IPipelineBehavior<UpdateWeatherForecastCommand, Result<WeatherForecast.API.Models.WeatherForecast>>),
        typeof(WeatherForecastExistPipelineValidator)
    );

var app = builder.Build();

app.UseMiddleware<ExceptionHandlingMiddleware>();
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
