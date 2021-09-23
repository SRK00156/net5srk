using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core_DataAccess_KalpeshSoliya.Models
{

    public class DeptMetadata
    {
        [Display(Name = "ID")]
        [Required(ErrorMessage = "ID is Must")]
        public int DeptId { get; set; }
        [Display(Name = "Name")]
        [Required(ErrorMessage = "Department name is Must")]
        public string DeptName { get; set; }

        [Required(ErrorMessage = "Capacity is Must")]
        public int Capacity { get; set; }
    }

    public class EmpMetadata
    {
        [Display(Name = "ID")]
        [Required(ErrorMessage = "ID is Must")]
        public int EmpId { get; set; }
        [Display(Name = "Name")]
        [Required(ErrorMessage = "Name is Must")]
        public string EmpName { get; set; }
        [Display(Name = "Designation")]
        [Required(ErrorMessage = "Designation is Must")]
        public string EmpDesignation { get; set; }
        [Display(Name = "Salary")]
        [Required(ErrorMessage = "Salary is Must")]
        public decimal? EmpSalary { get; set; }
        [Required(ErrorMessage = "DeptId is Must")]
        public int? DeptId { get; set; }

    }
}
