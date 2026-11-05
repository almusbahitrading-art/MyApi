using Npgsql;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddHealthChecks();

var app = builder.Build();

app.MapHealthChecks("/health");

app.MapGet("/", () => "Hello DevOps!");

app.MapGet("/api/hello/{name}", (string name) => new { message = $"Hello {name}" });

app.MapGet("/version", () => new { version = "1.0.0" });

app.MapGet("/db-check", async (IConfiguration config) =>
{
    var connectionString = config.GetConnectionString("Default");
    await using var conn = new NpgsqlConnection(connectionString);
    await conn.OpenAsync();
    await using var cmd = new NpgsqlCommand("SELECT version()", conn);
    var dbVersion = await cmd.ExecuteScalarAsync();
    return Results.Ok(new { database = "connected", version = dbVersion });
});

app.Run();