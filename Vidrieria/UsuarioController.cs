using System;
using System.Data.SqlClient;
using System.Web.Http;

namespace Vidrieria.Controllers
{
    public class UsuarioController : ApiController
    {
        // POST: api/usuario/verificar
        [HttpPost]
        [Route("api/usuario/verificar")]
        public IHttpActionResult Verificar([FromBody] LoginRequest request)
        {
            if (request == null || string.IsNullOrWhiteSpace(request.logina) || string.IsNullOrWhiteSpace(request.clavea))
            {
                return BadRequest("Faltan datos para el inicio de sesión.");
            }

            using (SqlConnection conn = new SqlConnection("Data Source=201.159.103.122;Initial Catalog=tecrisi;Integrated Security=True"))
            {
                try
                {
                    conn.Open();
                    string query = "SELECT idusuario, nombre FROM usuario WHERE logina = @logina AND clavea = @clavea";
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@logina", request.logina);
                        cmd.Parameters.AddWithValue("@clavea", request.clavea);

                        SqlDataReader reader = cmd.ExecuteReader();
                        if (reader.Read())
                        {
                            return Ok(new
                            {
                                idusuario = reader["idusuario"],
                                nombre = reader["nombre"]
                            });
                        }
                        else
                        {
                            return Unauthorized(); // Usuario no autorizado
                        }
                    }
                }
                catch (Exception ex)
                {
                    return InternalServerError(ex); // Error del servidor
                }
            }
        }
    }

    // Clase para recibir los datos del login
    public class LoginRequest
    {
        public string logina { get; set; }
        public string clavea { get; set; }
    }
}
