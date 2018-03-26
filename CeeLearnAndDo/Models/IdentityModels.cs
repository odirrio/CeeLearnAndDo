using System.Data.Entity;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.ComponentModel.DataAnnotations;

namespace CeeLearnAndDo.Models
{
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit https://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public class ApplicationUser : IdentityUser
    {
        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
            return userIdentity;
        }
        [Display(Name = "Voornaam")]
        public string Firstname { get; set; }

        [Display(Name = "Tussenvoegsel")]
        public string Middlename { get; set; }

        [Required]
        [Display(Name = "Achternaam")]
        public string Lastname { get; set; }

        [Display(Name = "Aangemaakt op")]
        public DateTime CreatedAt { get; set; }

        [Display(Name = "Gewijzigd op")]
        public DateTime UpdatedAt { get; set; }
}

    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
        }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }

        public DbSet<Article> Articles { get; set; }
        public DbSet<Reaction> Reactions { get; set; }
        public DbSet<Media> Media { get; set; }

        public DbSet<Question> Questions { get; set; }
        public DbSet<Answer> Answers { get; set; }

        public DbSet<Reference> References { get; set; }

        public DbSet<HomeContent> HomeContent { get; set; }
        public DbSet<ContactContent> ContactContent { get; set; }
        public DbSet<QuestionsContent> QuestionsContent { get; set; }
        public DbSet<ReferencesContent> ReferencesContent { get; set; }
    }
}