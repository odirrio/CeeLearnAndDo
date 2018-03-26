using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace CeeLearnAndDo.Models
{
    public class Media
    {
        public int Id { get; set; }

        [Required]
        [Display(Name = "Titel")]
        public string Title { get; set; }

        [Display(Name = "Pad")]
        public string Path { get; set; }

        public Article Article { get; set; }

        [Required]
        [Display(Name = "Artikel")]
        [ForeignKey("Article")]
        public int ArticleId { get; set; }

        [Display(Name = "Geplaatst op")]
        public DateTime CreatedAt { get; set; }

        [Display(Name = "Gewijzigd op")]
        public DateTime UpdatedAt { get; set; }
    }
}