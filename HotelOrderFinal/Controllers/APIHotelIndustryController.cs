using Microsoft.AspNetCore.Mvc;
using HotelOrderFinal.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace HotelOrderFinal.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class APIHotelIndustryController : ControllerBase
    {
        // GET: api/APIHotelIndustry/get
        // https://localhost:7007/api/APIHotelIndustry/get
        [Route("[action]")]
        [HttpGet]
        public IEnumerable<HotelIndustry> Get()
        {
            HotelOrderContext db = new HotelOrderContext();
            return db.HotelIndustry;
        }

        // GET: api/APIHotelIndustry/get/1
        // https://localhost:7007/api/APIHotelIndustry/get/1
        [Route("[action]/{hotelRegionId}")]
        [HttpGet]
        public IEnumerable<HotelIndustry> Get(int hotelRegionId)
        {
            HotelOrderContext db = new HotelOrderContext();
            return db.HotelIndustry.Where((x) => x.HotelRegionId == hotelRegionId);
        }
        
        // GET: api/APIHotelIndustry/get?hotelRegionId=1
        // https://localhost:7007/api/APIHotelIndustry/get?hotelRegionId=1
        //[Route("[action]")]
        //[HttpGet]
        //public IEnumerable<HotelIndustry> Get (int hotelRegionId)
        //{
        //    HotelOrderContext db = new HotelOrderContext();
        //    return db.HotelIndustry.Where((x) => x.HotelRegionId == hotelRegionId);
        //}

        // GET api/<APIHotelIndustryController>/5
        [HttpGet("{id}")]
        //public string Get ( int id )
        //{
        //    return "value";
        //}

        //POST api/<APIHotelIndustryController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<APIHotelIndustryController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] HotelIndustry p)
        {

        }

        // DELETE api/<APIHotelIndustryController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
