using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace E_Dnevnik.Models
{       
    public class E_DnevnikModel
    {       // Nema da se koristat site promenlivi tuka vo zavisnost dali e teacher ili student ke se koristat del od promenlivite 
        public Student Student { get; set; }    //Koriste se ako e student
        public Teacher Teacher { get; set; }    //Koriste se ako e teacher    
        public bool IsTeacher  { get; set; }    // Koriste se vo 2 slucaevi za da se znae role
        public List<Subject> Subjects { get; set; } //Koriste se vo slucaj da e teacher za da se znaat predmetite so gi predava
        public List<StudentSubject> StudentSubjects { get; set; }   // Koriste se u slucaj da e student site predmeti so gi slusa (koriste se StudentSubject ne samo Subjects oti tuka gi ima i ocenkite !)
        public Dictionary<Subject, List<Student>> SubjectStudentsDictionary { get; set; } // Koriste se u slucaj da e teacher za da ja imame listata koj student koj predmet go slusa za da moze teacher da stave ocenka !

        public double Prosek { get; set; }
    }
}