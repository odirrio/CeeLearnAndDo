using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace CeeLearnAndDo.Models
{
    public class Question
    {
        public int Id { get; set; }

       
        public ApplicationUser User { get; set; }

        [Required]
        [Display(Name = "Vraag")]
        public string Title { get; set; }

        [Required]
        [Display(Name = "Beschrijving")]
        public string Description { get; set; }

        [Display(Name = "Vraag")]
        public List<Answer> Answer { get; set; }

        [Display(Name = "Geplaatst op")]
        public DateTime CreatedAt { get; set; }

        [Display(Name = "Gewijzigd op")]
        public DateTime UpdatedAt { get; set; }

        public Question()
        {
            this.CreatedAt = DateTime.Now;
            this.UpdatedAt = DateTime.Now;
        }
    }
}