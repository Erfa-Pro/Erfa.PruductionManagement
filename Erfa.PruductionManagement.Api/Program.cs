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
        Console.WriteLine("\n\nAAA");

        Thread.Sleep(3000);

        context.Database.Migrate();
        Console.WriteLine("\n\nBBB");

    } catch (Exception e )
    {
        Console.WriteLine(e.ToString());
    }
}

app.Run();

public partial class Program { }