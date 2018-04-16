using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace CeeLearnAndDo.Models
{
    public class Answer
    {
        public int Id { get; set; }

        public ApplicationUser User { get; set; }

        [Required]
        [Display(Name = "Antwoord")]
        public string Content { get; set; }

        [Display(Name = "Goedgekeurd")]
        public bool Approved { get; set; }

        public Question Question { get; set; }

        [Display(Name = "Vraag")]
        [ForeignKey("Question")]
        public int QuestionId { get; set; }

        [Display(Name = "Geplaatst op")]
        public DateTime CreatedAt { get; set; }

        [Display(Name = "Gewijzigd op")]
        public DateTime UpdatedAt { get; set; }
    }
}