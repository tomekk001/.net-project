using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Castle.Core.Logging;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Model.DataModels;
using SchoolRegister.EF;
using SchoolRegister.Services.ConcreteServices;
using Services.Interfaces;
using ViewModels.VM;

namespace Services.ConcreteServices
{
    public class GradeService : BaseService, IGradeService
    {
        private readonly UserManager<User> _userManager;

        public GradeService(ApplicationDbContext dbContext, IMapper mapper, Microsoft.Extensions.Logging.ILogger<TeacherService> logger, UserManager<User> userManager) : base(dbContext, mapper, logger)
        {
            _userManager = userManager;
        }
        public GradeVm AddGradeToStudent(AddGradeToStudentVm addGradeToStudentVm)
        {
            try
            {
                var newGrade = new Grade()
                {
                    DateOfIssue = DateTime.Now,
                    GradedValue = addGradeToStudentVm.GradeValue,
                    StudentID = addGradeToStudentVm.StudentID
                };
                DbContext.Grades.Add(newGrade);
                DbContext.SaveChanges();
                var gradeVm = Mapper.Map<GradeVm>(newGrade);
                return gradeVm;
            }
            catch(Exception ex)
            {
                Logger.LogError(ex, ex.Message);
                throw;

            }
        }

        public GradeReportVm GetGradesReportForStudent(GetGradesReportVm getGradesVm)
        {
            try
            {
                var getterUser = DbContext.Users.FirstOrDefault(u => u.Id == getGradesVm.GetterUserId);

                if (getGradesVm.StudentId == getGradesVm.GetterUserId
                    || getterUser is Teacher
                    || getterUser is Parent && ((Parent)getterUser).Students.Any(s => s.Id == getGradesVm.StudentId))
                    {
                    var gradeEntities = DbContext.Grades.Where(g => g.StudentID == getGradesVm.StudentId).ToList();
                    var gradeVms = Mapper.Map<IEnumerable<GradeVm>>(gradeEntities);

                    return new GradeReportVm()
                    {
                        Grades = gradeVms
                    };
                    }
                else
                {
                    throw new UnauthorizedAccessException("You are not allowed to view grades");
                }

                }
            
            catch(Exception ex)
            {
                Logger.LogError(ex, ex.Message);
                throw;
            }
        }

    }
}
