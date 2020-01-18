using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using X.PagedList;
using X.PagedList.Mvc;
using System.Web.Mvc;
using ESC.Models;
using ESC.Persistencia;

namespace ESC.Controllers
{
    public class EmpresaDadoBancarioController : Controller
    {
        private Usuario usuario = new Usuario();

        public bool LoginValido()
        {
            //Recupera o usuário da Sessão
            //----------------------------------------------------------------------------------------------------------------------
            usuario = (Usuario)Session["Usuario"];

            //Verifica se o login está válido
            if (usuario == null || usuario.Ativo != true)
            {
                return false;
            }
            else
            {
                return true;
            }
               
        }

        public bool CadastroAtivo(string Ativo)
        {            
            if (Ativo == "checked" | Ativo == "on")
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public ActionResult Index()
        {
            //Se o login não estiver válido, encaminha para a tela do usuário
            if (LoginValido() == false)
            {
                return RedirectToAction("Logar", "Usuario");
            }

             return View(EmpresaDadoBancarioDados.Listar(usuario.Empresa.IdEmpresa));
            
        }

        public ActionResult Criar()
        {
            //Se o login não estiver válido, encaminha para a tela do usuário
            if (LoginValido() == false) { return RedirectToAction("Logar", "Usuario"); }


            return View();
        }

        [HttpPost]
        public ActionResult Criar(EmpresaDadoBancario DadoBancario)
        {
            
            //Se o login não estiver válido, encaminha para a tela do usuário
            if (LoginValido() == false) {return RedirectToAction("Logar", "Usuario");}

            DadoBancario.Empresa = EmpresaDados.RecuperarEmpresaPorId(usuario.Empresa.IdEmpresa);

            //Verifica se o usuário marcou o registro como ativo
            DadoBancario.Ativo = CadastroAtivo(Request.Form["ativo"]);

            DadoBancario.TipoConta = TipoContaDados.RecuperarPorDescTipoConta(Request.Form["TipoConta.DescTipoConta"]);

            if (ModelState.IsValid)
            {
                //Grava os dados da
                EmpresaDadoBancarioDados.Incluir(DadoBancario);

                return RedirectToAction("Index", "EmpresaDadoBancario");
            }
            else
            {
                return View(DadoBancario);
            }
        }

        [HttpGet]
        public ActionResult Editar(int idEmpresaDadoBancario)
        {
            if (LoginValido() == false) { return RedirectToAction("Logar", "Usuario"); }

            EmpresaDadoBancario dadobancario = new EmpresaDadoBancario();
            dadobancario = EmpresaDadoBancarioDados.RecuperarPorId(idEmpresaDadoBancario);

            return View(dadobancario);
        }

        [HttpPost]
        public ActionResult Editar(EmpresaDadoBancario DadoBancario)
        {

            //Se o login não estiver válido, encaminha para a tela do usuário
            if (LoginValido() == false) { return RedirectToAction("Logar", "Usuario"); }

            DadoBancario.Empresa = EmpresaDados.RecuperarEmpresaPorId(usuario.Empresa.IdEmpresa);
            
            //Verifica se o usuário marcou o registro como ativo
            DadoBancario.Ativo = CadastroAtivo(Request.Form["status"]);

            DadoBancario.TipoConta = TipoContaDados.RecuperarPorDescTipoConta(Request.Form["TipoConta.DescTipoConta"]);

            if (ModelState.IsValid)
            {
                //Grava os dados da
                EmpresaDadoBancarioDados.Gravar(DadoBancario);
                return RedirectToAction("Index", "EmpresaDadoBancario");
            }
            else
            {
                return View(DadoBancario);
            }
        }

        [HttpGet]
        public ActionResult Excluir(int idEmpresaDadoBancario)
        {
            if (LoginValido() == false) { return RedirectToAction("Logar", "Usuario"); }

            EmpresaDadoBancario EmpresaDadoBancario = new EmpresaDadoBancario();
            EmpresaDadoBancario = EmpresaDadoBancarioDados.RecuperarPorId(idEmpresaDadoBancario);
            return View(EmpresaDadoBancario);
        }

        [HttpPost]
        public ActionResult Excluir(EmpresaDadoBancario EmpresaDadoBancario)
        {
            if (LoginValido() == false) { return RedirectToAction("Logar", "Usuario"); }

            EmpresaDadoBancarioDados.Excluir(EmpresaDadoBancario);

            return RedirectToAction("Index", "Empresa");
        }

    }
}