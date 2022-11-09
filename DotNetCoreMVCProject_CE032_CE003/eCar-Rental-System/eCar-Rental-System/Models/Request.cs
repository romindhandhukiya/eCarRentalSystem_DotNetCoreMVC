using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace eCar_Rental_System.Models
{
    public class Request
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        public DateTime StartingDate { get; set; }

        [Required]
        public DateTime EndingDate { get; set; }

        [Required]
        public string Status { get; set; }

        public virtual IdentityUser user { get; set; }
        public virtual Car car { get; set; }
    }
}
