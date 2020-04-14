﻿using ControleEstoque.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ControleEstoque.Web.Controllers
{
    public class CadastroController : Controller
    {


        [Authorize]
        public ActionResult GrupoProduto()
        {
            return View(GrupoProdutoModel.RecuperarLista());
        }



        //[Authorize]
        //public ActionResult RecuperarGrupoProduto()
        //{
        //    return View(_listaGrupoProduto);
        //}


        [HttpPost]
        [Authorize]
        public ActionResult RecuperarGrupoProduto(int id)
        {
            return Json(GrupoProdutoModel.RecuperarPeloId(id));
        }

        [HttpPost]
        [Authorize]
        public ActionResult ExcluirGrupoProduto(int id)
        {
                       
            return Json(GrupoProdutoModel.ExcluirPeloId(id));
        }

        [HttpPost]
        [Authorize]
        public ActionResult SalvarGrupoProduto(GrupoProdutoModel model)
        {

            var resultado = "OK";
            var mensagens = new List<string>();
            var idsalvo = string.Empty;

            if (!ModelState.IsValid)
            {
                resultado = "AVISO";

                mensagens = ModelState.Values.SelectMany(x => x.Errors).Select(x => x.ErrorMessage).ToList();
            }
            else
            {
                try
                {
                    var id = model.Salvar();

                    if (id > 0)
                    {
                        idsalvo = id.ToString();
                    }
                    else
                    {
                        resultado = "ERRO";
                    }
                   
                }
                catch (Exception ex)
                {

                    resultado = "ERRO";
                }

            }
            return Json(new { Resultado = resultado, Mensagens = mensagens, IdSalvo = idsalvo });
        }




        [Authorize]
        public ActionResult MarcaProduto()
        {
            return View();
        }

        [Authorize]
        public ActionResult LocalProduto()
        {
            return View();
        }

        [Authorize]
        public ActionResult UnidadeMedida()
        {
            return View();
        }

        [Authorize]
        public ActionResult Produto()
        {
            return View();
        }

        [Authorize]
        public ActionResult Pais()
        {
            return View();
        }

        [Authorize]
        public ActionResult Estado()
        {
            return View();
        }

        [Authorize]
        public ActionResult Cidade()
        {
            return View();
        }

        [Authorize]
        public ActionResult Fornecedor()
        {
            return View();
        }

        [Authorize]
        public ActionResult PerfilUsuario()
        {
            return View();
        }

        [Authorize]
        public ActionResult Usuario()
        {
            return View();
        }
    }
}