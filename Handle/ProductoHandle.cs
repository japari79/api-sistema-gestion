using api_sistema_gestion.Models;
using System.Data.SqlClient;

namespace api_sistema_gestion.Handle
{
    public static class ProductoHandle
    {
        static string connectionString = "Data Source=DESKTOP-B08FRCB;Initial Catalog=SistemaGestion;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";

        public static List<Producto> getProductosXUsuario(long idUsuario)
        {
            List<Producto> producto = new List<Producto>();

            string qry = "SELECT * FROM dbo.Producto WHERE IdUsuario = @idUsuario";

            using (SqlConnection conex = new SqlConnection(connectionString))
            {
                SqlCommand comando = new SqlCommand(qry, conex);
                comando.Parameters.AddWithValue("@idUsuario", idUsuario);

                conex.Open();

                using (SqlDataReader reader = comando.ExecuteReader())
                {
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            producto.Add(new Producto(Convert.ToInt64(reader["Id"]), Convert.ToString(reader["Descripciones"]), Convert.ToDecimal(reader["Costo"]), Convert.ToDecimal(reader["PrecioVenta"]), Convert.ToInt32(reader["Stock"]), Convert.ToInt64(reader["IdUsuario"])));
                        }
                    }
                }

                return producto;
            }
        }

        public static bool postProducto(Producto producto)
        {
            string qry = "INSERT INTO dbo.Producto(Descripciones,Costo,PrecioVenta,Stock,IdUsuario) " +
                         "VALUES(@Descripciones, @Costo, @PrecioVenta, @Stock, @IdUsuario)";

            using (SqlConnection conex = new SqlConnection(connectionString))
            {
                SqlCommand comando = new SqlCommand(qry, conex);
                comando.Parameters.AddWithValue("@Descripciones", producto.Descripciones);
                comando.Parameters.AddWithValue("@Costo", producto.Costo);
                comando.Parameters.AddWithValue("@PrecioVenta", producto.PrecioVenta);
                comando.Parameters.AddWithValue("@Stock", producto.Stock);
                comando.Parameters.AddWithValue("@IdUsuario", producto.IdUsuario);

                conex.Open();

                comando.ExecuteNonQuery();
            }

            return true;
        }

        public static bool putProducto(Producto producto)
        {
            string qry = "UPDATE dbo.Producto SET " +
                         "Descripciones = @Descripciones, " +
                         "Costo = @Costo, " +
                         "PrecioVenta = @PrecioVenta, " +
                         "Stock = @Stock, " +
                         "IdUsuario = @IdUsuario " +
                         "WHERE Id = @Id";

            using (SqlConnection conex = new SqlConnection(connectionString))
            {
                SqlCommand comando = new SqlCommand(qry, conex);
                comando.Parameters.AddWithValue("@Descripciones", producto.Descripciones);
                comando.Parameters.AddWithValue("@Costo", producto.Costo);
                comando.Parameters.AddWithValue("@PrecioVenta", producto.PrecioVenta);
                comando.Parameters.AddWithValue("@Stock", producto.Stock);
                comando.Parameters.AddWithValue("@IdUsuario", producto.IdUsuario);
                comando.Parameters.AddWithValue("@Id", producto.Id);

                conex.Open();

                comando.ExecuteNonQuery();
            }

            return true;
        }

        public static bool delProducto(long Id)
        {
            string qry1 = "DELETE dbo.ProductoVendido WHERE IdProducto = @IdProducto";
            string qry2 = "DELETE dbo.Producto WHERE Id = @Id";

            using (SqlConnection conex = new SqlConnection(connectionString))
            {
                SqlCommand comandoPV = new SqlCommand(qry1, conex);
                comandoPV.Parameters.AddWithValue("@IdProducto", Id);

                SqlCommand comandoV = new SqlCommand(qry2, conex);
                comandoV.Parameters.AddWithValue("@Id", Id);

                conex.Open();

                comandoPV.ExecuteNonQuery();
                comandoV.ExecuteNonQuery();
            }

            return true;
        }

        public static void updStockProducto(long id, int cantidadVendida)
        {
            string qry = "UPDATE dbo.Producto SET " +
                         "Stock = Stock - @cantidadVendida " +
                         "WHERE Id = @Id";

            using (SqlConnection conex = new SqlConnection(connectionString))
            {
                SqlCommand comando = new SqlCommand(qry, conex);
                comando.Parameters.AddWithValue("@cantidadVendida", cantidadVendida);
                comando.Parameters.AddWithValue("@Id", id);

                conex.Open();

                comando.ExecuteNonQuery();
            }
        }
    }
}
