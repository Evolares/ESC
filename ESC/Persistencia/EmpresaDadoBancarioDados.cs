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
    public class EmpresaDadoBancarioDados
    {
        public static List<EmpresaDadoBancario> Listar(int IdEmpresa)
        {

            Conexao conexao = new Conexao();

            try
            {
                //Cria uma lista de Empresas
                List<EmpresaDadoBancario> ListaEmpresaDadoBancario = new List<EmpresaDadoBancario>();

                //Comando SQL para listar todas as empresas
                string ComandoSQL = "SELECT * FROM EMPRESADADOSBANCARIO WHERE IDEMPRESA = " + IdEmpresa.ToString() + ";";

                //Executa a consulta
                DbDataReader Dados = conexao.ExecutarConsulta(ComandoSQL);
                
                //Se existir o objeto, cria o objeto e atribui os valores
                while (Dados.Read())
                {
                    EmpresaDadoBancario EmpresaDadoBancario = new EmpresaDadoBancario();

                    EmpresaDadoBancario.Empresa = EmpresaDados.RecuperarEmpresaPorId(Convert.ToInt32(Dados["IdEmpresa"]));
                    EmpresaDadoBancario.IdDadoBancario = Convert.ToInt32(Dados["IdDadoBancario"]);
                    EmpresaDadoBancario.NumBanco = Convert.ToString(Dados["NumBanco"]);
                    EmpresaDadoBancario.NomeBanco = Convert.ToString(Dados["NomeBanco"]);
                    EmpresaDadoBancario.NumAgencia = Convert.ToString(Dados["NumAgencia"]);
                    EmpresaDadoBancario.NumConta = Convert.ToString(Dados["NumConta"]);
                    EmpresaDadoBancario.TipoConta = TipoContaDados.RecuperarPorId(Convert.ToInt32(Dados["IdTipoConta"]));
                    EmpresaDadoBancario.Ativo = Convert.ToBoolean(Dados["Ativa"]);

                    ListaEmpresaDadoBancario.Add(EmpresaDadoBancario);
                }

                Dados.Close(); //Fecha a Conexão

                return ListaEmpresaDadoBancario;
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

        public static EmpresaDadoBancario RecuperarPorId(int idDadoBancario)
        {
            Conexao conexao = new Conexao();

            try
            {
                conexao.AbrirConexao();

                    //Comando SQL para listar todas as empresas
                    string ComandoSQL = "SELECT * FROM EMPRESADADOSBANCARIO WHERE idDadoBancario = " + idDadoBancario.ToString() + ";";
                    conexao.Cmd = new SqlCommand(ComandoSQL, conexao.Con);
                    EmpresaDadoBancario EmpresaDadoBancario = new EmpresaDadoBancario();
                    DbDataReader Dados = conexao.ExecutarComando(conexao.Cmd);

                    if (Dados.Read())
                    {
                        EmpresaDadoBancario.Empresa = EmpresaDados.RecuperarEmpresaPorId( Convert.ToInt32(Dados["IdEmpresa"]));
                        EmpresaDadoBancario.IdDadoBancario = Convert.ToInt32(Dados["IdDadoBancario"]);
                        EmpresaDadoBancario.NumBanco = Convert.ToString(Dados["NumBanco"]);
                        EmpresaDadoBancario.NomeBanco = Convert.ToString(Dados["NomeBanco"]);
                        EmpresaDadoBancario.NumAgencia = Convert.ToString(Dados["NumAgencia"]);
                        EmpresaDadoBancario.NumConta = Convert.ToString(Dados["NumConta"]);
                        EmpresaDadoBancario.TipoConta = TipoContaDados.RecuperarPorId(Convert.ToInt32(Dados["IdTipoConta"]));
                        EmpresaDadoBancario.Ativo = Convert.ToBoolean(Dados["Ativa"]);
                    }

                Dados.Close();

                return EmpresaDadoBancario;
            }
            catch (Exception erro)
            {                
                throw new Exception("Erro ao Recuperar a Empresa " + erro.Message);//Em caso de erro retorna a mensagem de erro
            }
            finally
            {                
                conexao.FecharConexao();//Fecha a Conexão --Independentemente se der erro ou não, a conexão será fechada!
            }
        }

        public static void Gravar(EmpresaDadoBancario empresadadobancario)
        {
            Conexao conexao = new Conexao();

            conexao.AbrirConexao();

            try
            {

                //Monta o Comando SQL
                String ComandoSQL = " UPDATE EMPRESADADOSBANCARIO SET ";
                ComandoSQL = ComandoSQL + " IDEMPRESA = @VARIAVEL1,";
                ComandoSQL = ComandoSQL + " NUMBANCO = @VARIAVEL2, ";
                ComandoSQL = ComandoSQL + " NOMEBANCO = UPPER(@VARIAVEL3), ";
                ComandoSQL = ComandoSQL + " NUMAGENCIA = UPPER(@VARIAVEL4), ";
                ComandoSQL = ComandoSQL + " NUMCONTA = UPPER(@VARIAVEL5), ";
                ComandoSQL = ComandoSQL + " ATIVA = UPPER(@VARIAVEL6), ";
                ComandoSQL = ComandoSQL + " IDTIPOCONTA = UPPER(@VARIAVEL7) ";
                ComandoSQL = ComandoSQL + " WHERE   IDDADOBANCARIO = UPPER(@VARIAVEL8) ";

                conexao.Cmd = new SqlCommand(ComandoSQL, conexao.Con);

                //Define os parametros utilizado no SQL
                conexao.Cmd.Parameters.AddWithValue("@VARIAVEL1", empresadadobancario.Empresa.IdEmpresa);
                conexao.Cmd.Parameters.AddWithValue("@VARIAVEL2", empresadadobancario.NumBanco);
                conexao.Cmd.Parameters.AddWithValue("@VARIAVEL3", empresadadobancario.NomeBanco);
                conexao.Cmd.Parameters.AddWithValue("@VARIAVEL4", empresadadobancario.NumAgencia);
                conexao.Cmd.Parameters.AddWithValue("@VARIAVEL5", empresadadobancario.NumConta);
                conexao.Cmd.Parameters.AddWithValue("@VARIAVEL6", empresadadobancario.Ativo);
                conexao.Cmd.Parameters.AddWithValue("@VARIAVEL7", empresadadobancario.TipoConta.IdTipoConta);

                conexao.Cmd.Parameters.AddWithValue("@VARIAVEL8", empresadadobancario.IdDadoBancario);

                conexao.Cmd.ExecuteNonQuery(); //Executa o comando SQL na base de dados
            }
            catch (Exception erro)
            {
                //Em caso de erro retorna a mensagem de erro
                throw new Exception("Erro ao Editar o Dado Bancario " + erro.Message);
            }
            finally
            {
                //Fecha a Conexão --Independentemente se der erro ou não, a conexão será fechada!
                conexao.FecharConexao();
            }
        }

        public static void Incluir(EmpresaDadoBancario empresadadobancario)
        {
            Conexao conexao = new Conexao();

            conexao.AbrirConexao();

            try
            {

                //Monta o Comando SQL
                String ComandoSQL = " INSERT INTO EMPRESADADOSBANCARIO (IDEMPRESA, NUMBANCO, NOMEBANCO, NUMAGENCIA, NUMCONTA, IDTIPOCONTA, ATIVA) VALUES(@VARIAVEL1,@VARIAVEL2,@VARIAVEL3,@VARIAVEL4,@VARIAVEL5,@VARIAVEL6,@VARIAVEL7) ";

                conexao.Cmd = new SqlCommand(ComandoSQL, conexao.Con);

                //Define os parametros utilizado no SQL
                conexao.Cmd.Parameters.AddWithValue("@VARIAVEL1", empresadadobancario.Empresa.IdEmpresa);
                conexao.Cmd.Parameters.AddWithValue("@VARIAVEL2", empresadadobancario.NumBanco);
                conexao.Cmd.Parameters.AddWithValue("@VARIAVEL3", empresadadobancario.NomeBanco);
                conexao.Cmd.Parameters.AddWithValue("@VARIAVEL4", empresadadobancario.NumAgencia);
                conexao.Cmd.Parameters.AddWithValue("@VARIAVEL5", empresadadobancario.NumConta);
                conexao.Cmd.Parameters.AddWithValue("@VARIAVEL6", empresadadobancario.TipoConta.IdTipoConta);
                conexao.Cmd.Parameters.AddWithValue("@VARIAVEL7", empresadadobancario.Ativo);

                conexao.Cmd.ExecuteNonQuery(); //Executa o comando SQL na base de dados
            }
            catch (Exception erro)
            {
                //Em caso de erro retorna a mensagem de erro
                throw new Exception("Erro ao Incluir os Dados Bancário " + erro.Message);
            }
            finally
            {
                //Fecha a Conexão --Independentemente se der erro ou não, a conexão será fechada!
                conexao.FecharConexao();
            }

        }

        public static void Excluir(EmpresaDadoBancario empresadadobancario)
        {
            Conexao conexao = new Conexao();
            conexao.AbrirConexao();

            try
            {
                //Monta o Comando SQL
                String ComandoSQL = " DELETE FROM EMPRESADADOSBANCARIO WHERE IDDADOBANCARIO = UPPER(@VARIAVEL1) ";
                conexao.Cmd = new SqlCommand(ComandoSQL, conexao.Con);
                conexao.Cmd.Parameters.AddWithValue("@VARIAVEL1", empresadadobancario.IdDadoBancario);//Define os parametros utilizado no SQL
                conexao.Cmd.ExecuteNonQuery(); //Executa o comando SQL na base de dados
            }
            catch (Exception erro)
            {
                //Em caso de erro retorna a mensagem de erro
                throw new Exception("Erro ao Excluir a EMPRESADADOSBANCARIO " + erro.Message);
            }
            finally
            {
                //Fecha a Conexão --Independentemente se der erro ou não, a conexão será fechada!
                conexao.FecharConexao();
            }
        }
    }
}

