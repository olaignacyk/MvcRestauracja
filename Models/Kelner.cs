using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MvcRestauracja.Models
{
    public class Kelner
    {
        [Key]
        public int Id { get; set; }

        [Display(Name = "Imię")]
        public string Imie { get; set; }

        [Display(Name = "Nazwisko")]
        public string Nazwisko { get; set; }

        [DataType(DataType.Date)]
        [Display(Name = "Data zatrudnienia")]
        public DateTime DataZatrudnienia { get; set; }
        public ICollection<Stolik> ?Stoliki { get; set; } = new List<Stolik>();
    }
}
