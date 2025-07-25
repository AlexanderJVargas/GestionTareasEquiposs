using Microsoft.AspNetCore.Mvc;
using System.Data.Common;
using Microsoft.Data.SqlClient;
using GestionTareasEquipos.Modelos;
using Dapper;

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
        public Tareas Get(int id)
        {
            var tarea = connection.QuerySingle<Tareas>("SELECT * FROM Tareas WHERE Id = @Id", new { Id = id });
            return tarea;
        }

        // POST api/<TareasController>
        [HttpPost]
        public Tareas Post([FromBody] Tareas tareas)
        {
            connection.Execute("INSERT INTO Tareas (Nombre, Descripcion, FechaCreacion, FechaLimite, Completada, Prioridad, Estado, UsuarioId, ProyectoId) " +
                "VALUES (@Nombre, @Descripcion, @FechaCreacion, @FechaLimite, @Completada, @Prioridad, @Estado, @UsuarioId, @ProyectoId)",
                new
                {
                    Nombre = tareas.Nombre,
                    Descripcion = tareas.Descripcion,
                    FechaCreacion = tareas.FechaCreacion,
                    FechaLimite = tareas.FechaLimite,
                    Completada = tareas.Completada,
                    Prioridad = tareas.Prioridad,
                    Estado = tareas.Estado,
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
                "FechaLimite = @FechaLimite, Completada = @Completada, Prioridad = @Prioridad, Estado = @Estado, UsuarioId = @UsuarioId, ProyectoId = @ProyectoId WHERE Id = @Id",
                new
                {
                    Id = id,
                    Nombre = tareas.Nombre,
                    Descripcion = tareas.Descripcion,
                    FechaCreacion = tareas.FechaCreacion,
                    FechaLimite = tareas.FechaLimite,
                    Completada = tareas.Completada,
                    Prioridad = tareas.Prioridad,
                    Estado = tareas.Estado,
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

        [HttpGet("filtrar")]
        public IEnumerable<Tareas> GetTareasFiltradas([FromQuery] string? nombre = null, [FromQuery] string? estado = null, [FromQuery] string? prioridad = null)
        {
            var query = "SELECT * FROM Tareas WHERE 1=1";
            if (!string.IsNullOrEmpty(nombre))
            {
                query += " AND Nombre LIKE @Nombre";
            }
            if (!string.IsNullOrEmpty(estado))
            {
                query += " AND Estado = @Estado";
            }
            if (!string.IsNullOrEmpty(prioridad))
            {
                query += " AND Prioridad = @Prioridad";
            }
            return connection.Query<Tareas>(query, new { Nombre = $"%{nombre}%", Estado = estado, Prioridad = prioridad }).ToList();
        }

        // GET api/Tareas/usuario/5
        [HttpGet("usuario/{usuarioId}")]
        public IEnumerable<Tareas> GetTareasByUsuario(int usuarioId)
        {
            var query = "SELECT * FROM Tareas WHERE UsuarioId = @UsuarioId";
            return connection.Query<Tareas>(query, new { UsuarioId = usuarioId }).ToList();
        }

        // GET api/Tareas/proyecto/5
        [HttpGet("proyecto/{proyectoId}")]
        public IEnumerable<Tareas> GetTareasByProyecto(int proyectoId)
        {
            var query = "SELECT * FROM Tareas WHERE ProyectoId = @ProyectoId";
            return connection.Query<Tareas>(query, new { ProyectoId = proyectoId }).ToList();
        }
    }
}