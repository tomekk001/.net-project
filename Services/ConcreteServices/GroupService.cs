using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Castle.Core.Logging;
using Microsoft.AspNetCore.Identity;
using Model.DataModels;
using SchoolRegister.EF;
using SchoolRegister.ViewModels.VM;
using ViewModels.VM;
using Microsoft.Extensions.Logging;
using SchoolRegister.Services.ConcreteServices;
using Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.Formatters;


namespace Services.ConcreteServices
{
    public class GroupService : BaseService, IGroupService
    {
        private readonly UserManager<User> _userManager;

        public GroupService(ApplicationDbContext dbContext, IMapper mapper, Microsoft.Extensions.Logging.ILogger<TeacherService> logger, UserManager<User> userManager)
             : base(dbContext, mapper, logger)
        {
            _userManager = userManager;
        }

        public GroupVm AddOrUpdateGroup(AddOrUpdateGroupVm addOrUpdateGroupVm)
        {
            try
            {
                if(addOrUpdateGroupVm == null)
                {
                    throw new ArgumentNullException(nameof(addOrUpdateGroupVm));
                }

                var newGroup = Mapper.Map<Group>(addOrUpdateGroupVm);

                if(!addOrUpdateGroupVm.Id.HasValue || addOrUpdateGroupVm.Id == 0)
                {
                    DbContext.Groups.Add(newGroup);
                }
                else
                {
                    DbContext.Groups.Update(newGroup);
                }
                DbContext.SaveChanges();
                var groupVm = Mapper.Map<GroupVm>(addOrUpdateGroupVm);
                return groupVm;
            }
            catch (Exception ex)
            {
                throw new Exception("Wystąpił błąd podczas dodawania lub aktualizowania grupy.", ex);

            }
        }
        public StudentVm AttachStudentToGroup(AttachDeachStudentToGroupVm attachStudentToGroupVm)
        {
            try
            {
                if(attachStudentToGroupVm == null)
                {
                    throw new ArgumentNullException();
                }

                var studentEntity = DbContext.Users.OfType<Student>().FirstOrDefault(e => e.Id == attachStudentToGroupVm.StudentId);
                var groupEntity = DbContext.Groups.FirstOrDefault(g => g.Id == attachStudentToGroupVm.StudentId);

                if(groupEntity == null || studentEntity == null)
                {
                    throw new NullReferenceException();
                }

                if(groupEntity.Students.Any(s=> s.Id == attachStudentToGroupVm.StudentId))
                {
                    throw new Exception("Student jest juz dodany do grupy");
                }

                groupEntity.Students.Add(studentEntity);
                DbContext.SaveChanges();

                var studentVm = Mapper.Map<StudentVm>(studentEntity);
                return studentVm;

            }
            catch(Exception ex)
            {
                throw new Exception("Wystąpił błąd podczas dodania studenta do groupy", ex);
            }
        }
        public GroupVm AttachSubjectToGroup(AttachDetachSubjectGroupVm attachSubjectGroupVm)
        {
            try
            {
                if(attachSubjectGroupVm == null)
                {
                    throw new ArgumentNullException();
                }

                var subjectGroup = Mapper.Map<SubjectGroup>(attachSubjectGroupVm);
                var groupEntity = DbContext.Groups.Include(g => g.SubjectGroups).FirstOrDefault(g => g.Id == attachSubjectGroupVm.GroupId);
                var subjectEntity = DbContext.Subjects.FirstOrDefault(s => s.Id == attachSubjectGroupVm.SubjectId);

                if(subjectGroup == null || groupEntity  == null || subjectEntity == null)
                {
                    throw new NullReferenceException();
                }

                subjectGroup.Subject = subjectEntity;
                groupEntity.SubjectGroups.Add(subjectGroup);
                DbContext.SaveChanges();

                var groupVm = Mapper.Map<GroupVm>(groupEntity);
                return groupVm;
              
            }
            catch(Exception ex)
            {
                throw new Exception("Nie udalo sie załączyć przedmiotu do grupy", ex);
            }
        }
        public SubjectVm AttachTeacherToSubject(AttachDetachSubjectToTeacherVm attachDetachSubjectToTeacherVm)
        {
            try
            {
                if (attachDetachSubjectToTeacherVm == null)
                {
                    throw new ArgumentNullException();
                }

                var subjectEntity = DbContext.Subjects.FirstOrDefault(s => s.Id == attachDetachSubjectToTeacherVm.SubjectId);
                var teacherEntity = DbContext.Users.OfType<Teacher>().FirstOrDefault(t => t.Id == attachDetachSubjectToTeacherVm.TeacherId);

                if(subjectEntity == null || teacherEntity == null)
                {
                    throw new NullReferenceException();
                }

                subjectEntity.Teacher = teacherEntity;
                DbContext.SaveChanges();

                var subjectVm = Mapper.Map<SubjectVm>(subjectEntity);
                return subjectVm;
            }
            catch (Exception ex)
            {
                throw new Exception("Nie udalo sie załączyć nauczyciela  do przedmiotu", ex);
            }
        }
        public StudentVm DetachStudentFromGroup(AttachDetachStudentToGroupVm detachStudentToGroupVm)
        {
            try
            {
                if(detachStudentToGroupVm == null)
                {
                    throw new ArgumentNullException();
                }
                var studentEntity = DbContext.Users.OfType<Student>().FirstOrDefault(e => e.Id == detachStudentToGroupVm.StudentId);
                var groupEntity = DbContext.Groups.FirstOrDefault(g => g.Id == detachStudentToGroupVm.GroupId);

                if(groupEntity == null || groupEntity == null)
                {
                    throw new NullReferenceException();
                }

                if(!groupEntity.Students.Any(s=> s.Id == detachStudentToGroupVm.StudentId))
                {
                    throw new Exception("Student jest już przypisany do grupy");
                }

                groupEntity.Students.Remove(studentEntity);
                studentEntity.Group = null;
                DbContext.SaveChanges();

                var studentVm = Mapper.Map<StudentVm>(studentEntity);
                return studentVm;

            }

            catch(Exception ex)
            {
                throw new Exception("Nie udalo sie usunac Studenta z groupy", ex);
            }
        }
        public GroupVm DetachSubjectFromGroup(AttachDetachSubjectGroupVm detachSubjectFromGroupVm)
        {
            try
            {
                if(detachSubjectFromGroupVm == null)
                {
                    throw new ArgumentNullException();
                }

                var subjectGroup = DbContext.SubjectGroups.FirstOrDefault(e => e.SubjectID == detachSubjectFromGroupVm.SubjectId);
                var groupEntity = DbContext.Groups.Include(g => g.SubjectGroups).FirstOrDefault(g => g.Id == detachSubjectFromGroupVm.GroupId);

                if(subjectGroup == null || groupEntity == null)
                {
                    throw new NullReferenceException();
                }

                groupEntity.SubjectGroups.Remove(subjectGroup);
                DbContext.SaveChanges();
                var groupVm = Mapper.Map<GroupVm>(groupEntity);

                return groupVm;
            }
            catch(Exception ex)
            {
                throw new Exception("Nie mozna usunac przedmiotu z grupy", ex);
            }
        }
        public SubjectVm DetachTeacherFromSubject(AttachDetachSubjectToTeacherVm attachDetachSubjectToTeacherVm)
        {
            try
            {
               if( attachDetachSubjectToTeacherVm == null)
                {
                    throw new ArgumentNullException();
                }

                var teacherEntity = DbContext.Users.OfType<Teacher>().FirstOrDefault(e => e.Id == attachDetachSubjectToTeacherVm.TeacherId);
                var subjectEntity = DbContext.Subjects.Include(s => s.Teacher).FirstOrDefault(s => s.Id == attachDetachSubjectToTeacherVm.SubjectId);

                if(subjectEntity == null || teacherEntity == null)
                {
                    throw new NullReferenceException();
                }

                if(subjectEntity.Teacher != teacherEntity)
                {
                    throw new Exception("nauczyciel nie uczy tego przedmiotu");
                }

                subjectEntity.Teacher = null;

                DbContext.SaveChanges();

                var subjectVm = Mapper.Map<SubjectVm>(subjectEntity);
                return subjectVm;
            }
            catch (Exception ex)
            {
                throw new Exception("Nie mozna usunac przedmiotu z grupy", ex);
            }
        }
        public GroupVm GetGroup(Expression<Func<Model.DataModels.Group, bool>> filterPredicate)
        {
            try
            {
                if(filterPredicate == null)
                {
                    throw new ArgumentException("filterPredicate jest rowny null");
                }

                var groupEntity = DbContext.Groups.FirstOrDefault(filterPredicate);
                var groupVm = Mapper.Map<GroupVm>(groupEntity);
                return groupVm;
            }
            catch(Exception ex)
            {
                Logger.LogError(ex, ex.Message);
                throw;
            }
        }
        public IEnumerable<GroupVm> GetGroups(Expression<Func<Model.DataModels.Group, bool>> filterPredicate = null)
        {
            try
            {
                var groupEntities = DbContext.Groups.AsQueryable();

                if(filterPredicate != null)
                {
                    groupEntities = groupEntities.Where(filterPredicate);
                }

                var groupVms = Mapper.Map<IEnumerable<GroupVm>>(groupEntities);

                return groupVms;
            }
            catch (Exception ex)
            {
                Logger.LogError(ex, ex.Message);
                throw;
            }
        }
    }
}
