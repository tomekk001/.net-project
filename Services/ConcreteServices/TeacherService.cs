using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Castle.Core.Logging;
using Microsoft.AspNetCore.Identity;
using SchoolRegister.EF;
using Model.DataModels;
using SchoolRegister.Services.ConcreteServices;
using Services.Interfaces;
using ViewModels.VM;
using Microsoft.Extensions.Logging;
using System.Linq.Expressions;
using SchoolRegister.ViewModels.VM;
using Microsoft.EntityFrameworkCore;

namespace Services.ConcreteServices
{
    public class TeacherService : BaseService, ITeacherService
    {
        private readonly UserManager<User> _userManager;
        public TeacherService(ApplicationDbContext dbContext, IMapper mapper, Microsoft.Extensions.Logging.ILogger<TeacherService> logger, UserManager<User> userManager) 
            : base (dbContext, mapper, logger)
        {
            _userManager = userManager;
        }


        public TeacherVm GetTeacher(Expression<Func<Teacher, bool>> filterPredicate)
        {
           try
            {
                if (filterPredicate == null)
                {
                    throw new ArgumentNullException("FilterPredicate is null");
                }

                var teacherEntity = DbContext.Users.OfType<Teacher>().FirstOrDefault();
                var teacherVm = Mapper.Map<TeacherVm>(teacherEntity);

                return teacherVm;
            }
            catch(Exception ex)
            {
                Logger.LogError(ex, ex.Message);
                throw;
            }
        }

        public IEnumerable<TeacherVm> GetTeachers(Expression<Func<Teacher, bool>>? filterPredicate = null)
        {
            try
            {
                var teacherEntities = DbContext.Users.OfType<Teacher>().AsQueryable();

                if (filterPredicate != null)
                {
                    teacherEntities = teacherEntities.Where(filterPredicate);
                }

                var teacherVms = Mapper.Map<IEnumerable<TeacherVm>>(teacherEntities);
                return teacherVms;
            }
            catch (Exception ex)
            {
                Logger.LogError(ex, ex.Message);
                throw;
            }
        }

        public IEnumerable<GroupVm> GetTeachersGroups(TeachersGroupsVm getTeachersGroups)
        {
            try
            {
                int teacherId = getTeachersGroups.TeacherId;
                var teacherSubjectsIds = DbContext.Subjects.Where(s => s.TeacherId == teacherId).Select(s => s.Id).ToList();
                var subjectGroups = DbContext.SubjectGroups.Where(sg => sg.SubjectID != null ? teacherSubjectsIds.Contains((int)sg.SubjectID) : false);
                var groups = subjectGroups.Select(sg => sg.Group).ToList();
                var groupsVm = Mapper.Map<IEnumerable<GroupVm>>(groups);
                return groupsVm;
            }
            catch (Exception ex)
            {
                Logger.LogError(ex, ex.Message);
                throw;
            }
        }
    }
}
