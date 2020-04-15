﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;

namespace ControleEstoque.Web.Models
{
    public class GrupoProdutoModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Preencha o nome.")]
        public string Nome { get; set; }

        public bool Ativo { get; set; }



        public static List<GrupoProdutoModel> RecuperarLista()
        {
            var ret = new List<GrupoProdutoModel>();

            using (var conexao = new SqlConnection())
            {

                conexao.ConnectionString = ConfigurationManager.ConnectionStrings["principal"].ConnectionString;

                conexao.Open();
                using (var comando = new SqlCommand())
                {
                    comando.Connection = conexao;
                    comando.CommandText = "Select * from grupoProduto order by nome";

                    var reader = comando.ExecuteReader();
                    while (reader.Read())
                    {
                        ret.Add(new GrupoProdutoModel
                        {
                            Id = (int)reader["id"],
                            Nome = (string)reader["nome"],
                            Ativo = (bool)reader["ativo"]
                        });

                    }
                }
            }

            return ret;
        }

        public static GrupoProdutoModel RecuperarPeloId(int id)
        {
            GrupoProdutoModel ret = null;


            using (var conexao = new SqlConnection())
            {

                conexao.ConnectionString = ConfigurationManager.ConnectionStrings["principal"].ConnectionString;

                conexao.Open();
                using (var comando = new SqlCommand())
                {
                    comando.Connection = conexao;
                    comando.CommandText = "Select * from grupoProduto where (id = @Id)";

                    comando.Parameters.Add("@Id", SqlDbType.VarChar).Value = id;

                    var reader = comando.ExecuteReader();
                    if (reader.Read())
                    {
                        ret = new GrupoProdutoModel
                        {
                            Id = (int)reader["id"],
                            Nome = (string)reader["nome"],
                            Ativo = (bool)reader["ativo"]
                        };

                    }
                }
            }

            return ret;

        }

        public static bool ExcluirPeloId(int id)
        {
            var ret = false;



            if (RecuperarPeloId(id) != null)
            {
                using (var conexao = new SqlConnection())
                {

                    conexao.ConnectionString = ConfigurationManager.ConnectionStrings["principal"].ConnectionString;



                    conexao.Open();
                    using (var comando = new SqlCommand())
                    {
                        comando.Connection = conexao;
                        comando.CommandText = "delete from grupoProduto where (id = @Id)";

                        comando.Parameters.Add("@Id", SqlDbType.VarChar).Value = id;

                        ret = comando.ExecuteNonQuery() > 0;
                    }
                }
            }

            return ret;
        }

        public int Salvar()
        {
            var ret = 0;

            var model = RecuperarPeloId(this.Id);
                
            using (var conexao = new SqlConnection())
            {
                conexao.ConnectionString = ConfigurationManager.ConnectionStrings["principal"].ConnectionString;
                conexao.Open();
                using (var comando = new SqlCommand())
                {
                    comando.Connection = conexao;

                    if (model == null)
                    {
                        comando.CommandText = "insert into grupoProduto (nome, ativo) values (@Nome, @Ativo); select convert(int, scope_identity())";



                        comando.Parameters.Add("@Nome", SqlDbType.VarChar).Value = Nome;
                        comando.Parameters.Add("@Ativo", SqlDbType.VarChar).Value = Ativo ? 1 : 0;
                        


                        ret = (int)comando.ExecuteScalar();
                    }
                    else
                    {
                        comando.CommandText = "update grupoProduto set nome=@Nome, ativo=@Ativo where id = @Id";

                        comando.Parameters.Add("@Nome", SqlDbType.VarChar).Value = Nome;
                        comando.Parameters.Add("@Ativo", SqlDbType.VarChar).Value = Ativo ? 1 : 0;
                        comando.Parameters.Add("@Id", SqlDbType.Int).Value = Id;


                        if (comando.ExecuteNonQuery() > 0)
                        {
                            ret = this.Id;
                        }
                       
                    }
                }
            }

            return ret;
        }
    }

}
