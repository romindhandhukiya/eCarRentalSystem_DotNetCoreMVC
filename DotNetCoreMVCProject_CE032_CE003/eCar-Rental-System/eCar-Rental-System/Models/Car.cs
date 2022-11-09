using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace eCar_Rental_System.Models
{
    public class Car
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public int No_Seat { get; set; }

        [Required]
        public int Rate { get; set; }

        [Required]
        public string FuelType { get; set; }

        [Required]
        public string Description { get; set; }

        [Required]
        public string PhotoPath { get; set; }
    }
}
