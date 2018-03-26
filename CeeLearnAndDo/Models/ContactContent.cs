using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CeeLearnAndDo.Models
{
    public class ContactContent
    {
        public int Id { get; set; }

        public ApplicationUser User { get; set; }

        [Required]
        [Display(Name = "Titel")]
        public string Title { get; set; }

        [Display(Name = "Subtitel")]
        public string SubTitle { get; set; }

        [Display(Name = "Bedrijfsnaam")]
        public string CompanyName { get; set; }

        [Display(Name = "Adres")]
        public string Address { get; set; }

        [Display(Name = "Postcode")]
        public string Postcode { get; set; }

        [Display(Name = "Plaatsnaam")]
        public string City { get; set; }

        [Display(Name = "Telefoonnummer")]
        public string PhoneNumber { get; set; }

        [Display(Name = "E-mailadres")]
        public string Email { get; set; }

        [Display(Name = "Geplaatst op")]
        public DateTime CreatedAt { get; set; }

        [Display(Name = "Gewijzigd op")]
        public DateTime UpdatedAt { get; set; }
    }
}