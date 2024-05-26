using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model.DataModels;

namespace ViewModels.VM
{
    public class AddGradeToStudentVm
    {
        public int StudentID {  get; set; }
        public int SubjectID {  get; set; }
        public GradedScale GradeValue {  get; set; }
        public int TeacherId {  get; set; }

    }
}
