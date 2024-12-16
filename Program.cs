using Microsoft.Azure.Cosmos;
using onekarmaapi.Contracts;
using onekarmaapi.Extensions;

var builder = WebApplication.CreateBuilder(args);


// Add other services
builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

builder.Services.AddSwaggerGen();
// Adding services to the container
builder.Services.AddSingleton<endpoint_Users>(builder.Configuration.GetSection("CosmosDb").InitializeCosmosClientInstanceAsync().GetAwaiter().GetResult());



var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();
app.MapGet("/", context =>
{
    context.Response.Redirect("/swagger");
    return Task.CompletedTask;
});
app.MapControllers();
app.Run();
