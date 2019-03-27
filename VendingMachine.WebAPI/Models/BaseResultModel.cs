using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace VendingMachine.WebAPI.Models
{
    public abstract class BaseResultModel
    {
        protected BaseResultModel(string message, bool isSuccess)
        {
            Message = message;
            IsSuccess = isSuccess;
        }

        public string Message { get; private set; }
        public bool IsSuccess { get; private set; }
    }
}
