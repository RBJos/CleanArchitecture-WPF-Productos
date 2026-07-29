using Domain;
using ApplicationCore.Interfaces;
using Microsoft.EntityFrameworkCore;


namespace Infrastructure.Persistence;

public class ProductoRepository : IProductoRepository
{
    private readonly AppDbContext _context;
    public ProductoRepository(AppDbContext context) => _context = context;

    public async Task GuardarAsync(Producto producto)
    {
        _context.Productos.Add(producto);
        await _context.SaveChangesAsync();
    }

    public async Task<List<Producto>> ObtenerTodosAsync() => await _context.Productos.ToListAsync();

    public async Task EliminarAsync (Guid id)
    {
 
        var prod = await _context.Productos.FindAsync(id);
        if (prod != null)
        {
            _context.Productos.Remove(prod);
            await _context.SaveChangesAsync();
        }
    }

    public async Task ActualizarAsync(Producto producto)
    {
        //var prod = _context.Productos.Update(producto); // Ya no necesitas .Update(), porque el objeto ya viene de la base
        //_context.Productos.Update(producto);// y el _context ya sabe que lo modificaste con ActualizarDatos()
        await _context.SaveChangesAsync();
    }

    public async Task<Producto?> ObtenerPorIdAsync(Guid id) => await _context.Productos.FindAsync(id);

    public async Task<List<Producto>> BuscarPorNombreAsync(string texto)
    {
        return await _context.Productos
            .Where(p => p.Nombre.ToLower().Contains(texto.ToLower()))
            .ToListAsync();
    }
}