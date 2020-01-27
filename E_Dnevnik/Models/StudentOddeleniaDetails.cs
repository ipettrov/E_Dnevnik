using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace E_Dnevnik.Models
{
    public class StudentOddeleniaDetails
    {
        public string Name { get; set; }
        public List<StudentOddelenie> StudentOddelenies { get; set; }
        public List<StudentSubject> StudentSubjects { get; set; }
    }
}