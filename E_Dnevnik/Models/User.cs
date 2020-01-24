using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace E_Dnevnik.Models
{
    public class User
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Your name is required")]
        [StringLength(160)]
        public string Name { get; set; }

        [Required(ErrorMessage = "Your lst name is required")]
        [StringLength(160)]
        public string LastName { get; set; }

        [Required(ErrorMessage = "Teacher status is required")]
        public bool IsTeacher { get; set; }

        [Required(ErrorMessage = "Your email is required")]
        [EmailAddress]
        public string Email { get; set; }

        [Required(ErrorMessage = "Password is required")]
        [MinLength(8, ErrorMessage = "Minimum 8 characters required")]
        public string Password { get; set; }

        public List<int> Predmeti { get; set; }
        public User(string name, string lastName, string email, string password, bool isTeacher, List<int> predmeti = null)
        {
            Name = name;
            LastName = lastName;
            Email = email;
            Password = password;
            IsTeacher = isTeacher;
            Predmeti = predmeti;
        }
      
    }
}