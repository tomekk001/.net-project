using Microsoft.EntityFrameworkCore;
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
    public class GroupServiceUnitTests : BaseUnitTests
    {
        private readonly IGroupService _groupService;
        public GroupServiceUnitTests(ApplicationDbContext dbContext, IGroupService groupService) : base(dbContext)
        {
            _groupService = groupService;
        }

        [Fact]
        public void GetGroup()
        {
            var addedGroup = _groupService.GetGroup(x => x.Name == "PAI");
            Assert.NotNull(addedGroup);
        }

        [Fact]
        public void GetGroups()
        {
            var groups = _groupService.GetGroups(x => x.Id >= 1 && x.Id <= 2).ToList();

            Assert.NotNull(groups);
            Assert.NotEmpty(groups);
            Assert.Equal(2, groups.Count());
        }

        [Fact]
        public void GetAllGroups()
        {
            var groups = _groupService.GetGroups().ToList();

            Assert.NotNull(groups);
            Assert.NotEmpty(groups);
            Assert.Equal(DbContext.Groups.Count(), groups.Count());
        }


        [Fact]
        public void AddGroup()
        {
            var addOrUpdateGroupVm = new AddOrUpdateGroupVm
            {
                Name = "SK"
            };

            _groupService.AddOrUpdateGroup(addOrUpdateGroupVm);
            Assert.Equal(4, DbContext.Groups.Count());
            var addedGroup = _groupService.GetGroup(x => x.Name == "SK");
            Assert.NotNull(addedGroup);
        }

        [Fact]
        public void UpdateGroup()
        {
            var addOrUpdateGroupVm = new AddOrUpdateGroupVm
            {
                Name = "SIiDM",
                Id = 3
            };

            _groupService.AddOrUpdateGroup(addOrUpdateGroupVm);
            var addedGroup = _groupService.GetGroup(x => x.Name == "SIiDM");
            Assert.NotNull(addedGroup);
        }

   

        [Fact]
        public void DetachStudentFromGroup()
        {
            var detachStudentToGroupVm = new AttachDetachStudentToGroupVm()
            {
                GroupId = 2,
                StudentId = 7
            };
            var student = _groupService.DetachStudentFromGroup(detachStudentToGroupVm);
            Assert.NotNull(student);
            Assert.Null(student.GroupName);
        }

        [Fact]
        public void AttachSubjectToGroup()
        {
            var attachSubjectGroupVm = new AttachDetachSubjectGroupVm()
            {
                GroupId = 1,
                SubjectId = 4
            };
            _groupService.AttachSubjectToGroup(attachSubjectGroupVm);
            var group = _groupService.GetGroup(g => g.Id == attachSubjectGroupVm.GroupId);
            Assert.NotNull(group);
            Assert.NotNull(group.Subjects.FirstOrDefault(s => s.Name == "Administracja Intenetowymi Systemami Baz Danych"));
        }

        [Fact]
        public void DetachSubjectFromGroup()
        {
            var detachSubjectGroupVm = new AttachDetachSubjectGroupVm()
            {
                GroupId = 2,
                SubjectId = 4
            };
            var group = _groupService.DetachSubjectFromGroup(detachSubjectGroupVm);
            Assert.NotNull(group);
            Assert.Null(group.Subjects.FirstOrDefault(s => s.Name == "Administracja Intenetowymi Systemami Baz Danych"));
        }

        [Fact]
        public void AttachTeacherToSubject()
        {
            var attachSubjectTeacher = new AttachDetachSubjectToTeacherVm()
            {
                SubjectId = 5,
                TeacherId = 2
            };
            var subject = _groupService.AttachTeacherToSubject(attachSubjectTeacher);
            Assert.NotNull(subject);
            Assert.True(subject.TeacherId == attachSubjectTeacher.TeacherId);
        }

        [Fact]
        public void DetachTeacherToSubject()
        {
            var detachSubjectTeacher = new AttachDetachSubjectToTeacherVm()
            {
                SubjectId = 3,
                TeacherId = 2
            };
            var subject = _groupService.DetachTeacherFromSubject(detachSubjectTeacher);
            Assert.NotNull(subject);
            Assert.Null(subject.TeacherId);
            Assert.Null(subject.TeacherName);
        }

    }
}
