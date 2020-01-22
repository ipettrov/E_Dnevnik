using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace E_Dnevnik.Models
{
    public class Grade
    {

        [Key]
        public int Id { get; set; }

        [Required]
        public int Subject { get; set; }

        public int FirstGrade { get; set; }
        public int FinalGrade { get; set; }
    }
}