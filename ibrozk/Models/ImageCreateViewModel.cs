using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ibrozk.Models
{
    public class ImageCreateViewModel
    {
        [Required]
        [Display(Name = "Sütun No (1,2,3)")]
        public int SutunNo { get; set; }

        [Required]
        [Display(Name = "Tag")]
        public string? Tag { get; set; }

        [Required(ErrorMessage = "Bir fotoğraf seçiniz.")]
        [Display(Name = "Fotoğraf")]
        [DataType(DataType.Upload)]
        public IFormFile? Photo { get; set; }
    }
}