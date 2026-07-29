using Domain;
using ApplicationCore.Interfaces;

namespace MiErpLite.ApplicationCore
{
    public class ListarProductos
    {
        private readonly IProductoRepository _repo;
        public ListarProductos(IProductoRepository repo)
        {
            _repo = repo;
        }

        public async Task<List<Producto>> EjecutarAsync()
        {
            return await _repo.ObtenerTodosAsync();
        }

        public async Task<List<Producto>> BuscarAsync(string texto)
        {
            if (string.IsNullOrWhiteSpace(texto))
                return await _repo.ObtenerTodosAsync();

            return await _repo.BuscarPorNombreAsync(texto);
        }
    }
}