using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using VendingMachine.BLL;
using VendingMachine.BLL.DTO;

namespace VendingMachine.WebAPI.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class VendingMachineController : ControllerBase
    {
        private readonly IMapper _mapper;

        public VendingMachineController(IMapper mapper)
        {
            _mapper = mapper;
        }

        // GET: api/VendingMachine
        [HttpGet]
        public VendingMachineDTO Get()
        {
            //  VendingMachineService vendingMachineMy = new VendingMachineService();
            //Session["VendingMachineMy"] = vendingMachineMy;

            // return _mapper.Map<VendingMachineService, VendingMachineDTO>(vendingMachineMy);
            return null;
        }

        // GET: api/VendingMachine/5
        [HttpGet("{id}", Name = "Get")]
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/VendingMachine
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT: api/VendingMachine/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
