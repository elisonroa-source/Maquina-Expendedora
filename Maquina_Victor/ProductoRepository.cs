using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace Maquina_Victor
{
    public class ProductoRepository
    {
        private readonly Conexion conexion;

        public ProductoRepository()
        {
            conexion = new Conexion();
        }

        // Obtener todos los productos
        public List<Producto> ObtenerProductos()
        {
            var productos = new List<Producto>();

            using (SqlConnection conn = conexion.GetConnection())
            {
                conn.Open();
                string query = "SELECT Id, Nombre, Precio, Stock, Imagen FROM Productos";
                SqlCommand cmd = new SqlCommand(query, conn);
                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    productos.Add(new Producto
                    {
                        Id = reader.GetInt32(0),
                        Nombre = reader.GetString(1),
                        Precio = reader.GetDecimal(2),
                        Stock = reader.GetInt32(3),
                        Imagen = reader.IsDBNull(4) ? null : reader.GetString(4)
                    });
                }
            }

            return productos;
        }

        // Insertar producto nuevo
        public void InsertarProducto(Producto producto)
        {
            using (SqlConnection conn = conexion.GetConnection())
            {
                conn.Open();
                string query = "INSERT INTO Productos (Nombre, Precio, Stock, Imagen) VALUES (@nombre, @precio, @stock, @imagen)";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@nombre", producto.Nombre);
                cmd.Parameters.AddWithValue("@precio", producto.Precio);
                cmd.Parameters.AddWithValue("@stock", producto.Stock);
                cmd.Parameters.AddWithValue("@imagen", (object)producto.Imagen ?? DBNull.Value);
                cmd.ExecuteNonQuery();
            }
        }

        // Actualizar producto existente
        public void ActualizarProducto(Producto producto)
        {
            using (SqlConnection conn = conexion.GetConnection())
            {
                conn.Open();
                string query = "UPDATE Productos SET Nombre=@nombre, Precio=@precio, Stock=@stock, Imagen=@imagen WHERE Id=@id";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@id", producto.Id);
                cmd.Parameters.AddWithValue("@nombre", producto.Nombre);
                cmd.Parameters.AddWithValue("@precio", producto.Precio);
                cmd.Parameters.AddWithValue("@stock", producto.Stock);
                cmd.Parameters.AddWithValue("@imagen", (object)producto.Imagen ?? DBNull.Value);
                cmd.ExecuteNonQuery();
            }
        }

        // Eliminar producto por Id
        public void EliminarProducto(int id)
        {
            using (SqlConnection conn = conexion.GetConnection())
            {
                conn.Open();
                string query = "DELETE FROM Productos WHERE Id=@id";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@id", id);
                cmd.ExecuteNonQuery();
            }
        }

        // Actualizar stock (para ventas)
        public void ActualizarStock(int productoId, int cantidad)
        {
            using (SqlConnection conn = conexion.GetConnection())
            {
                conn.Open();
                string query = "UPDATE Productos SET Stock = Stock - @cantidad WHERE Id = @id";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@cantidad", cantidad);
                cmd.Parameters.AddWithValue("@id", productoId);
                cmd.ExecuteNonQuery();
            }
        }
    }
}
