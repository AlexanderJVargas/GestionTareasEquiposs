using System.ComponentModel.DataAnnotations;

namespace GestionTareasEquipos.Modelos
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
        
        // Navegacion
        public Usuario? Usuario { get; set; }

        // Relaciones
        
    }
}
