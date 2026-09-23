using Domain;
using Domain.Services;
using Infrastructure;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDomain();
builder.Services.AddInfrastructure(builder.Configuration);
builder.Services.AddControllers();
builder.Services.AddOpenApi();

var app = builder.Build();

app.MapControllers();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    
    app.UseSwaggerUI(options =>
    {
        options.SwaggerEndpoint("/openapi/v1.json", "v1");
    });
}

app.Run();