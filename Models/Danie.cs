using System.ComponentModel.DataAnnotations;

namespace MvcRestauracja.Models
{
    public class Danie
    {
        [Key]
        public int Id { get; set; }
        [Display(Name = "Nazwa")]
        public String Nazwa { get; set; }
        [Display(Name = "Cena")]
        public decimal Cena { get; set; }

        
    }
}