using Microsoft.EntityFrameworkCore;
using Model.DataModels;
using SchoolRegister.EF;
using SchoolRegister.Tests.UnitTests;
using Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ViewModels.VM;

namespace Tests.UnitTests
{
    public class GradeServiceUnitTests : BaseUnitTests
    {
        private readonly IGradeService _gradeService;
        public GradeServiceUnitTests(ApplicationDbContext dbContext, IGradeService gradeService) : base(dbContext)
        {
            _gradeService = gradeService;
        }

        [Fact]
        public void AddGradeToStudent()
        {
            var gradeVm = new AddGradeToStudentVm
            {
                StudentID = 5,
                SubjectID = 1,
                GradeValue = GradedScale.DB,
                TeacherId = 1
            };

            var grade = _gradeService.AddGradeToStudent(gradeVm);
            Assert.NotNull(grade);
            Assert.Equal(2, DbContext.Grades.Count());
        }

        [Fact]
        public void GetGradesReportForStudentByTeacher()
        {
            var getGradesReportForStudent = new GetGradesReportVm
            {
                StudentId = 5,
                GetterUserId = 1
            };

            var gradesReport = _gradeService.GetGradesReportForStudent(getGradesReportForStudent);
            Assert.NotNull(gradesReport);
        }

        [Fact]
        public void GetGradesReportForStudentByStudent()
        {
            var getGradesReportForStudent = new GetGradesReportVm
            {
                StudentId = 5,
                GetterUserId = 5
            };

            var gradesReport = _gradeService.GetGradesReportForStudent(getGradesReportForStudent);
            Assert.NotNull(gradesReport);
        }

        [Fact]
        public void GetGradesReportForStudentByParent()
        {
            var getGradesReportForStudent = new GetGradesReportVm
            {
                StudentId = 5,
                GetterUserId = 3
            };

            var gradesReport = _gradeService.GetGradesReportForStudent(getGradesReportForStudent);
            Assert.NotNull(gradesReport);
        }
    }
}
