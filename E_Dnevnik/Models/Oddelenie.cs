using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace E_Dnevnik.Models
{
    public class Oddelenie
    {
        [Key]
        public int Id { get; set; }

        public string Name { get; set; }

        public virtual ICollection<TeacherOddelenie> TeacherOddelenies { get; set; }
        public virtual ICollection<StudentOddelenie> StudentOddelenies { get; set; }

    }
}