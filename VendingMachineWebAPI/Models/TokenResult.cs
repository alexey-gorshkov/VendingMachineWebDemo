using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace VendingMachine.WebAPI.Models
{
    public class TokenResult
    {
        public TokenResult(string message, string token = "", bool isSuccess = false)
        {
            Token = token;
            Message = message;
            IsSuccess = isSuccess;
        }

        public string Token { get; set; }
        public string Message { get; set; }
        public bool IsSuccess { get; set; }
    }
}
