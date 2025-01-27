﻿using System;
using System.Collections.Generic;

#nullable disable

namespace CS_EFCore_DbFirst.Models
{
    public partial class Employee
    {
        public int EmpNo { get; set; }
        public string EmpName { get; set; }
        public string Designation { get; set; }
        public int DeptNo { get; set; }

        public virtual Department DeptNoNavigation { get; set; }
    }
}
