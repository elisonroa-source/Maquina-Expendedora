using System;

namespace Maquina_Victor
{
    public class VentaService
    {
        private readonly ProductoRepository productoRepo;

        public VentaService()
        {
            productoRepo = new ProductoRepository();
        }

        public bool RealizarVenta(int productoId, int cantidad)
        {
            var productos = productoRepo.ObtenerProductos();
            var producto = productos.Find(p => p.Id == productoId);

            if (producto == null)
            {
                Console.WriteLine("Producto no encontrado.");
                return false;
            }

            if (producto.Stock < cantidad)
            {
                Console.WriteLine("Stock insuficiente.");
                return false;
            }

            productoRepo.ActualizarStock(productoId, cantidad);
            Console.WriteLine($"Venta realizada: {cantidad} x {producto.Nombre} (${producto.Precio * cantidad})");
            return true;
        }
    }
}
