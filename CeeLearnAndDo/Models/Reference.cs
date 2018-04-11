using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CeeLearnAndDo.Models
{
    public class Reference
    {
        public int Id { get; set; }

        public ApplicationUser User { get; set; }

        [Required]
        [Display(Name = "Titel")]
        public string Title { get; set; }

        [DataType(DataType.MultilineText)]
        [Display(Name = "Beschrijving")]
        public string Description { get; set; }

        [Display(Name = "URL")]
        public string URL { get; set; }

        [Required]
        [Display(Name = "Afbeelding")]
        public string ImagePath { get; set; }

        [Display(Name = "Geplaatst op")]
        public DateTime CreatedAt { get; set; }

        [Display(Name = "Gewijzigd op")]
        public DateTime UpdatedAt { get; set; }
    }
}