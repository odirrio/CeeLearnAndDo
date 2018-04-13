using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CeeLearnAndDo.Models
{
    public class Article
    {
        public int Id { get; set; }

        public ApplicationUser User { get; set; }

        [Required]
        [Display(Name = "Titel")]
        public string Title { get; set; }

        public List<Category> Category { get; set; }

        [DataType(DataType.MultilineText)]
        [Required]
        [Display(Name = "Beschrijving")]
        public string Description { get; set; }

        [DataType(DataType.MultilineText)]
        [Required]
        [AllowHtml]
        [Display(Name = "Content")]
        public string Content { get; set; }

        [Required]
        [Display(Name = "Afbeeldingspad")]
        public string ImagePath { get; set; }

        [Display(Name = "Goedgekeurd")]
        public bool Approved { get; set; }

        [Display(Name = "Geplaatst op")]
        public DateTime CreatedAt { get; set; }

        [Display(Name = "Gewijzigd op")]
        public DateTime UpdatedAt { get; set; }
    }
}