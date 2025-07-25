using Microsoft.AspNetCore.Mvc;
using System.Data.Common;
using Microsoft.Data.SqlClient;
using GestionTareasEquipos.Modelos;
using Dapper;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace GestionTareasEquiposs.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TareasController : ControllerBase
    {
        private readonly DbConnection connection;
        
        public TareasController(IConfiguration config)
        {
            var connectionString = config.GetConnectionString("DefaultConnection");
            connection = new SqlConnection(connectionString);
            connection.Open();


        }
        // GET: api/<TareasController>
        [HttpGet]
        public IEnumerable<Tareas> Get()
        {
            var tareas = connection.Query<Tareas>("SELECT * FROM Tareas").ToList();
            return tareas;
        }

        // GET api/<TareasController>/5
        [HttpGet("{id}")]
        public Tareas Get(String id)
        {
            var tarea = connection.QuerySingle<Tareas>("SELECT * FROM Tareas WHERE Id = @Id", new { Id = id });
            return tarea;

        }

        // POST api/<TareasController>
        [HttpPost]
        public Tareas Post([FromBody] Tareas tareas)
        {
            connection.Execute("INSERT INTO Tareas (Nombre, Descripcion, FechaCreacion, FechaLimite, Completada, UsuarioId, ProyectoId) " +
                "VALUES (@Nombre, @Descripcion, @FechaCreacion, @FechaLimite, @Completada, @UsuarioId, @ProyectoId)",
                new
                {
                    Nombre = tareas.Nombre,
                    Descripcion = tareas.Descripcion,
                    FechaCreacion = tareas.FechaCreacion,
                    FechaLimite = tareas.FechaLimite,
                    Completada = tareas.Completada,
                    UsuarioId = tareas.UsuarioId,
                    ProyectoId = tareas.ProyectoId
                });
            return tareas;

        }

        // PUT api/<TareasController>/5
        [HttpPut("{id}")]
        public Tareas Put(int id, [FromBody] Tareas tareas)
        {
            connection.Execute("UPDATE Tareas SET Nombre = @Nombre, Descripcion = @Descripcion, FechaCreacion = @FechaCreacion, " +
                "FechaLimite = @FechaLimite, Completada = @Completada, UsuarioId = @UsuarioId, ProyectoId = @ProyectoId WHERE Id = @Id",
                new
                {
                    Id = id,
                    Nombre = tareas.Nombre,
                    Descripcion = tareas.Descripcion,
                    FechaCreacion = tareas.FechaCreacion,
                    FechaLimite = tareas.FechaLimite,
                    Completada = tareas.Completada,
                    UsuarioId = tareas.UsuarioId,
                    ProyectoId = tareas.ProyectoId
                });
            return tareas;

        }

        // DELETE api/<TareasController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            connection.Execute("DELETE FROM Tareas WHERE Id = @Id", new { Id = id });
        }
    }
}
