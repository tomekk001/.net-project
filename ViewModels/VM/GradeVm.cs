using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model.DataModels;

namespace ViewModels.VM
{
    public class GradeVm
    {
        public DateTime DateOfIssue { get; set; }
        public GradedScale? GradedValue { get; set; }
        public int SubjectID { get; set; }
        public int StudentID { get; set; }
        public virtual Student? Student { get; set; }
    }
}
