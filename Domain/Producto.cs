namespace Domain
{
    public  class Producto
    {
        public Guid Id { get; private set; } = Guid.NewGuid();
        public string Nombre { get; private set; } = string.Empty;
        public decimal Precio { get; private set; }

        public Producto() { } //Solo para EF Core
        public Producto(string nombre, decimal precio)
        {

            if(string.IsNullOrWhiteSpace(nombre))
            {
                throw new ArgumentException("El nombre del producto no puede estar vacío.", nameof(nombre));
            }
            if(precio <= 0)
            {
                throw new ArgumentException("El precio del producto no puede ser negativo o cero.", nameof(precio));
            }   

            Nombre = nombre;
            Precio = precio;
        }


        public void ActualizarDatos(string nombre, decimal precio)
        {
            if (string.IsNullOrWhiteSpace(nombre))
            {
                throw new ArgumentException("El nombre del producto no puede estar vacío.", nameof(nombre));
            }
            if (precio <= 0)
            {
                throw new ArgumentException("El precio del producto no puede ser negativo o cero.", nameof(precio));
            }

            Nombre = nombre;
            Precio = precio;
        }
    }
}
