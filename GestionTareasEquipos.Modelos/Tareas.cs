using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestionTareasEquipos.Modelos
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
        public string Prioridad { get; set; } // Ejemplo: "Alta", "Media", "Baja"
        public string Estado { get; set; }


        //Claves Foraneas
        public int UsuarioId { get; set; }
        public int ProyectoId { get; set; }


        //Navegacion
        public Usuario? Usuario { get; set; }
        public Proyecto? Proyecto { get; set; }

        //Relaciones

    }
}
