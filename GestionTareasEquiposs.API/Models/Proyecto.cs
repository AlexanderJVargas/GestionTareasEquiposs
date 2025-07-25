using System.ComponentModel.DataAnnotations;
using System.Threading;

namespace GestionTareasEquiposs.API.Models
{
    public class Proyecto
    {
        [Key]
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }

        public DateTime FechaCreacion { get; set; }
        public DateTime FechaLimite { get; set; }
        public bool esCompletado { get; set; }

        // Claves Foraneas
        public int UsuarioId { get; set; }
        public int TareaId { get; set; }
        // Navegacion
        public Usuario? Usuario { get; set; }
        public Tareas? Tarea { get; set; }
        // Relaciones
    }
}
