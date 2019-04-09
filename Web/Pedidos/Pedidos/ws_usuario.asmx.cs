using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Data;
using System.Data.SqlClient;
using System.Web.Script.Serialization;
using Pedidos.App_Code;

namespace Pedidos
{
    class usuario_login
    {
        public string retorno;
        public string cod_usuario;
        public string nome;
    }

    class usuario_nova_conta
    {
        public string retorno;
        public string cod_usuario;
    }


    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.Web.Script.Services.ScriptService]

    public class ws_usuario : System.Web.Services.WebService
    {

        [WebMethod]
        public string HelloWorld()
        {
            return "Olá, Mundo";
        }

        //Validar Login de acesso
        [WebMethod]
        public void Valida_Login(String email, String senha)
        {
            cConexao C = new cConexao();
            List<usuario_login> arr = new List<usuario_login>();

            email = HttpUtility.UrlDecode(email);
            senha = HttpUtility.UrlDecode(senha);

            try
            {
                C.Conectar();
                SqlCommand cmd = new SqlCommand("", C.Conexao());

                cmd.CommandText = "SET DATEFORMAT DMY EXECUTE USUARIO_VALIDA_LOGIN @EMAIL, @SENHA, @COD_USUARIO OUT, @NOME OUT, @RETORNO OUT ";

                cmd.Parameters.Add(new SqlParameter("@EMAIL", email));
                cmd.Parameters.Add(new SqlParameter("@SENHA", senha));

                cmd.Parameters.Add(new SqlParameter("@COD_USUARIO", "0"));
                cmd.Parameters["@COD_USUARIO"].Direction = System.Data.ParameterDirection.InputOutput;
                cmd.Parameters["@COD_USUARIO"].Size = 10;

                cmd.Parameters.Add(new SqlParameter("@NOME", ""));
                cmd.Parameters["@NOME"].Direction = System.Data.ParameterDirection.InputOutput;
                cmd.Parameters["@NOME"].Size = 100;

                cmd.Parameters.Add(new SqlParameter("@RETORNO", ""));
                cmd.Parameters["@RETORNO"].Direction = System.Data.ParameterDirection.InputOutput;
                cmd.Parameters["@RETORNO"].Size = 1000;

                SqlDataReader dr;
                dr = cmd.ExecuteReader();

                usuario_login u = new usuario_login();
                u.retorno = Server.HtmlEncode(cmd.Parameters["@RETORNO"].Value.ToString());
                u.cod_usuario = Server.HtmlEncode(cmd.Parameters["@COD_USUARIO"].Value.ToString());
                u.nome = Server.HtmlEncode(cmd.Parameters["@NOME"].Value.ToString());

                arr.Add(u);

                u = null;
                cmd = null;
            }
            catch (System.IndexOutOfRangeException e)
            {
                usuario_login u_erro = new usuario_login();
                u_erro.retorno = Server.HtmlEncode("ERRO " + e.Message);
                u_erro.cod_usuario = "0";
                u_erro.nome = "";

                arr.Add(u_erro);
            }

            //Serializa em Json
            JavaScriptSerializer js = new JavaScriptSerializer();

            Context.Response.Clear();
            Context.Response.Write(js.Serialize(arr));
            Context.Response.Flush();
            Context.Response.End();

        }

        //Criar nova conta de acesso
        [WebMethod]
        public void Criar_Nova_Conta(String nome, String email, String senha)
        {
            cConexao C = new cConexao();
            List<usuario_nova_conta> arr = new List<usuario_nova_conta>();

            nome = HttpUtility.UrlDecode(nome);
            email = HttpUtility.UrlDecode(email);
            senha = HttpUtility.UrlDecode(senha);

            try
            {
                C.Conectar();
                SqlCommand cmd = new SqlCommand("", C.Conexao());

                cmd.CommandText = "SET DATEFORMAT DMY EXECUTE USUARIO_CRIAR_CONTA @NOME, @EMAIL, @SENHA, @COD_USUARIO OUT, @RETORNO OUT ";

                cmd.Parameters.Add(new SqlParameter("@NOME", nome));
                cmd.Parameters.Add(new SqlParameter("@EMAIL", email));
                cmd.Parameters.Add(new SqlParameter("@SENHA", senha));

                cmd.Parameters.Add(new SqlParameter("@COD_USUARIO", "0"));
                cmd.Parameters["@COD_USUARIO"].Direction = System.Data.ParameterDirection.InputOutput;
                cmd.Parameters["@COD_USUARIO"].Size = 10;

                cmd.Parameters.Add(new SqlParameter("@RETORNO", ""));
                cmd.Parameters["@RETORNO"].Direction = System.Data.ParameterDirection.InputOutput;
                cmd.Parameters["@RETORNO"].Size = 1000;

                SqlDataReader dr;
                dr = cmd.ExecuteReader();

                usuario_nova_conta u = new usuario_nova_conta();
                u.retorno = Server.HtmlEncode(cmd.Parameters["@RETORNO"].Value.ToString());
                u.cod_usuario = Server.HtmlEncode(cmd.Parameters["@COD_USUARIO"].Value.ToString());

                arr.Add(u);

                u = null;
                cmd = null;
            }
            catch (System.IndexOutOfRangeException e)
            {
                usuario_nova_conta u_erro = new usuario_nova_conta();
                u_erro.retorno = Server.HtmlEncode("ERRO " + e.Message);
                u_erro.cod_usuario = "0";

                arr.Add(u_erro);
            }

            //Serializa em Json
            JavaScriptSerializer js = new JavaScriptSerializer();

            Context.Response.Clear();
            Context.Response.Write(js.Serialize(arr));
            Context.Response.Flush();
            Context.Response.End();

        }
    }
}
