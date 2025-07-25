using Microsoft.AspNetCore.Mvc;
using Dapper;
using System.Data.Common;
using Microsoft.Data.SqlClient;
using GestionTareasEquipos.Modelos;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace GestionTareasEquiposs.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuarioController : ControllerBase
    {
        private readonly DbConnection connection;

        public UsuarioController(IConfiguration config)
        {
            var connectionString = config.GetConnectionString("DefaultConnection");
            connection = new SqlConnection(connectionString);
            connection.Open();
        }
        // GET: api/<UsuarioController>
        [HttpGet]
        public IEnumerable<Usuario> Get()
        {
            var usuarios = connection.Query<Usuario>("SELECT * FROM Usuarios").ToList();
            return usuarios;

        }

        // GET api/<UsuarioController>/5
        [HttpGet("{id}")]
        public Usuario Get(String id)
        {
            var usuario = connection.QuerySingle<Usuario>("SELECT * FROM Usuarios WHERE Id = @Id", new { Id = id });
            return usuario;
        }

        // POST api/<UsuarioController>
        [HttpPost]
        public Usuario Post([FromBody] Usuario usuario)
        {
            connection.Execute("INSERT INTO Usuarios (Nombre, Email, Password, FechaCreacion) " +
                "VALUES (@Nombre, @Email, @Password, @FechaCreacion)",
                new
                {
                    Nombre = usuario.Nombre,
                    Email = usuario.Email,
                    Password = usuario.Password,
                    FechaCreacion = DateTime.Now



                });
            return usuario;

        }

        // PUT api/<UsuarioController>/5
        [HttpPut("{id}")]
        public Usuario Put(int id, [FromBody] Usuario usuario)
        {
            connection.Execute("UPDATE Usuarios SET Nombre = @Nombre, Email = @Email, Password = @Password WHERE Id = @Id",
                new
                {
                    Id = id,
                    Nombre = usuario.Nombre,
                    Email = usuario.Email,
                    Password = usuario.Password
                });
            return usuario;
        }

        // DELETE api/<UsuarioController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            connection.Execute("DELETE FROM Usuarios WHERE Id = @Id", new { Id = id });
        }
    }
}
