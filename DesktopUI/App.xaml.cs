using ApplicationCore.Interfaces;
using ApplicationCore.UseCases;
using Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using MiErpLite.ApplicationCore;
using System.Windows;

namespace DesktopUI;

public partial class App : System.Windows.Application
{
    public static ServiceProvider Services { get; private set; } = null!;

    protected override void OnStartup(StartupEventArgs e)
    {
        var collection = new ServiceCollection();

        collection.AddDbContext<AppDbContext>(opt => opt.UseSqlite("Data Source=erp.db"));

        collection.AddScoped<IProductoRepository, ProductoRepository>();
        collection.AddScoped<CrearProducto>();
        collection.AddScoped<ListarProductos>();
        collection.AddTransient<EliminarProducto>();
        collection.AddTransient<EditarProducto>();

        Services = collection.BuildServiceProvider();

        // Crea el archivo erp.db si no existe
        using var scope = Services.CreateScope();
        var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
        db.Database.EnsureCreated();

        base.OnStartup(e);
    }
}

