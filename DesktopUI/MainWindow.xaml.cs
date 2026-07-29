using ApplicationCore.UseCases;
using Domain;
using Microsoft.Extensions.DependencyInjection;
using MiErpLite.ApplicationCore;
using System.Windows;
using System.Windows.Controls;

namespace DesktopUI;

public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();
        CargarProductosAsync(); // Al abrir la ventana, ya muestra lo que hay
    }

    private async void BtnGuardar_ClickAsync(object sender, RoutedEventArgs e)
    {
        try
        {
            if (string.IsNullOrWhiteSpace(txtNombre.Text))
            {
                MessageBox.Show("El nombre es obligatorio");
                return;
            }

            if (!decimal.TryParse(txtPrecio.Text, out decimal precio))
            {
                MessageBox.Show("El precio debe ser un número válido", "Validación", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            if (BtnGuardar.Tag == null)
            {
                // 1. Vas a la recepción
                var servicioCrear = App.Services.GetRequiredService<CrearProducto>();

                // 2. Tomas lo que escribió el usuario y se ejecuta
                servicioCrear.Ejecutar(txtNombre.Text, decimal.Parse(txtPrecio.Text));
            }
            else
            {
                var servicioEditar = App.Services.GetRequiredService<EditarProducto>();

                await servicioEditar.EjecutarAsync((Guid)BtnGuardar.Tag,txtNombre.Text,precio);
            }


            // 3. Limpias y recargas la tabla e inicializas el Tag del botón GuardarAsync
            txtNombre.Clear();
            txtPrecio.Clear();
            BtnGuardar.Tag = null;
            BtnGuardar.Content = "Guardar";

            CargarProductosAsync();

            BtnCancelar.Visibility = Visibility.Collapsed;
        }
        catch (Exception ex)
        {
            MessageBox.Show(ex.Message, "Validación", MessageBoxButton.OK, MessageBoxImage.Warning);
        }
            
    }

    private async void BtnListar_ClickAsync(object sender, RoutedEventArgs e)
    {
       CargarProductosAsync();
    }

    private async void BtnEliminar_ClickAsynck(object sender, RoutedEventArgs e)
    {
        var producto = (Producto)((Button)sender).DataContext;

        if (MessageBox.Show($"¿Borrar {producto.Nombre}?", "Confirmar", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
        {
            var servicio = App.Services.GetRequiredService<EliminarProducto>();
            servicio.Ejecutar(producto.Id);
            CargarProductosAsync();
        }
    }

    private async void BtnEditar_ClickAsync(object sender, RoutedEventArgs e)
    {
        var producto = (Producto)((Button)sender).DataContext;
        txtNombre.Text = producto.Nombre;
        txtPrecio.Text = producto.Precio.ToString();

        // Guardamos el Id en el Tag del botón GuardarAsync para saber que es edición
        BtnGuardar.Tag = producto.Id;
        BtnGuardar.Content = "Actualizar";
        BtnCancelar.Visibility = Visibility.Visible;
    }

    private async void BtnCancelar_ClickAsync(object sender, RoutedEventArgs e)
    {
        txtNombre.Clear();
        txtPrecio.Clear();
        BtnGuardar.Tag = null;
        BtnGuardar.Content = "Guardar";
        BtnCancelar.Visibility = Visibility.Collapsed;
        await Task.CompletedTask;
    }

    private async void CargarProductosAsync()
    {
        var servicioListar = App.Services.GetRequiredService<ListarProductos>();
        var lista = await servicioListar.EjecutarAsync();
        dgProductos.ItemsSource = lista;
        await Task.CompletedTask;
    }

    private async void txtBuscar_TextChanged(object sender, TextChangedEventArgs e)
    {
        var servicio = App.Services.GetRequiredService<ListarProductos>();
        dgProductos.ItemsSource = await servicio.BuscarAsync(txtBuscar.Text);
    }

}