using System;
using System.Threading.Tasks;

namespace VendingMachine.BLL.Interfaces
{
    public interface IEmailService
    {
        Task SendAsync(string email, string subject, string message);
        Task SendConfirmEmailLinkAsync(Guid id, string email, string code);
    }
}
