using Microsoft.Extensions.Options;
using NOS.Engineering.Challenge.API.Extensions;
using NOS.Engineering.Challenge.Database;
using NOS.Engineering.Challenge.Models;

var builder = WebApplication.CreateBuilder(args)
        .ConfigureWebHost()
        .RegisterServices();

builder.Logging.ClearProviders();
builder.Logging.AddConsole();


builder.Services.Configure<MongoDBSettings>(builder.Configuration.GetSection(nameof(MongoDBSettings)));
builder.Services.AddSingleton<MongoDBContext>(serviceProvider =>
{
    var settings = serviceProvider.GetRequiredService<IOptions<MongoDBSettings>>();
    return new MongoDBContext(settings);
});
builder.Services.AddControllers();

var app = builder.Build();


app.UseHttpsRedirection();
app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();
app.UseSwagger()
    .UseSwaggerUI();

app.Run();