using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.DataModels
{
    public class Grade
    {
        public DateTime DateOfIssue { get; set; }
        public GradedScale ?GradedValue { get; set; }
        public virtual Subject? Subject { get; set; }   
        public int SubjectID { get; set; }
        public int StudentID {  get; set; } 
        public virtual Student ?Student { get; set; }
        public Grade()
        {

        }
    }
}
