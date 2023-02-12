using api_sistema_gestion.Models;
using System.Data.SqlClient;

namespace api_sistema_gestion.Handle
{
    public class ProductoVentaHandle
    {
        static string connectionString = "Data Source=DESKTOP-B08FRCB;Initial Catalog=SistemaGestion;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
        
        public static List<ProductoVenta> getProductosVendidosXUsuario(long idUsuario)
        {
            List<ProductoVenta> productoVenta = new List<ProductoVenta>();

            string qry = "SELECT PV.Id, PV.Stock, PV.IdProducto, PV.IdVenta " +
                         "FROM dbo.ProductoVendido PV " +
                         "INNER JOIN dbo.Venta V ON PV.IdVenta = V.Id " +
                         "WHERE V.IdUsuario = @idUsuario";

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
                            productoVenta.Add(new ProductoVenta(Convert.ToInt64(reader["Id"]), Convert.ToInt64(reader["IdProducto"]), Convert.ToInt32(reader["Stock"]), Convert.ToInt64(reader["IdVenta"])));
                        }
                    }
                }

                return productoVenta;
            }
        }

        public static void postProductoVenta(long IdUsuario, List<Producto> productosVendidos)
        {
            // Registro la Venta y obtengo el ID de la Venta
            long IdVenta = VentaHandle.postVenta(IdUsuario);

            if (IdVenta != 0)
            {
                string qry1 = "INSERT INTO dbo.ProductoVendido(Stock,IdProducto,IdVenta) " +
                         "VALUES(@Stock, @IdProducto, @IdVenta)";

                using (SqlConnection conex = new SqlConnection(connectionString))
                {
                    conex.Open();

                    foreach (var item in productosVendidos)
                    {
                        SqlCommand comando = new SqlCommand(qry1, conex);

                        comando.Parameters.AddWithValue("@Stock", item.Stock);
                        comando.Parameters.AddWithValue("@IdProducto", item.Id);
                        comando.Parameters.AddWithValue("@IdVenta", IdVenta);

                        int resp = comando.ExecuteNonQuery();

                        if (resp == 1)
                        {
                            ProductoHandle.updStockProducto(item.Id, item.Stock);
                        }
                        
                    }
                }
            }
        }
    }
}
