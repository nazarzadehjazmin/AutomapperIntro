using automapperIntro.Models;
using Microsoft.AspNetCore.Mvc;

namespace automapperIntro.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DriversController : ControllerBase
    {
        private readonly ILogger<DriversController> _logger;

        //in memory database
        private static List<Driver> drivers = new List<Driver>();

        public DriversController(ILogger<DriversController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public IActionResult GetDrivers()
        {
            List<Driver> allDrivers = drivers.Where(x => x.Status == 1).ToList();

            return Ok(allDrivers);
        }

        [HttpPost]
        public IActionResult CreateDriver(Driver data)
        {
            if (ModelState.IsValid)
            {
                drivers.Add(data);
                return CreatedAtAction("GetDrivers", routeValues: new { data.Id }, value: data);
            }
            return BadRequest();
        }

        [HttpGet("{id}")]
        public IActionResult GetDrivers(Guid id)
        {
            Driver? driver = drivers.FirstOrDefault(x => x.Id == id);

            if(driver != null)
            {
                return Ok(driver);
            }

            return NotFound();
        }
        
        [HttpPut("{id}")]
        public IActionResult UpdateDriver(Guid id, Driver data)
        {
            Driver? existingDriver = drivers.SingleOrDefault(x => x.Id == id);

            if (existingDriver != null)
            {
                existingDriver.DriverNumber = data.DriverNumber;

                return Ok();
            }

            return NotFound();
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteDriver(Guid id)
        {
            Driver? existingDriver = drivers.FirstOrDefault(x => x.Id == id);

            if(existingDriver != null)
            {
                existingDriver.Status = 0;
                return Ok($"Driver deleted: {existingDriver}");
            }
            return NotFound();
        }

    }
}
