using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

#nullable disable

namespace Core.DataAccess.Models
{
    public partial class Employee
    {
        [Required(ErrorMessage = "EmpNo is Must")]
        public int EmpNo { get; set; }
        [Required(ErrorMessage = "EmpName is Must")]
        public string EmpName { get; set; }
        [Required(ErrorMessage = "Designation is Must")]
        public string Designation { get; set; }
        [Required(ErrorMessage = "DeptNo is Must")]
        public int DeptNo { get; set; }

        public virtual Department DeptNoNavigation { get; set; }
    }
}
