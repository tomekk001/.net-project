using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.DataModels
{
    public class SubjectGroup
    {
        public virtual Subject Subject { get; set; }
        public int SubjectID {  get; set; }
        public virtual Group Group { get; set; }
        public int GroupID { get; set; }
        public SubjectGroup()
        {

        }
    }
}
