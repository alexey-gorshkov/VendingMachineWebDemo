using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using VendingMachine.BLL.DTO;
using VendingMachine.DAL.Entities;

namespace VendingMachine.BLL.Interfaces
{
    public interface IVendingMachineService
    {
        Task<VendingMachineStateDTO> GetUserStateAsync(User customer);
    }
}
