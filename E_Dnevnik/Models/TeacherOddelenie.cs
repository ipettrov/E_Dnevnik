using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace E_Dnevnik.Models
{
    public class TeacherOddelenie
    {
        [Key, Column(Order = 1)]
        public int TeacherId { get; set; }
        [Key, Column(Order = 2)]
        public int OddelenieId { get; set; }

        public virtual Teacher Teacher { get; set; }
        public virtual Oddelenie Oddelenie { get; set; }

    }
}