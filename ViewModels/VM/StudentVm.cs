using System.ComponentModel.DataAnnotations;
using Model.DataModels;

namespace SchoolRegister.ViewModels.VM;

public class StudentVm
{
    public virtual IList<Grade>? Grades { get; set; }
    public virtual Parent? ParentName { get; set; }
    public int? ParentId { get; set; }
    public virtual Group? GroupName { get; set; }
    public int? GroupId { get; set; }
}