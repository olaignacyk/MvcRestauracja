using System.ComponentModel.DataAnnotations;

namespace MvcRestauracja.Models
{
    public class Klient
    {
        [Key]
        public int Id { get; set; }
        [Display(Name = "Imię")]
        public String Imie { get; set; }
        [Display(Name = "Nazwisko")]
        public String Nazwisko { get; set; }

        [DataType(DataType.Date)]
        [Display(Name = "Data zamowienia")]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime DataZamowienia { get; set; }
        public ICollection<Danie> ?Dania { get; set; } = new List<Danie>();
        public Stolik? Stolik { get; set; }

  

    }
}