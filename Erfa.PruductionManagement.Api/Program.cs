using Erfa.ProductionManagement.Persistance;
using Erfa.PruductionManagement.Api;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

var app = builder
    .ConfigureServices()
    .ConfigurePipeline();

using var scope = app.Services.CreateScope();

var context = scope.ServiceProvider.GetService<ErfaDbContext>();
if (context != null)
{
    try
    {
        context.Database.Migrate();
    } catch (Exception e )
    {
        Console.WriteLine(e.ToString());
    }
}

app.Run();

public partial class Program { }