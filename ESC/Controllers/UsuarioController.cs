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
    public class UsuarioController : Controller
    {
        private string Classe = "USUARIO";
        
        public ActionResult Index()
        {
            ViewBag.MensagemErro = "";
            ViewBag.MensagemSenha = "";
            return View();
        }

        public ActionResult CadastrarUsuario()
        {
            ViewBag.MensagemErro = "";
            ViewBag.MensagemSenha = "";
            return View();
        }

        [HttpPost]
        public ActionResult CadastrarUsuario(Usuario usuario)
        {

            usuario.CNPJEmpresa = String.Join("", System.Text.RegularExpressions.Regex.Split(usuario.CNPJEmpresa, @"[^\d]"));

            //Senha Inválida!
            //----------------------------------------------------------------------------------
            if (usuario.Senha != Request.Form["ConfirmarSenha"])
            {

                usuario.Senha = "";
                ViewBag.IdEmpresa = usuario.Empresa.IdEmpresa; ViewBag.NomeEmpresa = usuario.Empresa.NomeFantasia; ViewBag.NomeUsuario = usuario.Login; ViewBag.CNPJEmpresa = "CNPJ: " + @Convert.ToUInt64(usuario.Empresa.CNPJ).ToString(@"00\.000\.000\/0000\-00");
                ViewBag.MensagemErro = "";
                ViewBag.MensagemSenha = "A senha não confere!";
                return View(usuario);

            }
            //----------------------------------------------------------------------------------


            //Verifica se o usuário já está cadastrado
            //----------------------------------------------------------------------------------
            usuario.Empresa = EmpresaDados.CarregaEmpresasPorCNPJ(usuario.CNPJEmpresa);

            bool Retorno = UsuarioDados.UsuarioJaExiste(usuario.Nome, usuario.Login);
            if (Retorno == true)
            {
                ViewBag.IdEmpresa = usuario.Empresa.IdEmpresa; ViewBag.NomeEmpresa = usuario.Empresa.NomeFantasia; ViewBag.NomeUsuario = usuario.Login; ViewBag.CNPJEmpresa = "CNPJ: " + @Convert.ToUInt64(usuario.Empresa.CNPJ).ToString(@"00\.000\.000\/0000\-00");
                ViewBag.MensagemErro = "";
                ViewBag.MensagemSenha = "Usuário já cadastrado.";
                return View(usuario);

            }
            //----------------------------------------------------------------------------------


            try
            {
                //Gravar o usuario
                //----------------------------------------------------------------------------------
                UsuarioDados.Gravar(usuario);

                return RedirectToAction("logar", "usuario");

                //----------------------------------------------------------------------------------
            }
            catch (Exception erro)
            {
                ViewBag.IdEmpresa = 0;
                ViewBag.MensagemErro = "Erro ao Cadastrar usuário";
                ViewBag.erro = erro.ToString();
                ViewBag.MensagemSenha = "";
                //Em caso de erro retorna a mensagem de erro
                return View(usuario);
            }

        }

        public ActionResult Logar()
        {
            ViewBag.usuario = "";
            ViewBag.MensagemErro = "";
            ViewBag.MensagemSenha = "";
            return View();
        }

        [HttpPost]
        public ActionResult Logar(Usuario usuario)
        {
            ViewBag.usuario = "";
            ViewBag.MensagemErro = "";
            ViewBag.MensagemSenha = "";

            try
            {

                string usuarionome = Request.Form["nomeusuario"];
                string usuariosenha = Request.Form["senha"];

                usuarionome = "Usuario";
                usuariosenha = "senha";

                ViewBag.usuario = usuarionome;
                
                string validacao = "";

                validacao = UsuarioDados.Logar(usuarionome, usuariosenha);

                //Se a validação não retornou nenhuma crítica, o usuário está autorizado
                //----------------------------------------------------------------------------------
                if (validacao == "")
                {

                    Usuario usuariobase = UsuarioDados.CarregaUsuariosPorLoginUsuario(usuarionome);

                    ////Passa as informações do usuário para o login
                    ////----------------------------------------------------------------------------------
                    Session["idUsuario"] = usuariobase.idUsuario;
                    Session["Nome"] = usuariobase.Nome;
                    Session["Login"] = usuariobase.Login.ToUpper();
                    Session["IdEmpresa"] = usuariobase.Empresa.IdEmpresa;
                    Session["NomeEmpresa"] = usuariobase.NomeEmpresa;
                    Session["CNPJEmpresa"] = usuariobase.CNPJEmpresa;
                    Session["HoraLogon"] = System.DateTime.Now;
                    Session["Usuario"] = usuariobase;

                    //----------------------------------------------------------------------------------

                    return RedirectToAction("index", "Empresa");
                }
                else
                {
                    ViewBag.MensagemErro = validacao;
                    ViewBag.MensagemSenha = "";
                    return View(usuario);
                }

            }
            catch (Exception erro)
            {
                ViewBag.MensagemErro = erro.ToString();//Em caso de erro retorna a mensagem de erro
                ViewBag.MensagemSenha = "";

                return View(usuario);
            }

        }

        public ActionResult AlterarSenha()
        {
            Usuario usuario;

            //Verifica se o usuário está ativo! Caso não esteja, vai para a tela de login
            //----------------------------------------------------------------------------------------------------------------------
            usuario = (Usuario)Session["Usuario"];

            if (usuario == null || usuario.Ativo != true)
            {
                return RedirectToAction("Logar", "Usuario");
            }
            //----------------------------------------------------------------------------------------------------------------------
            ViewBag.IdEmpresa = usuario.Empresa.IdEmpresa; ViewBag.NomeEmpresa = usuario.Empresa.NomeFantasia; ViewBag.NomeUsuario = usuario.Login; ViewBag.CNPJEmpresa = "CNPJ: " + @Convert.ToUInt64(usuario.Empresa.CNPJ).ToString(@"00\.000\.000\/0000\-00");
            ViewBag.SenhaAnterior = usuario.Senha;
            ViewBag.Classe = Classe;
            ViewBag.Operacao = "SELECIONADO";
            ViewBag.Resultado = "SUCESSO";
            ViewBag.Mensagem = "";
            @ViewBag.MensagemErro = "";
            @ViewBag.MensagemSenha = "";

            return View();

        }

        [HttpPost]
        public ActionResult AlterarSenha(Usuario User)
        {
            Usuario usuario;

            //Verifica se o usuário está ativo! Caso não esteja, vai para a tela de login
            //----------------------------------------------------------------------------------------------------------------------
            usuario = (Usuario)Session["Usuario"];

            if (usuario == null || usuario.Ativo != true)
            {
                return RedirectToAction("Logar", "Usuario");
            }
            //----------------------------------------------------------------------------------------------------------------------
            ViewBag.IdEmpresa = usuario.Empresa.IdEmpresa; ViewBag.NomeEmpresa = usuario.Empresa.NomeFantasia; ViewBag.NomeUsuario = usuario.Login; ViewBag.CNPJEmpresa = "CNPJ: " + @Convert.ToUInt64(usuario.Empresa.CNPJ).ToString(@"00\.000\.000\/0000\-00");


            //Senha Inválida!
            //----------------------------------------------------------------------------------
            if (Request.Form["Senha"] != Request.Form["ConfirmarSenha"])
            {
                usuario.Senha = "";
                ViewBag.MensagemErro = "";
                ViewBag.MensagemSenha = "A senha não confere!";
                return View(usuario);

            }
            //----------------------------------------------------------------------------------

            bool Retorno = UsuarioDados.ConfereSenhaAtual(usuario);
            if (Retorno == false)
            {
                ViewBag.MensagemErro = "";
                ViewBag.MensagemSenha = "A Senha já foi alterada por outra sessão.";
                return View(usuario);

            }
            //----------------------------------------------------------------------------------

            usuario.Senha = User.Senha;
            usuario.ConfirmarSenha = User.ConfirmarSenha;

            try
            {
                //Gravar o usuario
                //----------------------------------------------------------------------------------
                UsuarioDados.AlterarSenha(usuario);

                return RedirectToAction("Index", "Home");

                //----------------------------------------------------------------------------------
            }
            catch (Exception erro)
            {
                ViewBag.MensagemErro = erro;
                ViewBag.MensagemSenha = "";
                //Em caso de erro retorna a mensagem de erro
                return View(usuario);
            }

        }

        public ActionResult GerenciarUsuarios()
        {
            Usuario usuario;

            @ViewBag.Mensagem = "";
            @ViewBag.MensagemSucesso = "";

            //Verifica se o usuário está ativo! Caso não esteja, vai para a tela de login
            //----------------------------------------------------------------------------------------------------------------------
            usuario = (Usuario)Session["Usuario"];

            if (usuario == null || usuario.Ativo != true)
            {
                return RedirectToAction("Logar", "Usuario");
            }
            //----------------------------------------------------------------------------------------------------------------------
            ViewBag.IdEmpresa = usuario.Empresa.IdEmpresa; ViewBag.NomeEmpresa = usuario.Empresa.NomeFantasia; ViewBag.NomeUsuario = usuario.Login; ViewBag.CNPJEmpresa = "CNPJ: " + @Convert.ToUInt64(usuario.Empresa.CNPJ).ToString(@"00\.000\.000\/0000\-00");


            if (Session["MensagemErroAlteracaoCadastro"] != null)
            {
                if (Session["MensagemErroAlteracaoCadastro"].ToString() != "")
                {
                    @ViewBag.Mensagem = Session["MensagemErroAlteracaoCadastro"];
                    Session["MensagemErroAlteracaoCadastro"] = "";
                }
            };

            if (Session["MensagemSucessoSenha"] != null)
            {
                if (Session["MensagemSucessoSenha"].ToString() != "")
                {
                    @ViewBag.MensagemSucesso = Session["MensagemSucessoSenha"];
                    Session["MensagemSucessoSenha"] = "";
                }
            };


            ViewBag.SenhaAnterior = usuario.Senha;
            ViewBag.Classe = Classe;

            var Lista = UsuarioDados.CarregarUsuarios(usuario).ToPagedList(1, 1000);

            return View(Lista);


        }

        public ActionResult AtivarDesativarUsuario(int UsuarioAlterar)
        {
            Session["MensagemErroAlteracaoCadastro"] = "";

            Usuario usuario;

            //Verifica se o usuário está ativo! Caso não esteja, vai para a tela de login
            //----------------------------------------------------------------------------------------------------------------------
            usuario = (Usuario)Session["Usuario"];

            if (usuario == null || usuario.Ativo != true)
            {
                return RedirectToAction("Logar", "Usuario");
            }
            //----------------------------------------------------------------------------------------------------------------------
            ViewBag.IdEmpresa = usuario.Empresa.IdEmpresa; ViewBag.NomeEmpresa = usuario.Empresa.NomeFantasia; ViewBag.NomeUsuario = usuario.Login; ViewBag.CNPJEmpresa = "CNPJ: " + @Convert.ToUInt64(usuario.Empresa.CNPJ).ToString(@"00\.000\.000\/0000\-00");

            //Funcionalidade Disponível apenas para Administradores
            if (usuario.Administrador == false)
            {
                Session["MensagemErroAlteracaoCadastro"] = "ALTERAÇÕES PERMITIDAS APENAS PARA ADMINISTRADORES";

                return RedirectToAction("GerenciarUsuarios", "Usuario");
            }

            try
            {
                //Gravar o usuario
                //----------------------------------------------------------------------------------
                UsuarioDados.AtivarDesativarUsuario(usuario, UsuarioAlterar);

                return RedirectToAction("GerenciarUsuarios", "Usuario");
                //----------------------------------------------------------------------------------

            }
            catch (Exception erro)
            {
                ViewBag.MensagemErro = erro;
                ViewBag.MensagemSenha = "";
                //Em caso de erro retorna a mensagem de erro
                //return View(usuario);

                return RedirectToAction("GerenciarUsuarios", "Usuario");
            }


        }

        public ActionResult AtivarDesativarAdministrador(int UsuarioAlterar)
        {
            //int UsuarioAlterar = 1;

            Usuario usuario;

            Session["MensagemErroAlteracaoCadastro"] = "";

            //Verifica se o usuário está ativo! Caso não esteja, vai para a tela de login
            //----------------------------------------------------------------------------------------------------------------------
            usuario = (Usuario)Session["Usuario"];

            if (usuario == null || usuario.Ativo != true)
            {
                return RedirectToAction("Logar", "Usuario");
            }
            //----------------------------------------------------------------------------------------------------------------------
            ViewBag.IdEmpresa = usuario.Empresa.IdEmpresa; ViewBag.NomeEmpresa = usuario.Empresa.NomeFantasia; ViewBag.NomeUsuario = usuario.Login; ViewBag.CNPJEmpresa = "CNPJ: " + @Convert.ToUInt64(usuario.Empresa.CNPJ).ToString(@"00\.000\.000\/0000\-00");

            //Funcionalidade Disponível apenas para Administradores
            if (usuario.Administrador == false)
            {
                Session["MensagemErroAlteracaoCadastro"] = "ALTERAÇÕES PERMITIDAS APENAS PARA ADMINISTRADORES";

                return RedirectToAction("GerenciarUsuarios", "Usuario");
            }

            try
            {
                //Gravar o usuario
                //----------------------------------------------------------------------------------
                UsuarioDados.AtivarDesativarAdministrador(usuario, UsuarioAlterar);

                return RedirectToAction("GerenciarUsuarios", "Usuario");
                //----------------------------------------------------------------------------------

            }
            catch (Exception erro)
            {
                ViewBag.MensagemErro = erro;
                ViewBag.MensagemSenha = "";
                //Em caso de erro retorna a mensagem de erro
                //return View(usuario);

                return RedirectToAction("GerenciarUsuarios", "Usuario");
            }


        }

        public ActionResult AutorizarUsuario(int UsuarioAlterar)
        {
            //int UsuarioAlterar = 1;

            Usuario usuario;

            Session["MensagemErroAlteracaoCadastro"] = "";

            //Verifica se o usuário está ativo! Caso não esteja, vai para a tela de login
            //----------------------------------------------------------------------------------------------------------------------
            usuario = (Usuario)Session["Usuario"];

            if (usuario == null || usuario.Ativo != true)
            {
                return RedirectToAction("Logar", "Usuario");
            }
            //----------------------------------------------------------------------------------------------------------------------
            ViewBag.IdEmpresa = usuario.Empresa.IdEmpresa; ViewBag.NomeEmpresa = usuario.Empresa.NomeFantasia; ViewBag.NomeUsuario = usuario.Login; ViewBag.CNPJEmpresa = "CNPJ: " + @Convert.ToUInt64(usuario.Empresa.CNPJ).ToString(@"00\.000\.000\/0000\-00");

            //Funcionalidade Disponível apenas para Administradores
            if (usuario.Administrador == false)
            {
                Session["MensagemErroAlteracaoCadastro"] = "ALTERAÇÕES PERMITIDAS APENAS PARA ADMINISTRADORES";

                return RedirectToAction("GerenciarUsuarios", "Usuario");
            }

            try
            {
                //Gravar o usuario
                //----------------------------------------------------------------------------------
                UsuarioDados.AutorizarUso(usuario, UsuarioAlterar);

                return RedirectToAction("GerenciarUsuarios", "Usuario");
                //----------------------------------------------------------------------------------

            }
            catch (Exception erro)
            {
                ViewBag.MensagemErro = erro;
                ViewBag.MensagemSenha = "";
                //Em caso de erro retorna a mensagem de erro
                //return View(usuario);

                return RedirectToAction("GerenciarUsuarios", "Usuario");
            }


        }

        public ActionResult RenovarSenha(int UsuarioAlterar)
        {
            //int UsuarioAlterar = 1;

            Usuario usuario;

            Session["MensagemErroAlteracaoCadastro"] = "";

            //Verifica se o usuário está ativo! Caso não esteja, vai para a tela de login
            //----------------------------------------------------------------------------------------------------------------------
            usuario = (Usuario)Session["Usuario"];

            if (usuario == null || usuario.Ativo != true)
            {
                return RedirectToAction("Logar", "Usuario");
            }
            //----------------------------------------------------------------------------------------------------------------------
            ViewBag.IdEmpresa = usuario.Empresa.IdEmpresa; ViewBag.NomeEmpresa = usuario.Empresa.NomeFantasia; ViewBag.NomeUsuario = usuario.Login; ViewBag.CNPJEmpresa = "CNPJ: " + @Convert.ToUInt64(usuario.Empresa.CNPJ).ToString(@"00\.000\.000\/0000\-00");

            //Funcionalidade Disponível apenas para Administradores
            if (usuario.Administrador == false)
            {
                Session["MensagemErroAlteracaoCadastro"] = "ALTERAÇÕES PERMITIDAS APENAS PARA ADMINISTRADORES";

                return RedirectToAction("GerenciarUsuarios", "Usuario");
            }

            try
            {
                //Gravar o usuario
                //----------------------------------------------------------------------------------
                UsuarioDados.RenovarSenha(usuario, UsuarioAlterar);

                Session["MensagemSucessoSenha"] = "A SENHA FOI ALTERADA PARA 123456";

                return RedirectToAction("GerenciarUsuarios", "Usuario");
                //----------------------------------------------------------------------------------

            }
            catch (Exception erro)
            {
                ViewBag.MensagemErro = erro;
                ViewBag.MensagemSenha = "";
                //Em caso de erro retorna a mensagem de erro
                //return View(usuario);

                return RedirectToAction("GerenciarUsuarios", "Usuario");
            }


        }

        public ActionResult EsqueciASenha()
        {
            ViewBag.MensagemErro = "";
            ViewBag.MensagemSenha = "";
            return View();

        }

        [HttpPost]
        public ActionResult EsqueciASenha(Usuario User)
        {
            Usuario usuario;

            //Senha Inválida!
            //----------------------------------------------------------------------------------
            if (Request.Form["Senha"] != Request.Form["ConfirmarSenha"])
            {
                User.Senha = "";
                ViewBag.MensagemErro = "";
                ViewBag.MensagemSenha = "A senha não confere!";
                return View(User);

            }
            //----------------------------------------------------------------------------------


            //Verifica se o usuário existe
            //----------------------------------------------------------------------------------
            usuario = UsuarioDados.CarregaUsuariosPorLoginDocumento(User.Login, User.CPF);

            if (usuario.idUsuario == 0)
            {
                User.Senha = "";
                ViewBag.MensagemErro = "";
                ViewBag.MensagemSenha = "Usuário não Encontrado! Verifique o Login e o Documento informado!";
                return View(User);
            }
            //----------------------------------------------------------------------------------

            //Atualiza as informações
            //----------------------------------------------------------------------------------
            usuario.Senha = User.Senha;
            usuario.ConfirmarSenha = User.ConfirmarSenha;
            //----------------------------------------------------------------------------------

            //Grava o usuário e aponta para a tela de login
            //----------------------------------------------------------------------------------
            try
            {
                //Gravar o usuario
                //----------------------------------------------------------------------------------
                UsuarioDados.AlterarSenha(usuario);

                return RedirectToAction("Index", "Home");

                //----------------------------------------------------------------------------------
            }
            catch (Exception erro)
            {
                ViewBag.MensagemErro = erro;
                ViewBag.MensagemSenha = "";
                //Em caso de erro retorna a mensagem de erro
                return View(usuario);
            }


        }

        public ActionResult DadosCadastrais()
        {
            Usuario usuario;

            @ViewBag.Mensagem = "";
            @ViewBag.MensagemSucesso = "";

            //Verifica se o usuário está ativo! Caso não esteja, vai para a tela de login
            //----------------------------------------------------------------------------------------------------------------------
            usuario = (Usuario)Session["Usuario"];

            if (usuario == null || usuario.Ativo != true)
            {
                return RedirectToAction("Logar", "Usuario");
            }
            //----------------------------------------------------------------------------------------------------------------------
            ViewBag.IdEmpresa = usuario.Empresa.IdEmpresa; ViewBag.NomeEmpresa = usuario.Empresa.NomeFantasia; ViewBag.NomeUsuario = usuario.Login; ViewBag.CNPJEmpresa = "CNPJ: " + @Convert.ToUInt64(usuario.Empresa.CNPJ).ToString(@"00\.000\.000\/0000\-00");


            ViewBag.Mensagem = "";
            ViewBag.MensagemErro = "";
            ViewBag.MensagemSenha = "";

            return View(usuario);

        }

        [HttpPost]
        public ActionResult DadosCadastrais(Usuario User)
        {
            Usuario usuario;

            @ViewBag.Mensagem = "";
            @ViewBag.MensagemSucesso = "";

            //Verifica se o usuário está ativo! Caso não esteja, vai para a tela de login
            //----------------------------------------------------------------------------------------------------------------------
            usuario = (Usuario)Session["Usuario"];

            if (usuario == null || usuario.Ativo != true)
            {
                return RedirectToAction("Logar", "Usuario");
            }
            //----------------------------------------------------------------------------------------------------------------------
            ViewBag.IdEmpresa = usuario.Empresa.IdEmpresa; ViewBag.NomeEmpresa = usuario.Empresa.NomeFantasia; ViewBag.NomeUsuario = usuario.Login; ViewBag.CNPJEmpresa = "CNPJ: " + @Convert.ToUInt64(usuario.Empresa.CNPJ).ToString(@"00\.000\.000\/0000\-00");

            //Verifica se o usuário já está cadastrado
            //----------------------------------------------------------------------------------
            usuario.Empresa = EmpresaDados.CarregaEmpresasPorCNPJ(usuario.CNPJEmpresa);

            bool Retorno = UsuarioDados.UsuarioJaExisteEdicaoCadastral(usuario, User);
            if (Retorno == true)
            {
                ViewBag.IdEmpresa = usuario.Empresa.IdEmpresa; ViewBag.NomeEmpresa = usuario.Empresa.NomeFantasia; ViewBag.NomeUsuario = usuario.Login; ViewBag.CNPJEmpresa = "CNPJ: " + @Convert.ToUInt64(usuario.Empresa.CNPJ).ToString(@"00\.000\.000\/0000\-00");
                ViewBag.MensagemErro = "";
                ViewBag.MensagemSenha = "Usuário já cadastrado.";
                return View(User);

            }
            else
            {
                //----------------------------------------------------------------------------------

                UsuarioDados.AlterarDadosCadastrais(User);

                @ViewBag.Mensagem = "Dados Alterados com Sucesso!";
                @ViewBag.MensagemSucesso = "Dados Alterados com Sucesso!";

                return View(User);

            }

        }

        public ActionResult FaleConosco()
        {
            return RedirectToAction("index", "FaleConosco");
        }

    }
}