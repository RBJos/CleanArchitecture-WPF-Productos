using Domain;
using ApplicationCore.Interfaces;

namespace ApplicationCore.UseCases
{
    public class EliminarProducto
    {
        private readonly IProductoRepository _repo;
        public EliminarProducto(IProductoRepository repo) => _repo = repo;

        public void Ejecutar(Guid id) => _repo.EliminarAsync(id);
    }
}
