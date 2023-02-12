using CarRent.Application.Configuration;
using CarRent.Database.Configuration;
using CarRent.WebApi.Converters;
using Microsoft.OpenApi.Any;
using Microsoft.OpenApi.Models;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllers().AddJsonOptions(options =>
{
    options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
    options.JsonSerializerOptions.Converters.Add(new DateOnlyJsonConverter());
});
builder.Services.AddDatabaseServices(builder.Configuration);
builder.Services.AddMediatorServices().AddValidators();
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo { Title = "Car Rental", Version = "v1" });
    options.CustomSchemaIds(x => x.FullName!.Replace("+", "."));
    options.MapType<DateOnly>(() => new OpenApiSchema
    {
        Type = "string",
        Format = "date",
        Example = new OpenApiString("2022-01-01")
    });
});

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

// Used for integration tests
public partial class Program { }