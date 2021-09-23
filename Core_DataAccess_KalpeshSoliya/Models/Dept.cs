using System;
using System.Collections.Generic;

#nullable disable

namespace Core_DataAccess_KalpeshSoliya.Models
{
    public partial class Dept
    {
        public Dept()
        {
            Emps = new HashSet<Emp>();
        }

        public int DeptId { get; set; }
        public string DeptName { get; set; }
        public int Capacity { get; set; }

        public virtual ICollection<Emp> Emps { get; set; }
    }
}
