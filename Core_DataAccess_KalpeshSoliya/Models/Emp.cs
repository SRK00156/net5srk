using System;
using System.Collections.Generic;

#nullable disable

namespace Core_DataAccess_KalpeshSoliya.Models
{
    public partial class Emp
    {
        public int EmpId { get; set; }
        public string EmpName { get; set; }
        public string EmpDesignation { get; set; }
        public decimal? EmpSalary { get; set; }
        public int? DeptId { get; set; }

        public virtual Dept Dept { get; set; }
    }
}
