using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace VendingMachine.WebAPI.Models
{
    public class TokenResult
    {
        public TokenResult(string token, int expiresIn, string message = "")
        {
            Token = token;
            ExpiresIn = expiresIn;
            Message = message;
            IsSuccess = true;
        }

        public TokenResult(string message)
        {
            IsSuccess = false;
        }

        public string Token { get; set; }
        // seconds
        public int ExpiresIn { get; set; }
        public string Message { get; set; }
        public bool IsSuccess { get; set; }
    }
}
