using api_sistema_gestion.Models;
using System.Data.SqlClient;

namespace api_sistema_gestion.Handle
{
    public class VentaHandle
    {
        static string connectionString = "Data Source=DESKTOP-B08FRCB;Initial Catalog=SistemaGestion;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";

        public static List<Venta> getVentasXUsuario(long idUsuario)
        {
            List<Venta> ventas = new List<Venta>();

            string qry = "SELECT * FROM dbo.Venta WHERE IdUsuario = @idUsuario";

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
                            ventas.Add(new Venta(Convert.ToInt64(reader["Id"]), reader["Comentarios"].ToString(), Convert.ToInt64(reader["IdUsuario"])));
                        }
                    }
                }

                return ventas;
            }
        }

        public static long postVenta(long IdUsuario)
        {
            string qry = "INSERT INTO dbo.Venta(IdUsuario) " +
                         "VALUES(@IdUsuario); SELECT @@IDENTITY";

            using (SqlConnection conex = new SqlConnection(connectionString))
            {
                SqlCommand comando = new SqlCommand(qry, conex);
                comando.Parameters.AddWithValue("@IdUsuario", IdUsuario);

                conex.Open();

                long resp = Convert.ToInt64(comando.ExecuteScalar());

                return resp;
            }
        }
    }
}
