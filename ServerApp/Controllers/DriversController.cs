using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using ServerApp.Models;
using ServerApp.Models.DTO.Incoming;
using ServerApp.Models.DTO.Outgoing;

namespace ServerApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DriversController : ControllerBase
    {
        private DataContext _context;

        private readonly IMapper _mapper;

        public DriversController(DataContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        [HttpGet]
        public IActionResult GetDrivers()
        {
            var drivers = _context.Drivers.Where(x => x.Status == 1).ToList();
            var _drivers = _mapper.Map<IEnumerable<DriverForReturnDto>>(drivers);
            return Ok(_drivers);
        }

        [HttpPost]
        public IActionResult CreateDriver(DriverForCreation data)
        {
            if (ModelState.IsValid)
            {
                // this implementation has been specified in the mapper profile - code is no longer needed
                //var driver = new Driver()
                //{
                //Id = Guid.NewGuid(),
                //Status = 1,
                //DateAdded = DateTime.Now,
                //DriverNumber = data.DriverNumber,
                //FirstName = data.FirstName,
                //LastName = data.LastName,
                //WorldChampionship = data.WorldChampionship
                //};
                var driver = _mapper.Map<Driver>(data); // takes care of the mapping 
                _context.Drivers.Add(driver);
                _context.SaveChanges();

                var _driverToReturn = _mapper.Map<DriverForReturnDto>(driver);
                return CreatedAtAction("GetDriver", new { driver.Id }, _driverToReturn);
                //return CreatedAtAction("GetDriver", new { driver.Id }, data);
            }

            return new JsonResult("Something went wrong") { StatusCode = 500 };


            // public IActionResult CreateDriver(Driver data)
            //if (ModelState.IsValid)
            //{
            //    _context.Drivers.Add(data);
            //    _context.SaveChanges();
            //    return CreatedAtAction("GetDriver", new { data.Id }, data);
            //}
            //return new JsonResult("Something went wrong") { StatusCode = 500 };
        }

        [HttpGet]
        [Route("{id:Guid}")]
        public IActionResult GetDriver([FromRoute] Guid id)
        {
            var driver = _context.Drivers.FirstOrDefault(x => x.Id == id);
            //if (driver == null)
            //    return NotFound();
            return Ok(driver);
        }
        
        [HttpPut]
        public IActionResult UpdateDriver(Guid id, Driver data)
        {
            if (id != data.Id)
                return BadRequest();

            var existingDriver = _context.Drivers.FirstOrDefault(x => x.Id == id);
            if (existingDriver == null)
                return BadRequest();

            existingDriver.DriverNumber = data.DriverNumber;
            existingDriver.FirstName = data.FirstName;
            existingDriver.LastName = data.LastName;
            existingDriver.WorldChampionship = data.WorldChampionship;
            _context.Drivers.Update(existingDriver);
            _context.SaveChanges();
            return NoContent(); // update created succcessfully  
        }

        [HttpDelete("{id:Guid}")]
        public async Task<IActionResult> DeleteDriver(Guid id)
        {
            var existingDriver = await _context.Drivers.FindAsync(id);
            if (existingDriver == null)
                return BadRequest();
            _context.Drivers.Remove(existingDriver);
            _context.SaveChanges();
            return NoContent();
        }   
    }
}