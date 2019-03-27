using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace VendingMachine.WebAPI.Models
{
    public class TokenResultModel : BaseResultModel
    {
        public TokenResultModel(string token, int expiresIn, string message = "") : base(message, true)
        {
            Token = token;
            ExpiresIn = expiresIn;
        }

        public TokenResultModel(string message, bool isSuccess = false) : base(message, isSuccess)
        {
        }

        public string Token { get; set; }
        // seconds
        public int ExpiresIn { get; set; }
    }
}
