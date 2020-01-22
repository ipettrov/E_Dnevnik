using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace E_Dnevnik.Models
{
    public class Subject
    {

        [Key]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

    }
}