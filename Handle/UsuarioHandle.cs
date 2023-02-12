﻿using api_sistema_gestion.Models;
using System.Data.SqlClient;
using System.Reflection.Metadata.Ecma335;

namespace api_sistema_gestion.Handle
{
    public class UsuarioHandle
    {
        static string connectionString = "Data Source=DESKTOP-B08FRCB;Initial Catalog=SistemaGestion;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";

        public static Usuario getUsuario(long id)
        {
            Usuario usuario = new Usuario();

            string qry = "SELECT * FROM dbo.Usuario WHERE id = @id";

            using (SqlConnection conex = new SqlConnection(connectionString))
            {
                SqlCommand comando = new SqlCommand(qry, conex);
                comando.Parameters.AddWithValue("@id", id);

                conex.Open();

                using (SqlDataReader reader = comando.ExecuteReader())
                {
                    if (reader.HasRows)
                    {
                        reader.Read();
                        usuario.Id = reader.GetInt64(0);
                        usuario.Nombre = reader.GetString(1);
                        usuario.Apellido = reader.GetString(2);
                        usuario.Mail = reader.GetString(5);
                    }
                }

                return usuario;
            }
        }

        public static bool putUsuario(Usuario usuario)
        {
            string qry = "UPDATE dbo.Usuario SET " +
                         "Nombre = @Nombre, " +
                         "Apellido = @Apellido, " +
                         "NombreUsuario = @NombreUsuario, " +
                         "Contraseña = @Contraseña, " +
                         "Mail = @Mail " +
                         "WHERE Id = @Id";

            using (SqlConnection conex = new SqlConnection(connectionString))
            {
                SqlCommand comando = new SqlCommand(qry, conex);
                comando.Parameters.AddWithValue("@Nombre", usuario.Nombre);
                comando.Parameters.AddWithValue("@Apellido", usuario.Apellido);
                comando.Parameters.AddWithValue("@NombreUsuario", usuario.NombreUsuario);
                comando.Parameters.AddWithValue("@Contraseña", usuario.Contrasena);
                comando.Parameters.AddWithValue("@Mail", usuario.Mail);
                comando.Parameters.AddWithValue("@Id", usuario.Id);

                conex.Open();

                comando.ExecuteNonQuery();
            }

            return true;
        }

        public static Usuario Login(long id, string contrasena)
        {
            Usuario usuario = new Usuario();

            string qry = "SELECT * FROM dbo.Usuario WHERE id = @id AND contraseña = @contrasena";

            using (SqlConnection conex = new SqlConnection(connectionString))
            {
                SqlCommand comando = new SqlCommand(qry, conex);
                comando.Parameters.AddWithValue("@id", id);
                comando.Parameters.AddWithValue("@contrasena", contrasena);

                conex.Open();

                using (SqlDataReader reader = comando.ExecuteReader())
                {
                    if (reader.HasRows)
                    {
                        reader.Read();
                        usuario.Id = reader.GetInt64(0);
                        usuario.Nombre = reader.GetString(1);
                        usuario.Apellido = reader.GetString(2);
                        usuario.NombreUsuario = reader.GetString(3);
                        usuario.Contrasena = reader.GetString(4);
                        usuario.Mail = reader.GetString(5);
                    }
                }

                return usuario;
            }
        }
    }
}
