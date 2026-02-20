using System.Data.SqlClient;

namespace Maquina_Victor
{
    public class Conexion
    {
        private readonly string connectionString;

        public Conexion()
        {
            // Ajusta el nombre del servidor y la base de datos
            connectionString = @"Server=DESKTOP-HPAM3LL\SQLEXPRESS;Database=ExpendedoraDB;Trusted_Connection=True;";
        }

        public SqlConnection GetConnection()
        {
            return new SqlConnection(connectionString);
        }
    }
}
