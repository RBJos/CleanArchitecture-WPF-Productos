using Domain;
using ApplicationCore.Interfaces;

namespace ApplicationCore.UseCases
{
    public class CrearProducto
    {
        private readonly IProductoRepository _repositorio;

        public CrearProducto(IProductoRepository repositorio)
        {
            _repositorio = repositorio;
        }

        public async Task Ejecutar(string nombre, decimal precio)// Aquí ya explota si es inválido por la validación en el constructor de Producto
        {
            var producto = new Producto(nombre, precio);
           await _repositorio.GuardarAsync(producto);
        }
    }
}
