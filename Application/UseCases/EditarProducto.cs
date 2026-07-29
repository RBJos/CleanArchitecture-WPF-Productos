    using Domain;
using ApplicationCore.Interfaces;


namespace ApplicationCore.UseCases
{
    public class EditarProducto
    {
        private readonly IProductoRepository _repo;
        public EditarProducto(IProductoRepository repo) => _repo = repo;
            
        public async Task EjecutarAsync(Guid id, string nombre, decimal precio)
        {
            
            var producto = await _repo.ObtenerPorIdAsync(id);
            if (producto == null) throw new Exception("Producto no encontrado");

            producto.ActualizarDatos(nombre, precio); // usas tu método
            await _repo.ActualizarAsync(producto);
        }
    }


}
