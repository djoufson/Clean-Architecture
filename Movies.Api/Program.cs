using Movies.Api;
using Movies.Infrastructure.Data;

var builder = WebApplication.CreateBuilder(args);

// Add builder.Services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// #Djouf# Configure custom Services
builder.Services.ConfigureServices(builder.Configuration);


var app = builder.Build();

if (args.Any() && args[0] == "seed")
{
    using var scope = app.Services.CreateScope();
    var context = scope.ServiceProvider.GetService<MoviesContext>();
    await MoviesContextSeed.SeedDatas(context!, app.Logger);
}
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
