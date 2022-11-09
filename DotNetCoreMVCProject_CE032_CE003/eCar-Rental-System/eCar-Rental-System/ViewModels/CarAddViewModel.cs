using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace eCar_Rental_System.ViewModels
{
    public class CarAddViewModel
    {
        [Required]
        [Display(Name="Car Name")]
        public string Name { get; set; }

        [Required]
        [Display(Name = "No. Of Seat")]
        public int No_Seat { get; set; }

        [Required]
        public int Rate { get; set; }

        [Required]
        public string FuelType { get; set; }

        [Required]
        public string Description { get; set; }

        [Required]
        public IFormFile Photo { get; set; }
    }
}
