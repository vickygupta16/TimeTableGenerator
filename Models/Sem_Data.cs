using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TTG3.Models
{
    public class Sem_Data
    {
        public int SubjectID { get; set; }
        public string SubjectName { get; set; }
        public string SubjectType { get; set; }
        public int ProfessorID { get; set; }
        public string ProfessorName { get; set; }
        public int count { get; set; }
        public int SPW { get; set; }
        public int Priority { get; set; }
    }
}