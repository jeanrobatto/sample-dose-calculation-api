using DoseCalculationAPI.Persistence.Extensions;
using DoseCalculationAPI.Domain.Extensions;

using Scalar.AspNetCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddOpenApi();
builder.Services.AddCosmosDb(builder.Configuration);
builder.Services.AddDoseCalculationDomainServices();
builder.Services.AddDoseCalculationRepository();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.MapScalarApiReference();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

await app.InitializeCosmosDbAsync();

await app.RunAsync();