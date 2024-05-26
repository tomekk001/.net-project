using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ViewModels.VM;

namespace Services.Interfaces
{
    public interface IGradeService
    {
        GradeVm AddGradeToStudent(AddGradeToStudentVm addGradeToStudentVm);

        GradeReportVm GetGradesReportForStudent(GetGradesReportVm getGradesVm);

    }
}
