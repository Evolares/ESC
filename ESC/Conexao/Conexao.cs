using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient; //Biblioteca de acesso ao SQL Server
using System.Configuration;

namespace ESC.Persistencia
{
    public class Conexao
    {
        //Atributos da Classe (Protected Somente de Acesso ao atributo por herança)

        public SqlConnection Con; //Estabelce a conexão com o banco
        public SqlCommand Cmd; //Executar os comandos no banco
        public SqlDataReader Dr; //Similar ao Recordsset do Visual Basic

        //Método - Abrir Conexao --------------------------------------------------------------------------------------------------------
        public void AbrirConexao()
        {
            try
            {
                string dadosConexao = ConfigurationManager.ConnectionStrings["Conexao"].ConnectionString;
                Con = new SqlConnection(dadosConexao);//Connection String
                Con.Open();
            }
            //catch (Exception ex)
            catch
            {
                //Caso apresente algum problema, apresenta na tela o problema
                throw new Exception("Erro na Conexão com o Banco de Dados");
            }
        }

        //Método - Fechar Conexao --------------------------------------------------------------------------------------------------------
        public void FecharConexao()
        {
            try
            {
                Con.Close();
            }
            catch (Exception ex)
            {
            }
        }

        public SqlDataReader ExecutarConsulta(string ComandoSQL)
        {
            Conexao Conexao = new Conexao();
            Conexao.AbrirConexao();
            Conexao.Cmd = new SqlCommand(ComandoSQL, Conexao.Con);
            Conexao.Dr = Conexao.Cmd.ExecuteReader();
            return Conexao.Dr;
        }

        public SqlDataReader ExecutarComando(SqlCommand Comando)
        {

            return Comando.ExecuteReader();
        }
    }
}