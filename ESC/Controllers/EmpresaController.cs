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
    public class EmpresaController : Controller
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

        // GET: Empresa
        public ActionResult Index()
        {
            
            return View(EmpresaDados.Listar());
        }

        // GET: Empresa
        public ActionResult CriarEmpresa()
        {
            return View();
        }

        [HttpPost]
        public ActionResult CriarEmpresa(Empresa empresa)
        {
            if (LoginValido() == false) { return RedirectToAction("Logar", "Usuario"); }

            //Verifica se o usuário marcou o registro como ativo
            empresa.Ativa = CadastroAtivo(Request.Form["ativo"]);

            if (ModelState.IsValid)
            {
                //Grava os dados da
                EmpresaDados.Incluir(empresa);

                return View();
            }
            else
            {
                return View(empresa);
            }
            
        }

        [HttpGet]
        public ActionResult EditarEmpresa(int idEmpresa)
        {
            Empresa empresa = new Empresa();
            empresa = EmpresaDados.RecuperarEmpresaPorId(idEmpresa);
            return View(empresa);
        }

        [HttpPost]
        public ActionResult EditarEmpresa(Empresa empresa)
        {
            //Se o login não estiver válido, encaminha para a tela do usuário
            if (LoginValido() == false) { return RedirectToAction("Logar", "Usuario"); }

            //Verifica se o usuário marcou o registro como ativo
            empresa.Ativa = CadastroAtivo(Request.Form["ativo"]);

            //Grava os dados da
            EmpresaDados.Gravar(empresa);

            return RedirectToAction("Index", "Empresa");
        }

        [HttpGet]
        public ActionResult ExcluirEmpresa(int idEmpresa)
        {
            //Se o login não estiver válido, encaminha para a tela do usuário
            if (LoginValido() == false) { return RedirectToAction("Logar", "Usuario"); }

            Empresa empresa = new Empresa();
            empresa = EmpresaDados.RecuperarEmpresaPorId(idEmpresa);
            return View(empresa);
        }

        [HttpPost]
        public ActionResult ExcluirEmpresa(Empresa empresa)
        {
            //Se o login não estiver válido, encaminha para a tela do usuário
            if (LoginValido() == false) { return RedirectToAction("Logar", "Usuario"); }

            //Grava os dados da
            EmpresaDados.Excluir(empresa);

            return RedirectToAction("Index", "Empresa");
        }
    }
}