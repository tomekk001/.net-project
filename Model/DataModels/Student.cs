using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Model.DataModels
{
    public class Student : User
    {
        public virtual Group? Group { get; set; }
        public int? GroupId { get; set; }
        public virtual IList<Grade>? Grades { get; set; }
        public virtual Parent? Parent { get; set; }
        public int? ParentId { get; set; }   
        public double AverageGrade {
            get
            {
                if (Grades == null || !Grades.Any())
                    return 0.0;

                return Grades.Average(grade => (int)grade.GradedValue);
            }

        }
        public IDictionary<string,double>? AverageGradePerSubject {
            get
            {
                var averageGradePerSubject = new Dictionary<string, double>();

                if (Grades == null || !Grades.Any())
                    return averageGradePerSubject;

                foreach (var grade in Grades)
                {
                    if (!averageGradePerSubject.ContainsKey(grade.Subject?.Name ?? ""))
                        averageGradePerSubject[grade.Subject?.Name ?? ""] = 0.0;

                    averageGradePerSubject[grade.Subject?.Name ?? ""] += (int)grade.GradedValue;
                }

                foreach (var subject in averageGradePerSubject.Keys.ToList())
                {
                    averageGradePerSubject[subject] /= Grades.Count(g => g.Subject?.Name == subject);
                }

                return averageGradePerSubject;
            }
        
        }  
        public IDictionary<string, List<GradedScale>> GradesPerSubject { get;
        
        }
        public Student()
        {

        }



    }
}
