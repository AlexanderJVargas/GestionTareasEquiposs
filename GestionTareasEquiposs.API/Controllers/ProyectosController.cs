using Dapper;
using Microsoft.AspNetCore.Mvc;
using System.Data.Common;
using Microsoft.Data.SqlClient;
using GestionTareasEquipos.Modelos;


// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace GestionTareasEquiposs.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProyectosController : ControllerBase
    {
        private readonly DbConnection connection;

        public ProyectosController(IConfiguration config)
        {
            var connectionString = config.GetConnectionString("DefaultConnection");
            connection = new SqlConnection(connectionString);

        }
        // GET: api/<ProyectosController>
        [HttpGet]
        public IEnumerable<Proyecto> Get()
        {
            var proyectos = connection.Query<Proyecto>("SELECT * FROM Proyectos").ToList();
            return proyectos;


        }

        // GET api/<ProyectosController>/5
        [HttpGet("{id}")]
        public Proyecto Get(int id)
        {
            var proyecto = connection.QuerySingleOrDefault<Proyecto>("SELECT * FROM Proyectos WHERE Id = @Id", new { Id = id });
            return proyecto;
        }

        // POST api/<ProyectosController>
        [HttpPost]
        public Proyecto Post([FromBody] Proyecto proyecto)
        {
            connection.Execute("INSERT INTO Proyectos (Nombre, Descripcion, FechaCreacion, FechaLimite, esCompletado, UsuarioId) " +
                "VALUES (@Nombre, @Descripcion, @FechaCreacion, @FechaLimite, @esCompletado, @UsuarioId)",
                new
                {
                    Nombre = proyecto.Nombre,
                    Descripcion = proyecto.Descripcion,
                    FechaCreacion = proyecto.FechaCreacion,
                    FechaLimite = proyecto.FechaLimite,
                    esCompletado = proyecto.esCompletado,
                    UsuarioId = proyecto.UsuarioId
                });
            return proyecto;
        }

        // PUT api/<ProyectosController>/5
        [HttpPut("{id}")]
        public Proyecto Put(int id, [FromBody] Proyecto proyecto)
        {
            connection.Execute("UPDATE Proyectos SET Nombre = @Nombre, Descripcion = @Descripcion, FechaCreacion = @FechaCreacion, " +
                "FechaLimite = @FechaLimite, esCompletado = @esCompletado, UsuarioId = @UsuarioId WHERE Id = @Id",
                new
                {
                    Id = id,
                    Nombre = proyecto.Nombre,
                    Descripcion = proyecto.Descripcion,
                    FechaCreacion = proyecto.FechaCreacion,
                    FechaLimite = proyecto.FechaLimite,
                    esCompletado = proyecto.esCompletado,
                    UsuarioId = proyecto.UsuarioId,
                    
                });
            return proyecto;

        }

        // DELETE api/<ProyectosController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            connection.Execute("DELETE FROM Proyectos WHERE Id = @Id", new { Id = id });
        }
    }
}
