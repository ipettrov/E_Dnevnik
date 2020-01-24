using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace E_Dnevnik.Models
{
    public class Teacher
    {
        [Key]
        public int Id { get; set; }

        public string Name { get; set; }

        public string Email { get; set; }

        public virtual ICollection<TeacherSubject> TeacherSubjects { get; set; }

        public Teacher()
        {

        }
    }
}