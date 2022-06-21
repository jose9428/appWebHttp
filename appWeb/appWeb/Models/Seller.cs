using System.ComponentModel.DataAnnotations;

namespace appWeb.Models
{
    public class Seller
    {
        [Required(AllowEmptyStrings = true)]
        public string codigo { get; set; }
        [Required]
        public string nombre { get; set; }
        [Required]
        public string direccion { get; set; }
        [Required]
        public string idpais { get; set; }
        [Required]
        public string email { get; set; }
    }
}
