using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;

namespace Pedidos.App_Code
{
    public class cConexao
    {
        SqlConnection con;
        string servidor = "localhost";
        string banco = "Pedidos";
        string usuario = "sa";
        string senha = "Ceo1991@";
        string strcon = "";

        public void Conectar()
        {
            strcon = "Data Source=" + servidor + ";Initial Catalog=" + banco + ";User Id=" + usuario + ";Password=" + senha + ";";
            con = new SqlConnection(strcon);
            con.Open();
        }

        public void Fechar()
        {
            con.Close();
        }

        public System.Data.SqlClient.SqlConnection Conexao()
        {
            return con;
        }
    }
}