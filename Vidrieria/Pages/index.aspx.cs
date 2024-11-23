using System;

namespace Vidrieria.Pages
{
    public partial class index : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            // Verificar si el usuario está autenticado
            if (Session["idusuario"] == null)
            {
                // Si no está autenticado, redirigir al login
                Response.Redirect("login.aspx");
            }
            else
            {
                // Si está autenticado, redirigir al escritorio
                Response.Redirect("escritorio.aspx");
            }
        }
    }
}

