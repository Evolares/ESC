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
    public static class EmpresaDados
    {

        public static List<Empresa> Listar()
        {

            Conexao conexao = new Conexao();

            try
            {
                //Cria uma lista de Empresas
                List<Empresa> ListaEmpresa = new List<Empresa>();
                
                //Comando SQL para listar todas as empresas
                string ComandoSQL = "SELECT * FROM EMPRESA";
                
                //Executa a consulta
                DbDataReader Dados = conexao.ExecutarConsulta(ComandoSQL);

                //Empresa empresa = new Empresa();

                //Se existir o objeto, cria o objeto e atribui os valores
                while (Dados.Read())
                {
                    Empresa Empresa = new Empresa();

                    Empresa.IdEmpresa = Convert.ToInt32(Dados["IdEmpresa"]);
                    Empresa.Nome = Convert.ToString(Dados["Nome"]);
                    Empresa.NomeFantasia = Convert.ToString(Dados["NomeFantasia"]);
                    Empresa.CNPJ = Convert.ToString(Dados["CNPJ"]);
                    Empresa.Endereco = Convert.ToString(Dados["Endereco"]);
                    Empresa.Cidade = Convert.ToString(Dados["Cidade"]);
                    Empresa.UF = Convert.ToString(Dados["UF"]);
                    Empresa.CEP = Convert.ToString(Dados["CEP"]);
                    Empresa.Telefone = Convert.ToString(Dados["Telefone"]);
                    Empresa.email = Convert.ToString(Dados["Email"]);
                    Empresa.CapitalSocial = Convert.ToDecimal(Dados["CapitalSocial"]);
                    Empresa.Ativa = Convert.ToBoolean(Dados["Ativa"]);

                    ListaEmpresa.Add(Empresa);
                }

                Dados.Close();

                return ListaEmpresa;
            }
            catch (Exception erro)
            {
                //Em caso de erro retorna a mensagem de erro
                throw new Exception("Erro ao Recuperar a Empresa " + erro.Message);
            }
            finally
            {
                //Fecha a Conexão --Independentemente se der erro ou não, a conexão será fechada!
                conexao.FecharConexao();
            }

        }

        public static Empresa RecuperarEmpresaPorId(int idEmpresa)
        {
            Conexao conexao = new Conexao();
            
            try
            {
                conexao.AbrirConexao();

                string ComandoSQL = "SELECT * FROM EMPRESA WHERE IDEMPRESA = @VARIAVEL1;";
                
                conexao.Cmd = new SqlCommand(ComandoSQL, conexao.Con);

                //Define os parametros utilizado no SQL
                conexao.Cmd.Parameters.AddWithValue("@VARIAVEL1", idEmpresa);
                
                Empresa empresa = new Empresa();

                DbDataReader Dados = conexao.ExecutarComando(conexao.Cmd);

                    if (Dados.Read())
                    {

                        empresa.IdEmpresa = Convert.ToInt32(Dados["IdEmpresa"]);
                        empresa.Nome = Convert.ToString(Dados["Nome"]);
                        empresa.NomeFantasia = Convert.ToString(Dados["NomeFantasia"]);
                        empresa.CNPJ = Convert.ToString(Dados["CNPJ"]);
                        empresa.Endereco = Convert.ToString(Dados["Endereco"]);
                        empresa.Cidade = Convert.ToString(Dados["Cidade"]);
                        empresa.UF = Convert.ToString(Dados["UF"]);
                        empresa.CEP = Convert.ToString(Dados["CEP"]);
                        empresa.Telefone = Convert.ToString(Dados["Telefone"]);
                        empresa.email = Convert.ToString(Dados["Email"]);
                        empresa.CapitalSocial = Convert.ToDecimal(Dados["CapitalSocial"]);
                        empresa.Ativa = Convert.ToBoolean(Dados["Ativa"]);

                    }

                Dados.Close();

                return empresa;
            }
            catch (Exception erro)
            {
                //Em caso de erro retorna a mensagem de erro
                throw new Exception("Erro ao Recuperar a Empresa " + erro.Message);
            }
            finally
            {
                //Fecha a Conexão --Independentemente se der erro ou não, a conexão será fechada!
                conexao.FecharConexao();
            }
           


        }

        public static void Gravar(Empresa empresa)
        {
            Conexao conexao = new Conexao();

            /*Passo a Passo Conexao.
                1) Conexao.AbrirConexao();
                2) Monta a String de Conexão passando as variáveis (ComandoSQL)
                3) Comando de Execução, recebendo o comando SQL e a variavel de Conexao (conexao.con) (Exemplo: conexao.Cmd = new SqlCommand(ComandoSQL, conexao.Con);)
                4) Adiciona os parametros (Exemplo: conexao.Cmd.Parameters.AddWithValue("@VARIAVEL1", empresa.Nome);)
                5) Executa o comando SQL na Base. Exemplo: conexao.Cmd.ExecuteNonQuery();
                6) Fecha a conexão com o banco: Exemplo: conexao.FecharConexao(); F
            */

            conexao.AbrirConexao();

            try
            {

                //Monta o Comando SQL
                String ComandoSQL = " UPDATE EMPRESA SET ";
                ComandoSQL = ComandoSQL + " NOME = @VARIAVEL1,";
                ComandoSQL = ComandoSQL + " NOMEFANTASIA = @VARIAVEL2, ";
                ComandoSQL = ComandoSQL + " CNPJ = UPPER(@VARIAVEL3), ";
                ComandoSQL = ComandoSQL + " ENDERECO = UPPER(@VARIAVEL4), ";
                ComandoSQL = ComandoSQL + " CIDADE = UPPER(@VARIAVEL5), ";
                ComandoSQL = ComandoSQL + " UF = UPPER(@VARIAVEL6), ";
                ComandoSQL = ComandoSQL + " CEP = UPPER(@VARIAVEL7), ";
                ComandoSQL = ComandoSQL + " TELEFONE = UPPER(@VARIAVEL8), ";
                ComandoSQL = ComandoSQL + " EMAIL = UPPER(@VARIAVEL9), ";
                ComandoSQL = ComandoSQL + " CAPITALSOCIAL = UPPER(@VARIAVEL10), ";
                ComandoSQL = ComandoSQL + " ATIVA = UPPER(@VARIAVEL11) ";
                ComandoSQL = ComandoSQL + " WHERE   IDEMPRESA = UPPER(@VARIAVEL12) ";

                conexao.Cmd = new SqlCommand(ComandoSQL, conexao.Con);

                //Define os parametros utilizado no SQL
                conexao.Cmd.Parameters.AddWithValue("@VARIAVEL1", empresa.Nome);
                conexao.Cmd.Parameters.AddWithValue("@VARIAVEL2", empresa.NomeFantasia);
                conexao.Cmd.Parameters.AddWithValue("@VARIAVEL3", empresa.CNPJ);
                conexao.Cmd.Parameters.AddWithValue("@VARIAVEL4", empresa.Endereco);
                conexao.Cmd.Parameters.AddWithValue("@VARIAVEL5", empresa.Cidade);
                conexao.Cmd.Parameters.AddWithValue("@VARIAVEL6", empresa.UF);
                conexao.Cmd.Parameters.AddWithValue("@VARIAVEL7", empresa.CEP);
                conexao.Cmd.Parameters.AddWithValue("@VARIAVEL8", empresa.Telefone);
                conexao.Cmd.Parameters.AddWithValue("@VARIAVEL9", empresa.email);
                conexao.Cmd.Parameters.AddWithValue("@VARIAVEL10", empresa.CapitalSocial);
                conexao.Cmd.Parameters.AddWithValue("@VARIAVEL11", empresa.Ativa);

                conexao.Cmd.Parameters.AddWithValue("@VARIAVEL12", empresa.IdEmpresa);

                conexao.Cmd.ExecuteNonQuery(); //Executa o comando SQL na base de dados
            }
            catch (Exception erro)
            {
                //Em caso de erro retorna a mensagem de erro
                throw new Exception("Erro ao Editar a Empresa " + erro.Message);
            }
            finally
            {
                //Fecha a Conexão --Independentemente se der erro ou não, a conexão será fechada!
                conexao.FecharConexao();
            }

        }

        public static void Incluir(Empresa empresa)
        {
            Conexao conexao = new Conexao();

            /*Passo a Passo Conexao.
                1) Conexao.AbrirConexao();
                2) Monta a String de Conexão passando as variáveis (ComandoSQL)
                3) Comando de Execução, recebendo o comando SQL e a variavel de Conexao (conexao.con) (Exemplo: conexao.Cmd = new SqlCommand(ComandoSQL, conexao.Con);)
                4) Adiciona os parametros (Exemplo: conexao.Cmd.Parameters.AddWithValue("@VARIAVEL1", empresa.Nome);)
                5) Executa o comando SQL na Base. Exemplo: conexao.Cmd.ExecuteNonQuery();
                6) Fecha a conexão com o banco: Exemplo: conexao.FecharConexao(); F
            */

            conexao.AbrirConexao();

            try
            {

                //Monta o Comando SQL
                String ComandoSQL = " INSERT INTO EMPRESA (NOME, NOMEFANTASIA, CNPJ, ENDERECO, CIDADE, UF, CEP, TELEFONE, EMAIL, CAPITALSOCIAL, ATIVA) VALUES(@VARIAVEL1,@VARIAVEL2,@VARIAVEL3,@VARIAVEL4,@VARIAVEL5,@VARIAVEL6,@VARIAVEL7,@VARIAVEL8,@VARIAVEL9,@VARIAVEL10,@VARIAVEL11) ";

                conexao.Cmd = new SqlCommand(ComandoSQL, conexao.Con);

                //Define os parametros utilizado no SQL
                conexao.Cmd.Parameters.AddWithValue("@VARIAVEL1", empresa.Nome);
                conexao.Cmd.Parameters.AddWithValue("@VARIAVEL2", empresa.NomeFantasia);
                conexao.Cmd.Parameters.AddWithValue("@VARIAVEL3", empresa.CNPJ);
                conexao.Cmd.Parameters.AddWithValue("@VARIAVEL4", empresa.Endereco);
                conexao.Cmd.Parameters.AddWithValue("@VARIAVEL5", empresa.Cidade);
                conexao.Cmd.Parameters.AddWithValue("@VARIAVEL6", empresa.UF);
                conexao.Cmd.Parameters.AddWithValue("@VARIAVEL7", empresa.CEP);
                conexao.Cmd.Parameters.AddWithValue("@VARIAVEL8", empresa.Telefone);
                conexao.Cmd.Parameters.AddWithValue("@VARIAVEL9", empresa.email);
                conexao.Cmd.Parameters.AddWithValue("@VARIAVEL10", empresa.CapitalSocial);
                conexao.Cmd.Parameters.AddWithValue("@VARIAVEL11", empresa.Ativa);

                conexao.Cmd.ExecuteNonQuery(); //Executa o comando SQL na base de dados
            }
            catch (Exception erro)
            {
                //Em caso de erro retorna a mensagem de erro
                throw new Exception("Erro ao Criar a Empresa " + erro.Message);
            }
            finally
            {
                //Fecha a Conexão --Independentemente se der erro ou não, a conexão será fechada!
                conexao.FecharConexao();
            }

        }

        public static void Excluir(Empresa empresa)
        {
            Conexao conexao = new Conexao();

            conexao.AbrirConexao();

            try
            {

                //Monta o Comando SQL
                String ComandoSQL = " DELETE FROM EMPRESA WHERE   IDEMPRESA = UPPER(@VARIAVEL1) ";

                conexao.Cmd = new SqlCommand(ComandoSQL, conexao.Con);

                //Define os parametros utilizado no SQL
                conexao.Cmd.Parameters.AddWithValue("@VARIAVEL1", empresa.IdEmpresa);

                conexao.Cmd.ExecuteNonQuery(); //Executa o comando SQL na base de dados
            }
            catch (Exception erro)
            {
                //Em caso de erro retorna a mensagem de erro
                throw new Exception("Erro ao Excluir a Empresa " + erro.Message);
            }
            finally
            {
                //Fecha a Conexão --Independentemente se der erro ou não, a conexão será fechada!
                conexao.FecharConexao();
            }

        }

        public static int QuantidadeUsuarios(int IdEmpresa)
        {

            int Quantidade = 0;

            //Define o comando SQL
            String ComandoSQL = "SELECT COUNT(*) AS QUANTIDADE FROM USUARIOS WHERE IDEMPRESA = " + IdEmpresa.ToString();
            Conexao conexao = new Conexao();
            conexao.AbrirConexao();
            conexao.Cmd = new SqlCommand(ComandoSQL, conexao.Con);
            DbDataReader Dados = conexao.ExecutarComando(conexao.Cmd);

            //Se existir o objeto, cria o objeto e atribui os valores
            while (conexao.Dr.Read())
            {
                Quantidade = Convert.ToInt32(conexao.Dr["QUANTIDADE"]);

            }

            conexao.FecharConexao();

            return Quantidade;
        }

        public static Empresa CarregaEmpresasPorCNPJ(String CNPJ)
        {
            Conexao conexao = new Conexao();

            try
            {
                //Define o comando SQL
                String ComandoSQL = "SELECT * FROM EMPRESAS WHERE CNPJ = '" + CNPJ + "'";
                conexao.AbrirConexao();
                conexao.Cmd = new SqlCommand(ComandoSQL, conexao.Con);
                DbDataReader Dados = conexao.ExecutarComando(conexao.Cmd);

                Empresa empresa = new Empresa();
                
                if (Dados.Read())
                {

                    empresa.IdEmpresa = Convert.ToInt32(Dados["IdEmpresa"]);
                    empresa.Nome = Convert.ToString(Dados["Nome"]);
                    empresa.NomeFantasia = Convert.ToString(Dados["NomeFantasia"]);
                    empresa.CNPJ = Convert.ToString(Dados["CNPJ"]);
                    empresa.Endereco = Convert.ToString(Dados["Endereco"]);
                    empresa.Cidade = Convert.ToString(Dados["Cidade"]);
                    empresa.UF = Convert.ToString(Dados["UF"]);
                    empresa.CEP = Convert.ToString(Dados["CEP"]);
                    empresa.Telefone = Convert.ToString(Dados["Telefone"]);
                    empresa.email = Convert.ToString(Dados["Email"]);
                    empresa.CapitalSocial = Convert.ToDecimal(Dados["CapitalSocial"]);
                    empresa.Ativa = Convert.ToBoolean(Dados["Ativa"]);

                }

                Dados.Close();

                return empresa;
            }
            catch (Exception erro)
            {
                //Em caso de erro retorna a mensagem de erro
                throw new Exception("Erro ao Recuperar a Empresa " + erro.Message);
            }
            finally
            {
                //Fecha a Conexão --Independentemente se der erro ou não, a conexão será fechada!
                conexao.FecharConexao();
            }



        }
    }
}
