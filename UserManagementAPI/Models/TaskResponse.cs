using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UserManagementAPI.Models
{
    public class TaskResponse
    {
        public TaskResponse()
        {

        }
        public TaskResponse(bool isSuccess, string message)
        {
            IsSuccess = isSuccess;
            SystemMessage = message;
        }
        public bool IsSuccess { get; set; }
        public string SystemMessage { get; set; }
    }
}
