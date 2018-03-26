using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace CeeLearnAndDo.Models
{
    public class Reaction
    {
        public int Id { get; set; }

        public ApplicationUser User { get; set; }

        [Required]
        [Display(Name = "Reactie")]
        public string Content { get; set; }
        
        public Article Article { get; set; }

        [Required]
        [Display(Name = "Artikel")]
        [ForeignKey("Article")]
        public int ArticleId { get; set; }

        [Display(Name = "Aangemaakt op")]
        public DateTime CreatedAt { get; set; }

        [Display(Name = "Gewijzigd op")]
        public DateTime UpdatedAt { get; set; }

    }
}