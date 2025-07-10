using Microsoft.AspNetCore.Mvc;
using webApiSimulation.Models;

namespace webApiSimulation.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MachinesController : ControllerBase
    {
        private static readonly List<Machine> _machineData = new()
        {
            new Machine { Abbreviation = "DRS_0001", IsRunning = true, PowerConsumption = 0, DI1 = false, DI2 = false, Timestamp = DateTime.UtcNow },
            new Machine { Abbreviation = "DRS_0002", IsRunning = true, PowerConsumption = 0, DI1 = false, DI2 = false, Timestamp = DateTime.UtcNow },
            new Machine { Abbreviation = "DRS_0003", IsRunning = true, PowerConsumption = 0, DI1 = false, DI2 = false, Timestamp = DateTime.UtcNow },
            new Machine { Abbreviation = "DRS_0004", IsRunning = true, PowerConsumption = 0, DI1 = false, DI2 = false, Timestamp = DateTime.UtcNow },
        };


        [HttpGet]
        public ActionResult<List<Machine>> GetAll()
        {
            return Ok(_machineData);
        }

        [HttpPost]
        public ActionResult Add([FromBody] List<Machine> machines)
        {
            if (machines == null || machines.Count == 0)
                return BadRequest("No machine data received.");

            _machineData.Clear();
            _machineData.AddRange(machines);
            return Ok();
        }

        [HttpGet("{abbreviation}")]
        public ActionResult<Machine> GetByAbbreviation(string abbreviation)
        {
            var machine = _machineData.FirstOrDefault(m => m.Abbreviation == abbreviation);
            if (machine == null)
                return NotFound($"Machine with abbreviation '{abbreviation}' not found.");

            return Ok(machine);
        }
    }
}
