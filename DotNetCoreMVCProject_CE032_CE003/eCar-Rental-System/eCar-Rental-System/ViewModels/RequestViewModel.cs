using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace eCar_Rental_System.ViewModels
{
    public class RequestViewModel
    {
        [Required]
        public DateTime StartingDate { get; set; }

        [Required]
        public DateTime EndingDate { get; set; }
    }
}
