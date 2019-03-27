using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace VendingMachine.WebAPI.Models
{
    public class RegisterResultModel : BaseResultModel
    {
        public RegisterResultModel(string token, int expiresIn, string message = "") : base(message, true)
        {
            Token = token;
            ExpiresIn = expiresIn;
        }

        public RegisterResultModel(string message, bool isSuccess = false) : base (message, isSuccess)
        {
        }

        public string Token { get; set; }
        // seconds
        public int ExpiresIn { get; set; }
    }
}
