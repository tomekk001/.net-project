using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.DataModels
{
    public class Parent : User
    {
        public virtual IList<Student> ?Students { get; set; }
        public Parent()
        {

        }

    }
}
