using HotelOrderFinal.Models;
using Microsoft.AspNetCore.Mvc;
using NuGet.Protocol.Plugins;
using System.Text.Json;

namespace HotelOrderFinal.Controllers
{
    public class HotelIndustryController : Controller
    {
        IWebHostEnvironment _enviro;
        public HotelIndustryController ( IWebHostEnvironment p )
        {
            _enviro = p;
        }

        public IActionResult List ( )
        {
            return View ( );
        }

        public IActionResult Edit ( int? id )
        {
            HotelOrderContext db = new HotelOrderContext ( );
            HotelIndustry Hl = db.HotelIndustry.FirstOrDefault ( t => t.HotelId == id );
            if ( Hl == null )
                return RedirectToAction ( "List" );
            return View ( Hl );
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit ( CHotelIndustryWrap p )
        {
            HotelOrderContext db = new HotelOrderContext ( );
            HotelIndustry cust = db.HotelIndustry.FirstOrDefault ( t => t.HotelId == p.HotelId );
            if ( cust != null )
            {
                if ( p.photo != null )
                {
                    string photoName = Guid.NewGuid ( ).ToString ( ) + ".jpg";
                    string path = _enviro.WebRootPath + "/image/" + photoName;
                    p.photo.CopyTo ( new FileStream ( path , FileMode.Create ) );
                    cust.HotelImage = photoName;
                }

                cust.HotelName = p.HotelName;
                cust.HotelPhone = p.HotelPhone;
                cust.HotelRegionId = p.HotelRegionId;
                cust.HotelAddress = p.HotelAddress;
                cust.HotelImageDiscription = p.HotelImageDiscription;

                db.SaveChanges ( );
            }

            return RedirectToAction ( "List" );
        }
        public IActionResult Create ( ) //創建資料
        {
            return View ( );
        }
        [HttpPost]
        public IActionResult Create ( HotelIndustry p )
        {
            HotelOrderContext db = new HotelOrderContext ( );
            db.HotelIndustry.Add ( p );
            db.SaveChanges ( );
            return RedirectToAction ( "List" );
        }
        public IActionResult Delete ( int? id )
        {
            HotelOrderContext db = new HotelOrderContext ( );
            HotelIndustry cust = db.HotelIndustry.FirstOrDefault ( t => t.HotelId == id );
            if ( id != null )
            {
                db.HotelIndustry.Remove ( cust );
                db.SaveChanges ( );
            }
            return RedirectToAction ( "List" );
        }
    }
}
