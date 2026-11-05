var builder = WebApplication.CreateBuilder(args);

builder.Services.AddHealthChecks();

var app = builder.Build();

app.MapHealthChecks("/health");

app.MapGet("/", () => "Hello DevOps!");

app.MapGet("/api/hello/{name}", (string name) => new { message = $"Hello {name}" });

app.MapGet("/version", () => new { version = "1.0.0" });

app.Run();