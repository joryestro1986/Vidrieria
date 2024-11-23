using System;
using System.Data.SqlClient;
using System.Web.Mvc;
using System.Security.Cryptography;
using System.Text;

public class UsuarioController : Controller
{
    // Método para encriptar la contraseña con SHA256
    private string EncriptarClave(string clave)
    {
        using (SHA256 sha256Hash = SHA256.Create())
        {
            byte[] bytes = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(clave));
            StringBuilder builder = new StringBuilder();
            for (int i = 0; i < bytes.Length; i++)
            {
                builder.Append(bytes[i].ToString("x2"));
            }
            return builder.ToString();
        }
    }

    // Acción para autenticar al usuario
    [HttpPost]
    public JsonResult Verificar(string logina, string clavea)
    {
        if (string.IsNullOrEmpty(logina) || string.IsNullOrEmpty(clavea))
        {
            return Json(new { success = false, message = "Por favor, completa todos los campos." });
        }

        try
        {
            string claveEncriptada = EncriptarClave(clavea);
            using (SqlConnection conn = new SqlConnection("tu_cadena_de_conexion"))
            {
                conn.Open();
                string query = "SELECT idusuario, nombre, logina, estado FROM usuario WHERE logina = @logina AND clave = @clave AND estado = 'Activo'";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@logina", logina);
                    cmd.Parameters.AddWithValue("@clave", claveEncriptada);

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            Session["idusuario"] = reader["idusuario"];
                            Session["nombre"] = reader["nombre"];
                            return Json(new { success = true, message = "Acceso concedido." });
                        }
                        else
                        {
                            return Json(new { success = false, message = "Usuario o contraseña incorrectos." });
                        }
                    }
                }
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine("Error en login: " + ex.Message);
            return Json(new { success = false, message = "Error interno en el servidor." });
        }
    }
}
