using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.Common;
using ESC.Models;
using System.Data.SqlClient;

namespace ESC.Persistencia
{
    public static class UsuarioDados
    {
        private static List<Usuario> usuarios;

        public static List<Usuario> Usuarios
        {
            get
            {
                CarregarUsuarios();

                return usuarios;
            }
        }

        public static List<Usuario> Listar()
        {

            Conexao conexao = new Conexao();

            try
            {
                //Cria uma lista de Usuarios
                List<Usuario> ListaUsuario = new List<Usuario>();

                //Comando SQL para listar todas as empresas
                string ComandoSQL = "SELECT * FROM USUARIO";

                //Executa a consulta
                DbDataReader Dados = conexao.ExecutarConsulta(ComandoSQL);

                //Usuario empresa = new Usuario();

                //Se existir o objeto, cria o objeto e atribui os valores
                while (Dados.Read())
                {
                    Usuario Usuario = new Usuario();

                    Usuario.idUsuario = Convert.ToInt32(Dados["IdUsuario"]);
                    Usuario.Empresa= EmpresaDados.RecuperarEmpresaPorId( Convert.ToInt32(Dados["IdEmpresa"]));
                    Usuario.Nome = Convert.ToString(Dados["Nome"]);
                    Usuario.CPF = Convert.ToString(Dados["CPF"]);
                    Usuario.Login = Convert.ToString(Dados["Login"]);
                    Usuario.Senha = Convert.ToString(Dados["Senha"]);
                    Usuario.DataCadastro = Convert.ToDateTime(Dados["DataCadastro"]);
                    Usuario.AutorizadorPor = Convert.ToString(Dados["AutorizadorPor"]);
                    Usuario.DataAutorizacao = Convert.ToDateTime(Dados["DataAutorizacao"]);
                    Usuario.Ativo = Convert.ToBoolean(Dados["Ativo"]);
                    Usuario.Administrador = Convert.ToBoolean(Dados["Administrador"]);
                    
                    ListaUsuario.Add(Usuario);
                }

                Dados.Close();

                return ListaUsuario;
            }
            catch (Exception erro)
            {
                //Em caso de erro retorna a mensagem de erro
                throw new Exception("Erro ao Recuperar a Usuario " + erro.Message);
            }
            finally
            {
                //Fecha a Conexão --Independentemente se der erro ou não, a conexão será fechada!
                conexao.FecharConexao();
            }

        }

        public static Usuario RecuperarUsuarioPorId(int idUsuario)
        {
            Conexao conexao = new Conexao();

            try
            {
                conexao.AbrirConexao();

                string ComandoSQL = "SELECT * FROM USUARIO WHERE IDUSUARIO = @VARIAVEL1;";

                conexao.Cmd = new SqlCommand(ComandoSQL, conexao.Con);

                //Define os parametros utilizado no SQL
                conexao.Cmd.Parameters.AddWithValue("@VARIAVEL1", idUsuario);

                Usuario usuario = new Usuario();

                DbDataReader Dados = conexao.ExecutarComando(conexao.Cmd);

                if (Dados.Read())
                {

                    usuario.idUsuario = Convert.ToInt32(Dados["IdUsuario"]);
                    usuario.Empresa = EmpresaDados.RecuperarEmpresaPorId(Convert.ToInt32(Dados["IdEmpresa"]));
                    usuario.Nome = Convert.ToString(Dados["Nome"]);
                    usuario.CPF = Convert.ToString(Dados["CPF"]);
                    usuario.Login = Convert.ToString(Dados["Login"]);
                    usuario.Senha = Convert.ToString(Dados["Senha"]);
                    usuario.DataCadastro = Convert.ToDateTime(Dados["DataCadastro"]);
                    usuario.AutorizadorPor = Convert.ToString(Dados["AutorizadorPor"]);
                    usuario.DataAutorizacao = Convert.ToDateTime(Dados["DataAutorizacao"]);
                    usuario.Ativo = Convert.ToBoolean(Dados["Ativo"]);
                    usuario.Administrador = Convert.ToBoolean(Dados["Administrador"]);

                }

                Dados.Close();

                return usuario;
            }
            catch (Exception erro)
            {
                //Em caso de erro retorna a mensagem de erro
                throw new Exception("Erro ao Recuperar a Usuario " + erro.Message);
            }
            finally
            {
                //Fecha a Conexão --Independentemente se der erro ou não, a conexão será fechada!
                conexao.FecharConexao();
            }
            
        }

        private static void CarregarUsuarios()
        {
            Conexao conexao = new Conexao();
            conexao.AbrirConexao();
            string ComandoSQL = "SELECT * FROM USUARIO";
            conexao.Cmd = new SqlCommand(ComandoSQL, conexao.Con);
            DbDataReader Dados = conexao.ExecutarComando(conexao.Cmd);

            Usuario usuario = new Usuario();

            //Se existir o objeto, cria o objeto e atribui os valores
            while (Dados.Read())
            {

                usuario = new Usuario();

                usuario.idUsuario = Convert.ToInt32(Dados["IdUsuario"]);
                usuario.Empresa = EmpresaDados.RecuperarEmpresaPorId(Convert.ToInt32(Dados["IdEmpresa"]));
                usuario.Nome = Convert.ToString(Dados["Nome"]);
                usuario.CPF = Convert.ToString(Dados["CPF"]);
                usuario.Login = Convert.ToString(Dados["Login"]);
                usuario.Senha = Convert.ToString(Dados["Senha"]);
                usuario.DataCadastro = Convert.ToDateTime(Dados["DataCadastro"]);
                usuario.AutorizadorPor = Convert.ToString(Dados["AutorizadorPor"]);
                usuario.DataAutorizacao = Convert.ToDateTime(Dados["DataAutorizacao"]);
                usuario.Ativo = Convert.ToBoolean(Dados["Ativo"]);
                usuario.Administrador = Convert.ToBoolean(Dados["Administrador"]);


                if (Dados["AutorizadorPor"] != DBNull.Value)
                {
                    usuario.AutorizadorPor = Convert.ToString(Dados["AutorizadorPor"]);
                }

                if (Dados["DataAutorizacao"] != DBNull.Value)
                {
                    usuario.DataAutorizacao = Convert.ToDateTime(Dados["DataAutorizacao"]);
                }

                //Adiciona o TipoVeiculo à relação dos Tipos de Veiculos
                usuarios.Add(usuario);
            }

            conexao.FecharConexao();

        }

        public static List<string> CarregaUsuariosAtivos()
        {

            var Lista = new List<string>();
            string usuario = "";

            //Define o comando SQL
            String ComandoSQL = "SELECT * FROM USUARIO WHERE ATIVO = 1 ORDER BY LOGIN";
            Conexao conexao = new Conexao();
            conexao.AbrirConexao();
            conexao.Cmd = new SqlCommand(ComandoSQL, conexao.Con);
            DbDataReader Dados = conexao.ExecutarComando(conexao.Cmd);

            //Se existir o objeto, cria o objeto e atribui os valores
            while (Dados.Read())
            {

                usuario = Convert.ToString(Dados["LOGIN"]);

                //Adiciona o Usuario à lista de retornos
                Lista.Add(usuario);
            }

            conexao.FecharConexao();

            return Lista;
        }

        public static List<Usuario> CarregaUsuariosPorLogin(string Parametro)
        {

            usuarios = new List<Usuario>();

            //Define o comando SQL
            String ComandoSQL = "SELECT * FROM USUARIO WHERE LOGIN LIKE '%" + Parametro + "%' ORDER BY LOGIN";
            Conexao conexao = new Conexao();
            conexao.AbrirConexao();
            conexao.Cmd = new SqlCommand(ComandoSQL, conexao.Con);
            DbDataReader Dados = conexao.ExecutarComando(conexao.Cmd);

            Usuario usuario = new Usuario();

            //Se existir o objeto, cria o objeto e atribui os valores
            while (Dados.Read())
            {

                usuario = new Usuario();

                usuario.idUsuario = Convert.ToInt32(Dados["IdUsuario"]);
                usuario.Empresa = EmpresaDados.RecuperarEmpresaPorId(Convert.ToInt32(Dados["IdEmpresa"]));
                usuario.Nome = Convert.ToString(Dados["Nome"]);
                usuario.CPF = Convert.ToString(Dados["CPF"]);
                usuario.Login = Convert.ToString(Dados["Login"]);
                usuario.Senha = Convert.ToString(Dados["Senha"]);
                usuario.DataCadastro = Convert.ToDateTime(Dados["DataCadastro"]);
                usuario.AutorizadorPor = Convert.ToString(Dados["AutorizadorPor"]);
                usuario.DataAutorizacao = Convert.ToDateTime(Dados["DataAutorizacao"]);
                usuario.Ativo = Convert.ToBoolean(Dados["Ativo"]);
                usuario.Administrador = Convert.ToBoolean(Dados["Administrador"]);


                if (Dados["AutorizadorPor"] != DBNull.Value)
                {
                    usuario.AutorizadorPor = Convert.ToString(Dados["AutorizadorPor"]);
                }

                if (Dados["DataAutorizacao"] != DBNull.Value)
                {
                    usuario.DataAutorizacao = Convert.ToDateTime(Dados["DataAutorizacao"]);
                }

                //Adiciona o Usuario à lista de retornos
                usuarios.Add(usuario);
            }

            conexao.FecharConexao();

            return usuarios;
        }

        public static List<Usuario> CarregarUsuarios(Usuario User)
        {

            usuarios = new List<Usuario>();

            //Define o comando SQL
            String ComandoSQL = "SELECT * FROM USUARIO WHERE IDEMPRESA = " + User.Empresa.IdEmpresa.ToString() + " ORDER BY ATIVO DESC, LOGIN ASC";
            Conexao conexao = new Conexao();
            conexao.AbrirConexao();
            conexao.Cmd = new SqlCommand(ComandoSQL, conexao.Con);
            DbDataReader Dados = conexao.ExecutarComando(conexao.Cmd);

            Usuario usuario = new Usuario();

            //Se existir o objeto, cria o objeto e atribui os valores
            while (Dados.Read())
            {

                usuario = new Usuario();

                usuario.idUsuario = Convert.ToInt32(Dados["IdUsuario"]);
                usuario.Empresa = EmpresaDados.RecuperarEmpresaPorId(Convert.ToInt32(Dados["IdEmpresa"]));
                usuario.Nome = Convert.ToString(Dados["Nome"]);
                usuario.CPF = Convert.ToString(Dados["CPF"]);
                usuario.Login = Convert.ToString(Dados["Login"]);
                usuario.Senha = Convert.ToString(Dados["Senha"]);
                usuario.DataCadastro = Convert.ToDateTime(Dados["DataCadastro"]);
                usuario.AutorizadorPor = Convert.ToString(Dados["AutorizadorPor"]);
                usuario.DataAutorizacao = Convert.ToDateTime(Dados["DataAutorizacao"]);
                usuario.Ativo = Convert.ToBoolean(Dados["Ativo"]);
                usuario.Administrador = Convert.ToBoolean(Dados["Administrador"]);

                if (Dados["AutorizadorPor"] != DBNull.Value)
                {
                    usuario.AutorizadorPor = Convert.ToString(Dados["AutorizadorPor"]);
                }

                if (Dados["DataAutorizacao"] != DBNull.Value)
                {
                    usuario.DataAutorizacao = Convert.ToDateTime(Dados["DataAutorizacao"]);
                }

                //Adiciona o Usuario à lista de retornos
                usuarios.Add(usuario);
            }

            conexao.FecharConexao();

            return usuarios;
        }

        public static Usuario CarregaUsuariosPorUsuario(Usuario usuario)
        {
            
            //Define o comando SQL
            String ComandoSQL = "SELECT * FROM USUARIO WHERE IDEMPRESA = " + usuario.Empresa.IdEmpresa.ToString() + "' AND IDUSUARIO = '" + usuario.idUsuario + "'";
            Conexao conexao = new Conexao();
            conexao.AbrirConexao();
            conexao.Cmd = new SqlCommand(ComandoSQL, conexao.Con);
            DbDataReader Dados = conexao.ExecutarComando(conexao.Cmd);

            Usuario U = new Usuario();

            //Se existir o objeto, cria o objeto e atribui os valores
            while (Dados.Read())
            {

                U = new Usuario();

                usuario.idUsuario = Convert.ToInt32(Dados["IdUsuario"]);
                usuario.Empresa = EmpresaDados.RecuperarEmpresaPorId(Convert.ToInt32(Dados["IdEmpresa"]));
                usuario.Nome = Convert.ToString(Dados["Nome"]);
                usuario.CPF = Convert.ToString(Dados["CPF"]);
                usuario.Login = Convert.ToString(Dados["Login"]);
                usuario.Senha = Convert.ToString(Dados["Senha"]);
                usuario.DataCadastro = Convert.ToDateTime(Dados["DataCadastro"]);
                usuario.Ativo = Convert.ToBoolean(Dados["Ativo"]);
                usuario.Administrador = Convert.ToBoolean(Dados["Administrador"]);


                if (Dados["AutorizadorPor"] != DBNull.Value)
                {
                    U.AutorizadorPor = Convert.ToString(Dados["AutorizadorPor"]);
                }

                if (Dados["DataAutorizacao"] != DBNull.Value)
                {
                    U.DataAutorizacao = Convert.ToDateTime(Dados["DataAutorizacao"]);
                }

            }

            conexao.FecharConexao();

            return usuario;
        }

        public static Usuario CarregaUsuariosPorLoginDocumento(string Login, string Documento)
        {

            //Define o comando SQL
            String ComandoSQL = "SELECT * FROM USUARIO WHERE LOGIN = '" + Login.ToString() + "' AND CPF = '" + Documento.ToString() + "'";
            Conexao conexao = new Conexao();
            conexao.AbrirConexao();
            conexao.Cmd = new SqlCommand(ComandoSQL, conexao.Con);
            DbDataReader Dados = conexao.ExecutarComando(conexao.Cmd);
            Usuario usuario = new Usuario();

            //Se existir o objeto, cria o objeto e atribui os valores
            while (Dados.Read())
            {

                usuario = new Usuario();

                usuario.idUsuario = Convert.ToInt32(Dados["IdUsuario"]);
                usuario.Empresa = EmpresaDados.RecuperarEmpresaPorId(Convert.ToInt32(Dados["IdEmpresa"]));
                usuario.Nome = Convert.ToString(Dados["Nome"]);
                usuario.CPF = Convert.ToString(Dados["CPF"]);
                usuario.Login = Convert.ToString(Dados["Login"]);
                usuario.Senha = Convert.ToString(Dados["Senha"]);
                usuario.DataCadastro = Convert.ToDateTime(Dados["DataCadastro"]);
                usuario.Ativo = Convert.ToBoolean(Dados["Ativo"]);
                usuario.Administrador = Convert.ToBoolean(Dados["Administrador"]);


                if (Dados["AutorizadorPor"] != DBNull.Value)
                {
                    usuario.AutorizadorPor = Convert.ToString(Dados["AutorizadorPor"]);
                }

                if (Dados["DataAutorizacao"] != DBNull.Value)
                {
                    usuario.DataAutorizacao = Convert.ToDateTime(Dados["DataAutorizacao"]);
                }

            }

            conexao.FecharConexao();

            return usuario;
        }

        public static Usuario CarregaUsuariosPorNomeUsuario(Usuario User, string nomeusuario)
        {

            //Define o comando SQL
            String ComandoSQL = "SELECT TOP 1 * FROM USUARIO WHERE IDEMPRESA = " + User.Empresa.IdEmpresa.ToString() + " AND NOME = '" + nomeusuario + "'";
            Conexao conexao = new Conexao();
            conexao.AbrirConexao();
            conexao.Cmd = new SqlCommand(ComandoSQL, conexao.Con);
            DbDataReader Dados = conexao.ExecutarComando(conexao.Cmd);

            Usuario usuario = new Usuario();

            //Se existir o objeto, cria o objeto e atribui os valores
            while (Dados.Read())
            {

                usuario = new Usuario();

                usuario.idUsuario = Convert.ToInt32(Dados["IdUsuario"]);
                usuario.Empresa = EmpresaDados.RecuperarEmpresaPorId(Convert.ToInt32(Dados["IdEmpresa"]));
                usuario.Nome = Convert.ToString(Dados["Nome"]);
                usuario.CPF = Convert.ToString(Dados["CPF"]);
                usuario.Login = Convert.ToString(Dados["Login"]);
                usuario.Senha = Convert.ToString(Dados["Senha"]);
                usuario.DataCadastro = Convert.ToDateTime(Dados["DataCadastro"]);
                usuario.AutorizadorPor = Convert.ToString(Dados["AutorizadorPor"]);
                usuario.DataAutorizacao = Convert.ToDateTime(Dados["DataAutorizacao"]);
                usuario.Ativo = Convert.ToBoolean(Dados["Ativo"]);
                usuario.Administrador = Convert.ToBoolean(Dados["Administrador"]);

            }

            conexao.FecharConexao();

            return usuario;
        }

        public static Usuario CarregaUsuariosPorLoginUsuario(string login)
        {
            Conexao conexao = new Conexao();

            //Comando SQL para listar todas as empresas
            string ComandoSQL = "SELECT * FROM USUARIO WHERE LOGIN = '" + login.ToString() + "'";

            //Executa a consulta
            DbDataReader Dados = conexao.ExecutarConsulta(ComandoSQL);

            Usuario usuario = new Usuario();

           
            //Se existir o objeto, cria o objeto e atribui os valores
            while (Dados.Read())
            {

                usuario.idUsuario = Convert.ToInt32(Dados["IdUsuario"]);
                usuario.Empresa = EmpresaDados.RecuperarEmpresaPorId(Convert.ToInt32(Dados["IdEmpresa"]));
                usuario.Nome = Convert.ToString(Dados["Nome"]);
                usuario.CPF = Convert.ToString(Dados["CPF"]);
                usuario.Login = Convert.ToString(Dados["Login"]);
                usuario.Senha = Convert.ToString(Dados["Senha"]);
                usuario.DataCadastro = Convert.ToDateTime(Dados["DataCadastro"]);
                usuario.AutorizadorPor = Convert.ToString(Dados["AutorizadorPor"]);
                usuario.DataAutorizacao = Convert.ToDateTime(Dados["DataAutorizacao"]);
                usuario.Ativo = Convert.ToBoolean(Dados["Ativo"]);
                usuario.Administrador = Convert.ToBoolean(Dados["Administrador"]);
            }

            conexao.FecharConexao();

            return usuario;
        }

        public static Usuario CarregaUsuariosPorId(Usuario User, int Id)
        {

            //Define o comando SQL
            String ComandoSQL = "SELECT * FROM USUARIO WHERE IDEMPRESA = " + User.Empresa.IdEmpresa.ToString() + " AND IDUSUARIO = '" + Id + "'";
            Conexao conexao = new Conexao();
            conexao.AbrirConexao();
            conexao.Cmd = new SqlCommand(ComandoSQL, conexao.Con);
            DbDataReader Dados = conexao.ExecutarComando(conexao.Cmd);

            Usuario usuario = new Usuario();

            //Se existir o objeto, cria o objeto e atribui os valores
            while (Dados.Read())
            {

                usuario = new Usuario();

                usuario.idUsuario = Convert.ToInt32(Dados["IdUsuario"]);
                usuario.Empresa = EmpresaDados.RecuperarEmpresaPorId(Convert.ToInt32(Dados["IdEmpresa"]));
                usuario.Nome = Convert.ToString(Dados["Nome"]);
                usuario.CPF = Convert.ToString(Dados["CPF"]);
                usuario.Login = Convert.ToString(Dados["Login"]);
                usuario.Senha = Convert.ToString(Dados["Senha"]);
                usuario.Ativo = Convert.ToBoolean(Dados["Ativo"]);
                usuario.Administrador = Convert.ToBoolean(Dados["Administrador"]);

                if (Dados["AutorizadorPor"] != DBNull.Value)
                {
                    usuario.AutorizadorPor = Convert.ToString(Dados["AutorizadorPor"]);
                }

                if (Dados["DataAutorizacao"] != DBNull.Value)
                {
                    usuario.DataAutorizacao = Convert.ToDateTime(Dados["DataAutorizacao"]);
                }
            }

            conexao.FecharConexao();

            return usuario;
        }

        public static void Autorizar(Usuario usuario)
        {
            Conexao conexao = new Conexao();
            conexao.AbrirConexao();

            try
            {

                //Monta o Comando SQL
                String ComandoSQL = "UPDATE USUARIO SET IDEMPRESA = UPPER(@VARIAVEL1), NOME = UPPER(@VARIAVEL2), CPF = UPPER(@VARIAVEL3), LOGIN = UPPER(@VARIAVEL4), SENHA = UPPER(@VARIAVEL5), DATACADASTRO = UPPER(@VARIAVEL6), AUTORIZADORPOR = UPPER(@VARIAVEL7), DATAAUTORIZACAO = UPPER(@VARIAVEL8), ATIVO = UPPER(@VARIAVEL9) WHERE IDUSUARIO = @VARIAVEL10";

                conexao.Cmd = new SqlCommand(ComandoSQL, conexao.Con);

                //Define os parametros utilizado no SQL
                conexao.Cmd.Parameters.AddWithValue("@VARIAVEL1", usuario.Empresa.IdEmpresa);
                conexao.Cmd.Parameters.AddWithValue("@VARIAVEL2", usuario.Nome);
                conexao.Cmd.Parameters.AddWithValue("@VARIAVEL3", usuario.CPF);
                conexao.Cmd.Parameters.AddWithValue("@VARIAVEL4", usuario.Login);
                conexao.Cmd.Parameters.AddWithValue("@VARIAVEL5", usuario.Senha);
                conexao.Cmd.Parameters.AddWithValue("@VARIAVEL6", usuario.DataCadastro);
                conexao.Cmd.Parameters.AddWithValue("@VARIAVEL7", usuario.AutorizadorPor);
                conexao.Cmd.Parameters.AddWithValue("@VARIAVEL8", usuario.DataAutorizacao);
                conexao.Cmd.Parameters.AddWithValue("@VARIAVEL9", usuario.Ativo);
                conexao.Cmd.Parameters.AddWithValue("@VARIAVEL10", usuario.idUsuario);

                conexao.Cmd.ExecuteNonQuery(); //Executa o comando SQL na base de dados
            }
            catch (Exception erro)
            {
                //Em caso de erro retorna a mensagem de erro
                throw new Exception("Erro ao Editar o Usuario " + erro.Message);
            }
            finally
            {
                //Fecha a Conexão --Independentemente se der erro ou não, a conexão será fechada!
                conexao.FecharConexao();
            }
        }

        public static void Gravar(Usuario usuario)
        {
            Conexao conexao = new Conexao();
            conexao.AbrirConexao();

            try
            {
                usuario.DataCadastro = DateTime.Now; //Hora de inclusão

                //Se a empresa não existe, é o primeiro acesso! O usuário ja irá ficar autorizado
                //------------------------------------------------------------------------------------------------------
                int IdEmpresa = RetornaEmpresa(usuario.NomeEmpresa, usuario.CNPJEmpresa);

                int QuantidadeUsuarios = EmpresaDados.QuantidadeUsuarios(IdEmpresa);

                //Definir se a empresa for nova, o usuário já deverá ser autorizado na hora
                if (QuantidadeUsuarios == 0)
                {
                    usuario.AutorizadorPor = "SistemaAcesso1";
                    usuario.DataAutorizacao = DateTime.Now; //Hora de inclusão
                    usuario.Ativo = true;
                }
                //------------------------------------------------------------------------------------------------------


                //Busca o Identificador da Empresa
                //------------------------------------------------------------------------------------------------------
                usuario.Empresa = EmpresaDados.CarregaEmpresasPorCNPJ(usuario.CNPJEmpresa);
                //------------------------------------------------------------------------------------------------------

                usuario.CPF = String.Join("", System.Text.RegularExpressions.Regex.Split(usuario.CPF, @"[^\d]"));

                //Monta o Comando SQL
                String ComandoSQL = "INSERT INTO USUARIO (IDEMPRESA, NOME, CPF, LOGIN, SENHA, DATACADASTRO, AUTORIZADORPOR, DATAAUTORIZACAO, ATIVO) VALUES(UPPER(@VARIAVEL1), UPPER(@VARIAVEL2), UPPER(@VARIAVEL3), UPPER(@VARIAVEL4), UPPER(@VARIAVEL5), UPPER(@VARIAVEL6), UPPER(@VARIAVEL7), UPPER(@VARIAVEL8), UPPER(@VARIAVEL9))";
                conexao.Cmd = new SqlCommand(ComandoSQL, conexao.Con);

                conexao.Cmd.Parameters.AddWithValue("@VARIAVEL1", usuario.Empresa.IdEmpresa);
                conexao.Cmd.Parameters.AddWithValue("@VARIAVEL2", usuario.Nome);
                conexao.Cmd.Parameters.AddWithValue("@VARIAVEL3", usuario.CPF);
                conexao.Cmd.Parameters.AddWithValue("@VARIAVEL4", usuario.Login);
                conexao.Cmd.Parameters.AddWithValue("@VARIAVEL5", usuario.Senha);
                conexao.Cmd.Parameters.AddWithValue("@VARIAVEL6", usuario.DataCadastro);
                conexao.Cmd.Parameters.AddWithValue("@VARIAVEL9", usuario.Ativo);


                //Usuário responsável pela autorização do usuário
                //-------------------------------------------------------------------------------------------------------------
                if (usuario.AutorizadorPor != null)
                {
                    conexao.Cmd.Parameters.AddWithValue("@VARIAVEL7", usuario.AutorizadorPor);
                }
                else
                {
                    conexao.Cmd.Parameters.AddWithValue("@VARIAVEL7", DBNull.Value);
                }
                //-------------------------------------------------------------------------------------------------------------

                //-------------------------------------------------------------------------------------------------------------
                if (usuario.DataAutorizacao != null)
                {
                    conexao.Cmd.Parameters.AddWithValue("@VARIAVEL8", usuario.DataAutorizacao);
                }
                else
                {
                    conexao.Cmd.Parameters.AddWithValue("@VARIAVEL8", DBNull.Value);
                }
                //-------------------------------------------------------------------------------------------------------------


                conexao.Cmd.ExecuteNonQuery(); //Executa o comando SQL na base de dados
            }
            catch (Exception erro)
            {
                //Em caso de erro retorna a mensagem de erro
                throw new Exception("Erro ao Gravar o Usuario " + erro.Message);
            }
            finally
            {
                //Fecha a Conexão --Independentemente se der erro ou não, a conexão será fechada!
                conexao.FecharConexao();
            }
        }

        public static void Editar(Usuario usuario)
        {
            Conexao conexao = new Conexao();
            conexao.AbrirConexao();

            try
            {

                //Monta o Comando SQL
                String ComandoSQL = "UPDATE USUARIO SET IDEMPRESA = UPPER(@VARIAVEL1), NOME = UPPER(@VARIAVEL2), CPF = UPPER(@VARIAVEL3), LOGIN = UPPER(@VARIAVEL4), SENHA = UPPER(@VARIAVEL5), DATACADASTRO = UPPER(@VARIAVEL6), AUTORIZADORPOR = UPPER(@VARIAVEL7), DATAAUTORIZACAO = UPPER(@VARIAVEL8), ATIVO = UPPER(@VARIAVEL9) WHERE IDUSUARIO = @VARIAVEL10";

                conexao.Cmd = new SqlCommand(ComandoSQL, conexao.Con);

                //Define os parametros utilizado no SQL
                conexao.Cmd.Parameters.AddWithValue("@VARIAVEL1", usuario.Empresa.IdEmpresa);
                conexao.Cmd.Parameters.AddWithValue("@VARIAVEL2", usuario.Nome);
                conexao.Cmd.Parameters.AddWithValue("@VARIAVEL3", usuario.CPF);
                conexao.Cmd.Parameters.AddWithValue("@VARIAVEL4", usuario.Login);
                conexao.Cmd.Parameters.AddWithValue("@VARIAVEL5", usuario.Senha);
                conexao.Cmd.Parameters.AddWithValue("@VARIAVEL6", usuario.DataCadastro);
                conexao.Cmd.Parameters.AddWithValue("@VARIAVEL7", usuario.AutorizadorPor);
                conexao.Cmd.Parameters.AddWithValue("@VARIAVEL8", usuario.DataAutorizacao);
                conexao.Cmd.Parameters.AddWithValue("@VARIAVEL9", usuario.Ativo);
                conexao.Cmd.Parameters.AddWithValue("@VARIAVEL10", usuario.idUsuario);

                conexao.Cmd.ExecuteNonQuery(); //Executa o comando SQL na base de dados
            }
            catch (Exception erro)
            {
                //Em caso de erro retorna a mensagem de erro
                throw new Exception("Erro ao Editar o Usuario " + erro.Message);
            }
            finally
            {
                //Fecha a Conexão --Independentemente se der erro ou não, a conexão será fechada!
                conexao.FecharConexao();
            }
        }

        public static void AlterarDadosCadastrais(Usuario usuario)
        {
            Conexao conexao = new Conexao();
            conexao.AbrirConexao();

            try
            {

                //Monta o Comando SQL
                String ComandoSQL = "UPDATE USUARIO SET NOME = UPPER(@VARIAVEL1), CPF = UPPER(@VARIAVEL2), LOGIN = UPPER(@VARIAVEL3) WHERE IDUSUARIO = @VARIAVEL4";

                conexao.Cmd = new SqlCommand(ComandoSQL, conexao.Con);

                //Define os parametros utilizado no SQL
                conexao.Cmd.Parameters.AddWithValue("@VARIAVEL1", usuario.Nome);
                conexao.Cmd.Parameters.AddWithValue("@VARIAVEL2", usuario.CPF);
                conexao.Cmd.Parameters.AddWithValue("@VARIAVEL3", usuario.Login);
                conexao.Cmd.Parameters.AddWithValue("@VARIAVEL4", usuario.idUsuario);

                conexao.Cmd.ExecuteNonQuery(); //Executa o comando SQL na base de dados
            }
            catch (Exception erro)
            {
                //Em caso de erro retorna a mensagem de erro
                throw new Exception("Erro ao Editar o Usuario " + erro.Message);
            }
            finally
            {
                //Fecha a Conexão --Independentemente se der erro ou não, a conexão será fechada!
                conexao.FecharConexao();
            }
        }

        public static int RetornaEmpresa(string NomeEmpresa, string CNPJEmpresa)
        {
            Conexao conexao = new Conexao();
            conexao.AbrirConexao();

            try
            {

                //Busca o Identificador da empresa
                //--------------------------------------------------------------------------------------------------------------
                int IdEmpresa = EmpresaExiste(NomeEmpresa, CNPJEmpresa);

                //--------------------------------------------------------------------------------------------------------------


                //Se a empresa não existe, inclui
                //--------------------------------------------------------------------------------------------------------------
                if (IdEmpresa == 0)
                {
                    //Monta o Comando SQL
                    String ComandoSQL = "INSERT INTO EMPRESAS (NOMEEMPRESA, CNPJ) VALUES (UPPER(@VARIAVEL1), UPPER(@VARIAVEL2))";
                    conexao.Cmd = new SqlCommand(ComandoSQL, conexao.Con);

                    conexao.Cmd.Parameters.AddWithValue("@VARIAVEL1", NomeEmpresa);
                    conexao.Cmd.Parameters.AddWithValue("@VARIAVEL2", CNPJEmpresa);

                    conexao.Cmd.ExecuteNonQuery(); //Executa o comando SQL na base de dados

                    //Busca Novamente o identificador da Empresa
                    IdEmpresa = EmpresaExiste(NomeEmpresa, CNPJEmpresa);

                }
                //--------------------------------------------------------------------------------------------------------------

                return IdEmpresa;
            }
            catch (Exception erro)
            {
                //Em caso de erro retorna a mensagem de erro
                throw new Exception("Erro ao Obter a Empresa " + erro.Message);
            }
            finally
            {
                //Fecha a Conexão --Independentemente se der erro ou não, a conexão será fechada!
                conexao.FecharConexao();
            }
        }

        public static void Desativar(Usuario usuario)
        {
            Conexao conexao = new Conexao();
            conexao.AbrirConexao();

            try
            {

                //Monta o Comando SQL
                String ComandoSQL = "UPDATE USUARIO SET ATIVO = 0 WHERE IDEMPRESA = @EMPRESA AND IDUSUARIO = @VARIAVEL1";

                conexao.Cmd = new SqlCommand(ComandoSQL, conexao.Con);

                //Define os parametros utilizado no SQL
                conexao.Cmd.Parameters.AddWithValue("@VARIAVEL1", usuario.idUsuario);
                conexao.Cmd.Parameters.AddWithValue("@EMPRESA", usuario.Empresa.IdEmpresa);

                conexao.Cmd.ExecuteNonQuery(); //Executa o comando SQL na base de dados
            }
            catch (Exception erro)
            {
                //Em caso de erro retorna a mensagem de erro
                throw new Exception("Erro ao Desativar o Usuario " + erro.Message);
            }
            finally
            {
                //Fecha a Conexão --Independentemente se der erro ou não, a conexão será fechada!
                conexao.FecharConexao();
            }
        }

        public static void AlterarSenha(Usuario usuario)
        {
            Conexao conexao = new Conexao();
            conexao.AbrirConexao();

            try
            {

                //Monta o Comando SQL
                String ComandoSQL = "UPDATE USUARIO SET SENHA = @SENHA WHERE IDEMPRESA = @EMPRESA AND IDUSUARIO = @VARIAVEL1";

                conexao.Cmd = new SqlCommand(ComandoSQL, conexao.Con);

                //Define os parametros utilizado no SQL
                conexao.Cmd.Parameters.AddWithValue("@SENHA", usuario.Senha);
                conexao.Cmd.Parameters.AddWithValue("@EMPRESA", usuario.Empresa.IdEmpresa);
                conexao.Cmd.Parameters.AddWithValue("@VARIAVEL1", usuario.idUsuario);

                conexao.Cmd.ExecuteNonQuery(); //Executa o comando SQL na base de dados
            }
            catch (Exception erro)
            {
                //Em caso de erro retorna a mensagem de erro
                throw new Exception("Erro ao Alterar a senha o Usuario " + erro.Message);
            }
            finally
            {
                //Fecha a Conexão --Independentemente se der erro ou não, a conexão será fechada!
                conexao.FecharConexao();
            }
        }

        public static void AtivarDesativarUsuario(Usuario usuario, int UsuarioAlterado)
        {
            Conexao conexao = new Conexao();
            conexao.AbrirConexao();

            try
            {

                //Monta o Comando SQL
                String ComandoSQL = "UPDATE USUARIO SET ATIVO = CASE WHEN ATIVO = 1 THEN 0 ELSE 1 END WHERE IDEMPRESA = @EMPRESA AND IDUSUARIO = @VARIAVEL1";

                conexao.Cmd = new SqlCommand(ComandoSQL, conexao.Con);

                //Define os parametros utilizado no SQL
                conexao.Cmd.Parameters.AddWithValue("@EMPRESA", usuario.Empresa.IdEmpresa);
                conexao.Cmd.Parameters.AddWithValue("@VARIAVEL1", UsuarioAlterado);

                conexao.Cmd.ExecuteNonQuery(); //Executa o comando SQL na base de dados
            }
            catch (Exception erro)
            {
                //Em caso de erro retorna a mensagem de erro
                throw new Exception("Erro ao Alterar o Usuario " + erro.Message);
            }
            finally
            {
                //Fecha a Conexão --Independentemente se der erro ou não, a conexão será fechada!
                conexao.FecharConexao();
            }
        }

        public static void AtivarDesativarAdministrador(Usuario usuario, int UsuarioAlterado)
        {
            Conexao conexao = new Conexao();
            conexao.AbrirConexao();

            try
            {

                //Monta o Comando SQL
                String ComandoSQL = "UPDATE USUARIO SET Administrador = CASE WHEN Administrador = 1 THEN 0 ELSE 1 END WHERE IDEMPRESA = @EMPRESA AND IDUSUARIO = @VARIAVEL1";

                conexao.Cmd = new SqlCommand(ComandoSQL, conexao.Con);

                //Define os parametros utilizado no SQL
                conexao.Cmd.Parameters.AddWithValue("@EMPRESA", usuario.Empresa.IdEmpresa);
                conexao.Cmd.Parameters.AddWithValue("@VARIAVEL1", UsuarioAlterado);

                conexao.Cmd.ExecuteNonQuery(); //Executa o comando SQL na base de dados
            }
            catch (Exception erro)
            {
                //Em caso de erro retorna a mensagem de erro
                throw new Exception("Erro ao Alterar o Usuario " + erro.Message);
            }
            finally
            {
                //Fecha a Conexão --Independentemente se der erro ou não, a conexão será fechada!
                conexao.FecharConexao();
            }
        }

        public static void AutorizarUso(Usuario usuario, int UsuarioAlterado)
        {
            Conexao conexao = new Conexao();
            conexao.AbrirConexao();

            try
            {

                //Monta o Comando SQL
                String ComandoSQL = "UPDATE USUARIO SET AUTORIZADORPOR = UPPER(@AUTORIZADOR), DATAAUTORIZACAO = @DATAAUTORIZACAO, ATIVO = 1 WHERE IDEMPRESA = @EMPRESA AND IDUSUARIO = @VARIAVEL1";

                conexao.Cmd = new SqlCommand(ComandoSQL, conexao.Con);

                //Define os parametros utilizado no SQL
                conexao.Cmd.Parameters.AddWithValue("@AUTORIZADOR", usuario.Login);
                conexao.Cmd.Parameters.AddWithValue("@DATAAUTORIZACAO", System.DateTime.Now);
                conexao.Cmd.Parameters.AddWithValue("@EMPRESA", usuario.Empresa.IdEmpresa);
                conexao.Cmd.Parameters.AddWithValue("@VARIAVEL1", UsuarioAlterado);

                conexao.Cmd.ExecuteNonQuery(); //Executa o comando SQL na base de dados
            }
            catch (Exception erro)
            {
                //Em caso de erro retorna a mensagem de erro
                throw new Exception("Erro ao Alterar o Usuario " + erro.Message);
            }
            finally
            {
                //Fecha a Conexão --Independentemente se der erro ou não, a conexão será fechada!
                conexao.FecharConexao();
            }
        }

        public static void RenovarSenha(Usuario usuario, int UsuarioAlterado)
        {
            Conexao conexao = new Conexao();
            conexao.AbrirConexao();

            try
            {

                //Monta o Comando SQL
                String ComandoSQL = "UPDATE USUARIO SET SENHA = @SENHA, ATIVO = 1 WHERE IDEMPRESA = @EMPRESA AND IDUSUARIO = @VARIAVEL1";

                conexao.Cmd = new SqlCommand(ComandoSQL, conexao.Con);

                //Define os parametros utilizado no SQL
                conexao.Cmd.Parameters.AddWithValue("@SENHA", "123456");
                conexao.Cmd.Parameters.AddWithValue("@EMPRESA", usuario.Empresa.IdEmpresa);
                conexao.Cmd.Parameters.AddWithValue("@VARIAVEL1", UsuarioAlterado);

                conexao.Cmd.ExecuteNonQuery(); //Executa o comando SQL na base de dados
            }
            catch (Exception erro)
            {
                //Em caso de erro retorna a mensagem de erro
                throw new Exception("Erro ao Alterar o Usuario " + erro.Message);
            }
            finally
            {
                //Fecha a Conexão --Independentemente se der erro ou não, a conexão será fechada!
                conexao.FecharConexao();
            }
        }

        public static void Excluir(Usuario usuario, int Id)
        {
            Conexao conexao = new Conexao();
            conexao.AbrirConexao();

            try
            {

                //Monta o Comando SQL
                String ComandoSQL = "DELETE FROM USUARIO WHERE IDEMPRESA = @EMPRESA AND IDUSUARIO = @VARIAVEL1";
                conexao.Cmd = new SqlCommand(ComandoSQL, conexao.Con);

                //Define os parametros utilizado no SQL
                conexao.Cmd.Parameters.AddWithValue("@VARIAVEL1", Id);
                conexao.Cmd.Parameters.AddWithValue("@EMPRESA", usuario.Empresa.IdEmpresa);

                conexao.Cmd.ExecuteNonQuery(); //Executa o comando SQL na base de dados
            }
            catch (Exception erro)
            {
                //Em caso de erro retorna a mensagem de erro
                throw new Exception("Erro ao Excluir o Usuario " + erro.Message);
            }
            finally
            {
                //Fecha a Conexão --Independentemente se der erro ou não, a conexão será fechada!
                conexao.FecharConexao();
            }
        }

        public static string Logar(string usuarionome, string usuariosenha)
        {

            Conexao conexao = new Conexao();

            string Retorno = "";

            try
            {

                //Define o comando SQL
                string ComandoSQL = "SELECT * FROM USUARIO WHERE UPPER(LOGIN) = UPPER('" + usuarionome +  "')";
                //string ComandoSQL = "SELECT * FROM EMPRESA";

                //Executa a consulta
                DbDataReader Dados = conexao.ExecutarConsulta(ComandoSQL);

                if (Dados.HasRows == false)
                {
                    Retorno = "Usuário Não Encontrado";
                    return Retorno;
                }

                Usuario usuario = new Usuario();

                //Se existir o objeto, cria o objeto e atribui os valores
                while (Dados.Read())
                {

                    usuario.idUsuario = Convert.ToInt32(Dados["IdUsuario"]);

                    usuario.Nome = Convert.ToString(Dados["Nome"]);
                    usuario.CPF = Convert.ToString(Dados["CPF"]);
                    usuario.Login = Convert.ToString(Dados["Login"]);
                    usuario.Senha = Convert.ToString(Dados["Senha"]);
                    usuario.DataCadastro = Convert.ToDateTime(Dados["DataCadastro"]);
                    usuario.Ativo = Convert.ToBoolean(Dados["Ativo"]);


                    if (Dados["IdEmpresa"] != DBNull.Value)
                    {
                        usuario.Empresa = EmpresaDados.RecuperarEmpresaPorId(Convert.ToInt32(Dados["IdEmpresa"]));
                    }

                    if (Dados["AutorizadorPor"] != DBNull.Value)
                    {
                        usuario.AutorizadorPor = Convert.ToString(Dados["AutorizadorPor"]);
                    }

                    if (Dados["DataAutorizacao"] != DBNull.Value)
                    {
                        usuario.DataAutorizacao = Convert.ToDateTime(Dados["DataAutorizacao"]);
                    }
                }

                //verifica se a senha está válida
                if (usuario.Senha.ToUpper() != usuariosenha.ToUpper())
                {
                    Retorno = "A Senha não confere!";
                }


                //Verifica se o usuário está desativado
                if (usuario.Ativo == false)
                {
                    Retorno = "Usuário está Desativado!";
                }

                //Verifica se o usuário já está autorizado
                if (usuario.DataAutorizacao == null)
                {
                    Retorno = "Aguardando Autorização!";
                }

                return Retorno;
            }
            catch (Exception erro)
            {
                //Em caso de erro retorna a mensagem de erro
                throw new Exception("Erro ao Editar o Usuario " + erro.Message);
            }
            finally
            {
                //Fecha a Conexão --Independentemente se der erro ou não, a conexão será fechada!
                conexao.FecharConexao();
            }
        }

        public static bool UsuarioJaExiste(string usuarionome, string usuariologin)
        {

            bool Retorno = false;
            
            //Verifica se o usuário já foi cadastrado
            String ComandoSQL = "";
            ComandoSQL = ComandoSQL + " SELECT IDUSUARIO FROM USUARIO  WHERE LOGIN = @VARIAVEL1";
            Conexao conexao = new Conexao();
            conexao.AbrirConexao();
            conexao.Cmd = new SqlCommand(ComandoSQL, conexao.Con);
            DbDataReader Dados = conexao.ExecutarComando(conexao.Cmd);


            while (Dados.Read())
            {
                Retorno = true;
            }

            return Retorno;


        }

        public static bool UsuarioJaExisteEdicaoCadastral(Usuario usuario, Usuario User)
        {

            bool Retorno = false;

            Conexao conexao = new Conexao();
            conexao.AbrirConexao();

            //Verifica se o usuário já foi cadastrado
            String ComandoSQL = "";
            ComandoSQL = ComandoSQL + " SELECT IDUSUARIO ";
            ComandoSQL = ComandoSQL + " FROM   USUARIO ";
            ComandoSQL = ComandoSQL + " WHERE  IDEMPRESA = @EMPRESA AND ";
            ComandoSQL = ComandoSQL + "        IDUSUARIO <> @IDUSUARIO AND ";
            ComandoSQL = ComandoSQL + "        LOGIN = @VARIAVEL1";

            conexao.Cmd = new SqlCommand(ComandoSQL, conexao.Con);

            conexao.Cmd.Parameters.AddWithValue("@EMPRESA", usuario.Empresa.IdEmpresa);
            conexao.Cmd.Parameters.AddWithValue("@IDUSUARIO", usuario.idUsuario);
            conexao.Cmd.Parameters.AddWithValue("@VARIAVEL1", User.Login);

            DbDataReader Dados = conexao.ExecutarComando(conexao.Cmd);

            while (Dados.Read())
            {
                Retorno = true;
            }

            return Retorno;


        }

        public static bool ConfereSenhaAtual(Usuario usuario)
        {

            bool Retorno = false;

            Conexao conexao = new Conexao();
            conexao.AbrirConexao();

            //Verifica se o usuário já foi cadastrado
            String ComandoSQL = "";
            ComandoSQL = ComandoSQL + " SELECT LOGIN ";
            ComandoSQL = ComandoSQL + " FROM USUARIO ";
            ComandoSQL = ComandoSQL + " WHERE IDEMPRESA= @EMPRESA AND LOGIN = @VARIAVEL1 AND SENHA = UPPER(@VARIAVEL2)";

            conexao.Cmd = new SqlCommand(ComandoSQL, conexao.Con);
            conexao.Cmd.Parameters.AddWithValue("@EMPRESA", usuario.Empresa.IdEmpresa);
            conexao.Cmd.Parameters.AddWithValue("@VARIAVEL1", usuario.Login);
            conexao.Cmd.Parameters.AddWithValue("@VARIAVEL2", usuario.Senha);

            DbDataReader Dados = conexao.ExecutarComando(conexao.Cmd);

            while (Dados.Read())
            {
                Retorno = true;
            }

            return Retorno;
        }

        public static int EmpresaExiste(string empresanome, string empresacnpj)
        {
            int idEmpresa = 0;

            Conexao conexao = new Conexao();
            conexao.AbrirConexao();

            //Verifica se o usuário já foi cadastrado
            String ComandoSQL = "";
            ComandoSQL = ComandoSQL + " SELECT IDEMPRESA ";
            ComandoSQL = ComandoSQL + " FROM EMPRESAS ";
            ComandoSQL = ComandoSQL + " WHERE CNPJ = @VARIAVEL1";

            conexao.Cmd = new SqlCommand(ComandoSQL, conexao.Con);
            conexao.Cmd.Parameters.AddWithValue("@VARIAVEL1", empresacnpj);

            DbDataReader Dados = conexao.ExecutarComando(conexao.Cmd);

            //Verifica se a empresa existe
            while (Dados.Read())
            {
                idEmpresa = Convert.ToInt32(Dados["IDEMPRESA"]);

                conexao.FecharConexao();

                return idEmpresa;
            }

            conexao.FecharConexao();

            return idEmpresa;
        }
    }
}