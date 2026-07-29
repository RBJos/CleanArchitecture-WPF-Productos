using Domain;

namespace ApplicationCore.Interfaces
{
    public interface IProductoRepository
    {
        Task GuardarAsync(Producto producto);
        Task<List<Producto>> ObtenerTodosAsync();
        Task EliminarAsync(Guid id);
        Task ActualizarAsync(Producto producto);
        Task<Producto?> ObtenerPorIdAsync(Guid id);
        Task<List<Producto>> BuscarPorNombreAsync(string texto);
    }
}
                                           