using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.DataModels
{
    public class Teacher : User
    {
        public virtual IList<Subject> ?Subjects { get; set; }
        public string ?Title { get; set; }
        public Teacher()
        {
            Subjects = new List<Subject>();
            Title = "tytul";
        }
    }
}
