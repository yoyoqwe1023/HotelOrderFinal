using Microsoft.AspNetCore.Mvc;
using HotelOrderFinal.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace HotelOrderFinal.Controllers
{
    [Route ( "api/[controller]" )]
    [ApiController]
    public class APIHotelIndustryController : ControllerBase
    {
        // GET: api/<APIHotelIndustryController>
        [HttpGet]
        public IEnumerable<HotelIndustry> Get ( int hotelRegionId )
        {
            HotelOrderContext db = new HotelOrderContext ( );
            var datas = from HI in db.HotelIndustry
                        where hotelRegionId == HI.HotelRegionId
                        select HI;
            return datas;
        }

        // GET api/<APIHotelIndustryController>/5
        [HttpGet ( "{id}" )]
        //public string Get ( int id )
        //{
        //    return "value";
        //}

        // POST api/<APIHotelIndustryController>
        [HttpPost]
        public void Post ( [FromBody] string value )
        {
        }

        // PUT api/<APIHotelIndustryController>/5
        [HttpPut ( "{id}" )]
        public void Put ( int id , [FromBody] HotelIndustry p )
        {
 
        }

        // DELETE api/<APIHotelIndustryController>/5
        [HttpDelete ( "{id}" )]
        public void Delete ( int id )
        {
        }
    }
}
