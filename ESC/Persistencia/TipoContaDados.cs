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
    public class TipoContaDados
    {
        public static List<string> Listar()
        {

            Conexao conexao = new Conexao();

            var Lista = new List<string>();
            
            try
            {
                Lista.Add("");

                //Comando SQL para listar todas as empresas
                string ComandoSQL = "SELECT * FROM TIPOCONTA;";
                DbDataReader Dados = conexao.ExecutarConsulta(ComandoSQL); //Executa a consulta
                
                //Se existir o objeto, cria o objeto e atribui os valores
                while (Dados.Read())
                {
                    Lista.Add(Convert.ToString(Dados["DescTipoConta"]));
                }

                Dados.Close(); //Fecha a Conexão

                return Lista;
            }
            catch (Exception erro)
            {
                //Em caso de erro retorna a mensagem de erro
                throw new Exception("Erro ao Recuperar o a Lista " + erro.Message);
            }
            finally
            {
                //Fecha a Conexão --Independentemente se der erro ou não, a conexão será fechada!
                conexao.FecharConexao();
            }

        }

        public static TipoConta RecuperarPorId(int idTipoConta)
        {
            Conexao conexao = new Conexao();

            try
            {
                conexao.AbrirConexao();

                //Comando SQL para listar todas as empresas
                string ComandoSQL = "SELECT * FROM TIPOCONTA WHERE IDTIPOCONTA = " + idTipoConta.ToString() + ";";
                conexao.Cmd = new SqlCommand(ComandoSQL, conexao.Con);
                TipoConta TipoConta = new TipoConta();
                DbDataReader Dados = conexao.ExecutarComando(conexao.Cmd);

                if (Dados.Read())
                {
                    TipoConta.IdTipoConta = Convert.ToInt32(Dados["IdTipoConta"]);
                    TipoConta.DescTipoConta = Convert.ToString(Dados["DescTipoConta"]);
                }

                Dados.Close();

                return TipoConta;
            }
            catch (Exception erro)
            {
                throw new Exception("Erro ao Recuperar a TIPOCONTA " + erro.Message);//Em caso de erro retorna a mensagem de erro
            }
            finally
            {
                conexao.FecharConexao();//Fecha a Conexão --Independentemente se der erro ou não, a conexão será fechada!
            }
        }

        public static TipoConta RecuperarPorDescTipoConta(string DescTipoConta)
        {
            Conexao conexao = new Conexao();

            try
            {
                conexao.AbrirConexao();

                //Comando SQL para listar todas as empresas
                string ComandoSQL = "SELECT * FROM TIPOCONTA WHERE DESCTIPOCONTA = '" + DescTipoConta + "';";
                conexao.Cmd = new SqlCommand(ComandoSQL, conexao.Con);
                TipoConta TipoConta = new TipoConta();
                DbDataReader Dados = conexao.ExecutarComando(conexao.Cmd);

                if (Dados.Read())
                {
                    TipoConta.IdTipoConta = Convert.ToInt32(Dados["IdTipoConta"]);
                    TipoConta.DescTipoConta = Convert.ToString(Dados["DescTipoConta"]);
                }

                Dados.Close();

                return TipoConta;
            }
            catch (Exception erro)
            {
                throw new Exception("Erro ao Recuperar a TIPOCONTA " + erro.Message);//Em caso de erro retorna a mensagem de erro
            }
            finally
            {
                conexao.FecharConexao();//Fecha a Conexão --Independentemente se der erro ou não, a conexão será fechada!
            }
        }

        public static void Gravar(TipoConta TipoConta)
        {
            Conexao conexao = new Conexao();

            conexao.AbrirConexao();

            try
            {

                //Monta o Comando SQL
                String ComandoSQL = " UPDATE TIPOCONTA SET ";
                ComandoSQL = ComandoSQL + " DESCTIPOCONTA = @VARIAVEL1, ";
                ComandoSQL = ComandoSQL + " WHERE   IDTIPOCONTA = UPPER(@VARIAVEL2) ";

                conexao.Cmd = new SqlCommand(ComandoSQL, conexao.Con);

                //Define os parametros utilizado no SQL
                conexao.Cmd.Parameters.AddWithValue("@VARIAVEL1", TipoConta.DescTipoConta);
                conexao.Cmd.Parameters.AddWithValue("@VARIAVEL2", TipoConta.IdTipoConta);

                conexao.Cmd.ExecuteNonQuery(); //Executa o comando SQL na base de dados
            }
            catch (Exception erro)
            {
                //Em caso de erro retorna a mensagem de erro
                throw new Exception("Erro ao Editar o Tipo da Conta " + erro.Message);
            }
            finally
            {
                //Fecha a Conexão --Independentemente se der erro ou não, a conexão será fechada!
                conexao.FecharConexao();
            }
        }

        public static void Incluir(TipoConta TipoConta)
        {
            Conexao conexao = new Conexao();

            conexao.AbrirConexao();

            try
            {

                //Monta o Comando SQL
                String ComandoSQL = " INSERT INTO TIPOCONTA (DESCTIPOCONTA) VALUES(@VARIAVEL1) ";

                conexao.Cmd = new SqlCommand(ComandoSQL, conexao.Con);

                //Define os parametros utilizado no SQL
                conexao.Cmd.Parameters.AddWithValue("@VARIAVEL1", TipoConta.DescTipoConta);

                conexao.Cmd.ExecuteNonQuery(); //Executa o comando SQL na base de dados
            }
            catch (Exception erro)
            {
                //Em caso de erro retorna a mensagem de erro
                throw new Exception("Erro ao Incluir o Tipo de Conta " + erro.Message);
            }
            finally
            {
                //Fecha a Conexão --Independentemente se der erro ou não, a conexão será fechada!
                conexao.FecharConexao();
            }

        }

        public static void Excluir(TipoConta TipoConta)
        {
            Conexao conexao = new Conexao();
            conexao.AbrirConexao();

            try
            {
                //Monta o Comando SQL
                String ComandoSQL = " DELETE FROM TIPOCONTA WHERE IDTIPOCONTA = UPPER(@VARIAVEL1) ";
                conexao.Cmd = new SqlCommand(ComandoSQL, conexao.Con);
                conexao.Cmd.Parameters.AddWithValue("@VARIAVEL1", TipoConta.IdTipoConta);//Define os parametros utilizado no SQL
                conexao.Cmd.ExecuteNonQuery(); //Executa o comando SQL na base de dados
            }
            catch (Exception erro)
            {
                //Em caso de erro retorna a mensagem de erro
                throw new Exception("Erro ao Excluir a Tipo de Conta " + erro.Message);
            }
            finally
            {
                //Fecha a Conexão --Independentemente se der erro ou não, a conexão será fechada!
                conexao.FecharConexao();
            }
        }
    }
}

