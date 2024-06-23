using System.ComponentModel.DataAnnotations;

namespace MvcRestauracja.Models
{
    public class Stolik
    {
        [Key]
        public int Id { get; set; }
        [Display(Name = "Nazwa")]
        public string Nazwa {get; set; }


        [Display(Name = "Ilość nakryc")]
        public decimal Nakrycie { get; set; }


    }
}