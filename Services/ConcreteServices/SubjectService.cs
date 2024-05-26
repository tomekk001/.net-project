using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Model.DataModels;
using SchoolRegister.EF;
using SchoolRegister.Services.Interfaces;
using SchoolRegister.ViewModels.VM;

namespace SchoolRegister.Services.ConcreteServices
{
    public class SubjectService : BaseService, ISubjectService
    {
        public SubjectService(ApplicationDbContext dbContext, IMapper mapper, ILogger<SubjectService> logger)
            : base(dbContext, mapper, logger) { }

        public SubjectVm AddOrUpdateSubject(AddOrUpdateSubjectVm addOrUpdateVm)
        {
            try
            {
                if (addOrUpdateVm == null) throw new ArgumentNullException(nameof(addOrUpdateVm), "View model parameter is null");

                var subjectEntity = Mapper.Map<Subject>(addOrUpdateVm);
                if (!addOrUpdateVm.Id.HasValue || addOrUpdateVm.Id == 0)
                    DbContext.Subjects.Add(subjectEntity);
                else
                    DbContext.Subjects.Update(subjectEntity);

                DbContext.SaveChanges();
                var subjectVm = Mapper.Map<SubjectVm>(subjectEntity);
                return subjectVm;
            }
            catch (Exception ex)
            {
                Logger.LogError(ex, "An error occurred while adding or updating a subject.");
                throw;
            }
        }

        public SubjectVm GetSubject(Expression<Func<Subject, bool>> filterExpression)
        {
            try
            {
                if (filterExpression == null) throw new ArgumentNullException(nameof(filterExpression), "Filter expression is null");

                var subjectEntity = DbContext.Subjects.FirstOrDefault(filterExpression);
                var subjectVm = Mapper.Map<SubjectVm>(subjectEntity);
                return subjectVm;
            }
            catch (Exception ex)
            {
                Logger.LogError(ex, "An error occurred while getting a subject.");
                throw;
            }
        }

        public IEnumerable<SubjectVm> GetSubjects(Expression<Func<Subject, bool>> filterExpression = null)
        {
            try
            {
                var subjectEntities = DbContext.Subjects.AsQueryable();
                if (filterExpression != null)
                    subjectEntities = subjectEntities.Where(filterExpression);

                var subjectList = subjectEntities.ToList(); // Execute query here
                var subjectVms = Mapper.Map<IEnumerable<SubjectVm>>(subjectList);
                return subjectVms;
            }
            catch (Exception ex)
            {
                Logger.LogError(ex, "An error occurred while getting subjects.");
                throw;
            }
        }
    }
}
