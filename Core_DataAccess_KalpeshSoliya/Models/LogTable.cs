using System;
using System.Collections.Generic;

#nullable disable

namespace Core_DataAccess_KalpeshSoliya.Models
{
    public partial class LogTable
    {
        public int LogId { get; set; }
        public string CurrentLoginName { get; set; }
        public DateTime? RequestDateTime { get; set; }
        public string ControllerName { get; set; }
        public string ActioName { get; set; }
        public string ErrorMessage { get; set; }
    }
}
