using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model.DataModels;

namespace ViewModels.VM
{
    public class TeacherVm
    {
        public IList<Subject>? Subjects { get; set; }
        public string? Title { get; set; }

        public TeacherVm()
        {
            Subjects = new List<Subject>();
            Title = "tytul";
        }
    }
}
