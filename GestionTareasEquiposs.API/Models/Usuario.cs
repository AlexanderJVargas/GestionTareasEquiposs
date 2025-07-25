using System.ComponentModel.DataAnnotations;

namespace GestionTareasEquiposs.API.Models
{
    public class Usuario
    {
        [Key]
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }

        //Claves Foraneas

    }
}
