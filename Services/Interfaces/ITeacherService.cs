using Model.DataModels;
using SchoolRegister.Services.ConcreteServices;
using SchoolRegister.ViewModels.VM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using ViewModels.VM;

namespace Services.Interfaces
{
    public interface ITeacherService
    {
        public TeacherVm GetTeacher(Expression<Func<Teacher, bool>> filterPredicate);

        IEnumerable<TeacherVm> GetTeachers(Expression<Func<Teacher, bool>> filterPredicate = null);

        IEnumerable<GroupVm> GetTeachersGroups(TeachersGroupsVm getTeachersGroups);
        
    }
}
