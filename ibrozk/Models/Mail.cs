using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ibrozk.Models
{
    public class Mail
    {
        [Required]
        public required string Adsoyad { get; set; }
        [Required]
        public required string Email { get; set; }
        [Required]
        public required string Telefon { get; set; }
        [Required]
        public required string Mesaj { get; set; }
    }
}