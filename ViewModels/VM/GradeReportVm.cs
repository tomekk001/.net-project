using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model.DataModels;

namespace ViewModels.VM
{
    public class GradeReportVm
    {
        public IEnumerable<GradeVm> Grades { get; set; }
    }
}
