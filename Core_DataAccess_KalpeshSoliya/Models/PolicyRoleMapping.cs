using System;
using System.Collections.Generic;

#nullable disable

namespace Core_DataAccess_KalpeshSoliya.Models
{
    public partial class PolicyRoleMapping
    {
        public int PolicyId { get; set; }
        public string PolicyName { get; set; }
        public string RoleId { get; set; }
    }
}
