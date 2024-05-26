using Microsoft.EntityFrameworkCore;
using SchoolRegister.EF;
using SchoolRegister.Tests.UnitTests;
using Services.Interfaces;
using ViewModels.VM;

namespace Tests.UnitTests
{
    public class TeacherServiceUnitTests : BaseUnitTests
    {
        private readonly ITeacherService _teacherService;
        public TeacherServiceUnitTests(ApplicationDbContext dbContext, ITeacherService teacherService) : base(dbContext)
        {
            _teacherService = teacherService;
        }

        [Fact]
        public void GetTeacher()
        {
            var teacher = _teacherService.GetTeacher(x => x.UserName == "t1@eg.eg");
            Assert.NotNull(teacher);
        }

        [Fact]
        public void GetTeachers()
        {
            var teachersTest = DbContext.Users.ToList();
            var teachers = _teacherService.GetTeachers(x => x.UserName.Contains("@eg.eg"));
            Assert.NotNull(teachers);
            Assert.NotEmpty(teachers);
            Assert.Equal(3, teachers.Count());
        }

        [Fact]
        public void GetAllTeachers()
        {
            var teachers = _teacherService.GetTeachers();
            Assert.NotNull(teachers);
            Assert.NotEmpty(teachers);
            Assert.Equal(3, teachers.Count());
        }

        [Fact]
        public void GetTeachersGroups()
        {
            var getTeachersGroup = new TeachersGroupsVm
            {
                TeacherId = 1
            };

            var teacherGroups = _teacherService.GetTeachersGroups(getTeachersGroup);
            Assert.NotNull(teacherGroups);
            Assert.NotEmpty(teacherGroups);
            Assert.Equal(5, teacherGroups.Count());
        }
    }
}