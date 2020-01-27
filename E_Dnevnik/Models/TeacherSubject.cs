using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace E_Dnevnik.Models
{
    public class TeacherSubject
    {
        [Key, Column(Order = 2)]
        public int TeacherId { get; set; }
        [Key, Column(Order = 3)]
        public int SubjectId { get; set; }

        public virtual Teacher Teacher { get; set; }
        public virtual Subject Subject { get; set; }

    }
}