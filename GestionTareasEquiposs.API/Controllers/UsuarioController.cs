using Microsoft.AspNetCore.Mvc;
using Dapper;
using System.Data.Common;
using System.Data.SqlClient;
using GestionTareasEquiposs.API.Models;

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
            var usuario = connection.QuerySingleOrDefault<Usuario>("SELECT * FROM Usuarios WHERE Id = @Id", new { Id = id });
            return usuario;
        }

        // POST api/<UsuarioController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<UsuarioController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<UsuarioController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
