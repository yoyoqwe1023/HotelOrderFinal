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
        //現在這個寫法是display全部datas，沒有做分類，要思考一下分類作法
        //找demo的查詢應該是一個分類方向
        //0507:可以透過where條件強制做分類處理，現在綁死為1
        {
            //HotelOrderContext db = new HotelOrderContext ( );
            //var datas = from HI in db.HotelIndustry
            //            where HI.HotelRegionId == 1
            //            select HI;
            //return View ( datas );
            return View ();
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

        //    public string demoObjtoJson ( )
        //    {
        //        HotelIndustry db = new HotelIndustry ( )
        //        {
        //            HotelId = 1 ,
        //            HotelName = "華泰蔚藍飯店" ,
        //            HotelPhone = "(02) 6531-6582" ,
        //            HotelAddress = "台北市復興南路一段390號" ,
        //            HotelImage = "524efbc9-7b42-4f36-8b07-4b6581b1600b.jpg" ,
        //            HotelImageDiscription = "華泰蔚藍飯店照片(128173663.jpg)" ,
        //            HotelRegionId = 1 ,
        //        };
        //        string json = JsonSerializer.Serialize ( db );
        //        return json;
        //    }
        //    public string demoJsontoObj ( )
        //    {
        //        string json = demoObjtoJson ( );
        //        HotelIndustry db = JsonSerializer.Deserialize<HotelIndustry> ( json );
        //        return db.HotelId + "<br/>" + db.HotelName + "<br/>"+  db.HotelPhone + "<br/>" + db.HotelImage;
        //    }
        //}
    }
}
