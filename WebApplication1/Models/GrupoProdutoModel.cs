using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace WebApplication1.Models
{
    public class GrupoProdutoModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Preencha o Nome.")]
        public string Nome { get; set; }

        public bool Ativo { get; set; }


        public static List<GrupoProdutoModel> RecuperarLista()
        {
            var ret = new List<GrupoProdutoModel>();
            using (var conexao = new SqlConnection())
            {
                conexao.ConnectionString = ConfigurationManager.ConnectionStrings["controle-estoque"].ConnectionString;
                conexao.Open();
                using (var comando = new SqlCommand())
                {
                    comando.Connection = conexao;
                    comando.CommandText = "select * from GrupoProduto order by nome";
                    var reader = comando.ExecuteReader();
                    while (reader.Read())
                    {
                        ret.Add(new GrupoProdutoModel
                        {
                            Id = (Int32)reader["id"],
                            Nome = (string)reader["nome"],
                            Ativo = (bool)reader["ativo"]
                        });
                    }
                }
                conexao.Close();
            }
            return ret;
        }


        public static GrupoProdutoModel RecuperarGrupoProduto(int id)
        {
            GrupoProdutoModel ret = null;
            using (var conexao = new SqlConnection())
            {
                conexao.ConnectionString = ConfigurationManager.ConnectionStrings["controle-estoque"].ConnectionString;
                conexao.Open();
                using (var comando = new SqlCommand())
                {
                    comando.Connection = conexao;
                    comando.CommandText = string.Format("select * from GrupoProduto where ( id = {0} ) order by nome", id);
                    var reader = comando.ExecuteReader();
                    if (reader.Read())
                    {

                        ret = new GrupoProdutoModel
                        {
                            Id = (Int32)reader["id"],
                            Nome = (string)reader["nome"],
                            Ativo = (bool)reader["ativo"]
                        };
                    }
                }
                conexao.Close();
            }
            return ret;
        }

        public static Boolean Existe(int id)
        {
            GrupoProdutoModel grupoProduto = RecuperarGrupoProduto(id);
            return grupoProduto != null;
        }

        public static Boolean ExcluirGrupoProduto(int id)
        {

            var ret = false;
            if (Existe(id))
            {
                using (var conexao = new SqlConnection())
                {
                    conexao.ConnectionString = ConfigurationManager.ConnectionStrings["controle-estoque"].ConnectionString;
                    conexao.Open();
                    using (var comando = new SqlCommand())
                    {
                        comando.Connection = conexao;
                        comando.CommandText = string.Format("delete  from GrupoProduto where ( id = {0} )", id);
                        ret = (int)comando.ExecuteNonQuery() > 0;
                    }
                    conexao.Close();
                }
            }


            return ret;
        }



        public int Salvar()
        {

            var ret = 0;

            using (var conexao = new SqlConnection())
            {
                conexao.ConnectionString = ConfigurationManager.ConnectionStrings["controle-estoque"].ConnectionString;
                conexao.Open();
                using (var comando = new SqlCommand())
                {
                    comando.Connection = conexao;
                    if (!Existe(this.Id))
                    {
                        comando.CommandText = string.Format("insert into GrupoProduto (nome, ativo) values('{0}', {1}); select convert(int,scope_identity())", this.Nome, Ativo ? 1 : 0);
                        ret = (int)comando.ExecuteScalar();
                    }
                    else
                    {
                        comando.CommandText = string.Format("update GrupoProduto set nome= '{1}', ativo={2} where id = {0}", this.Id, this.Nome, this.Ativo ? 1 : 0);
                        if (comando.ExecuteNonQuery() > 0)
                        {
                            return this.Id;
                        }
                    }
                }
                conexao.Close();
            }


            return ret;
        }
    }
}