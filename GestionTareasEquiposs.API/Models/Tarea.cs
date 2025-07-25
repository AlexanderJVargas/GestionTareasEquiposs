using System.ComponentModel.DataAnnotations;

namespace GestionTareasEquiposs.API.Models
{
    public class Tareas
    {
        [Key]
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public DateTime FechaCreacion { get; set; }
        public DateTime FechaLimite { get; set; }
        public bool Completada { get; set; }


        //Claves Foraneas
        public int UsuarioId { get; set; }


        //Navegacion
        public Usuario? Usuario { get; set; }

        //Relaciones

    }
}
