using LoginWeb_CSharp.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace LoginWeb_CSharp.Data
{
    public class UsuarioDAL
    {
        public Usuario ValidarLogin(string username, string password)
        {
            string connString = ConfigurationManager.ConnectionStrings["DB_Connection"].ConnectionString;
            Usuario user = null;

            using (SqlConnection conn = new SqlConnection(connString))
            {
                SqlCommand cmd = new SqlCommand("sp_ValidarUsuario", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Username", username);
                cmd.Parameters.AddWithValue("@Password", password);

                try
                {
                    conn.Open();
                    SqlDataReader reader = cmd.ExecuteReader();
                    if (reader.Read())
                    {
                        user = new Usuario
                        {
                            Id = (int)reader["Id"],
                            Username = reader["Username"].ToString(),
                            NombreCompleto = reader["NombreCompleto"].ToString()
                        };
                    }
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message);
                }
            }
            return user;
        }
    }
}