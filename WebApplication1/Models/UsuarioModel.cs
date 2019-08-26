using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using WebApplication1.Helpers;

namespace WebApplication1.Models
{
    public class UsuarioModel
    {
        public static bool ValidarUsuario(string usuario, string senha)
        {
            using (var conexao = new SqlConnection())
            {
                conexao.ConnectionString = ConfigurationManager.ConnectionStrings["controle-estoque"].ConnectionString;
                conexao.Open();
                using (var comando = new SqlCommand())
                {
                    comando.Connection = conexao;
                    comando.CommandText = string.Format("select count(*) from Usuario where login='{0}' and senha='{1}'", usuario, Criptografia.HashMD5(senha));
                    return ((int)comando.ExecuteScalar() > 0);
                }
            }
            return false;
        }
    }
}