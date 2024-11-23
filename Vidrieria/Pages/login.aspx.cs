using System;
using System.Data.SqlClient;
using System.Web.UI;

namespace Vidrieria.Pages
{
    public partial class login : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            // Si el usuario ya está autenticado, redirigir al escritorio
            if (Session["idusuario"] != null)
            {
                Response.Redirect("escritorio.aspx");
            }
        }

        protected void btnIngresar_Click(object sender, EventArgs e)
        {
            string usuario = txtUsuario.Text.Trim();
            string clave = txtClave.Text.Trim();

            if (string.IsNullOrEmpty(usuario) || string.IsNullOrEmpty(clave))
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "alert('Por favor, completa todos los campos.');", true);
                return;
            }

            using (SqlConnection conn = new SqlConnection("Data Source=TU_SERVIDOR;Initial Catalog=VidrieriaDB;Integrated Security=True"))
            {
                conn.Open();
                string query = "SELECT idusuario, nombre FROM usuarios WHERE logina = @usuario AND clavea = @clave";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@usuario", usuario);
                    cmd.Parameters.AddWithValue("@clave", clave);

                    SqlDataReader reader = cmd.ExecuteReader();
                    if (reader.Read())
                    {
                        Session["idusuario"] = reader["idusuario"];
                        Session["nombre"] = reader["nombre"];

                        Response.Redirect("escritorio.aspx");
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(this, GetType(), "alert", "alert('Usuario o contraseña incorrectos.');", true);
                    }
                }
            }
        }
    }
}
