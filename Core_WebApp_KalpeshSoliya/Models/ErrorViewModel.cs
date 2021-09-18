using System;

namespace Core_WebApp_KalpeshSoliya.Models
{
    public class ErrorViewModel
    {
        public string RequestId { get; set; }

        public bool ShowRequestId => !string.IsNullOrEmpty(RequestId);
        //adding properties to show Detail of exception
        public string ControllerName { get; set; }
        public string ActionName { get; set; }
        public string ErrorMessage { get; set; }
    }
}
