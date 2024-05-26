using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Model.DataModels;
using SchoolRegister.ViewModels.VM;

namespace Services.Interfaces
{
    public interface IStudentService
    {
        StudentVm GetStudent(Expression<Func<Student, bool>> filterPredicate);
        IEnumerable<StudentVm> GetStudents(Expression<Func<Student, bool>> filterPredicate = null);

    }
}
