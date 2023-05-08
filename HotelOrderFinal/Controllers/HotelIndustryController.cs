using HotelOrderFinal.Models;
using Microsoft.AspNetCore.Mvc;
using NuGet.Protocol.Plugins;

namespace HotelOrderFinal.Controllers
{
    public class HotelIndustryController : Controller
    {
        IWebHostEnvironment _enviro;
        public HotelIndustryController(IWebHostEnvironment p)
        {
            _enviro = p;
        }

        public IActionResult List()
        {
            HotelOrderContext db = new HotelOrderContext();
            var datas = from HI in db.HotelIndustry
                        select HI;
            return View(datas);
        }
        public IActionResult Edit(int? id)
        {
            HotelOrderContext db = new HotelOrderContext();
            HotelIndustry Hl = db.HotelIndustry.FirstOrDefault(t => t.HotelId == id);
            if (Hl == null)
                return RedirectToAction("List");
            return View(Hl);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(CHotelIndustryWrap p)
        {
            HotelOrderContext db = new HotelOrderContext();
            HotelIndustry cust = db.HotelIndustry.FirstOrDefault(t => t.HotelId == p.HotelId);
            if (cust != null)
            {
                if (p.photo != null)
                {
                    string photoName = Guid.NewGuid().ToString() + ".jpg";
                    string path = _enviro.WebRootPath + "/image/" + photoName;
                    p.photo.CopyTo(new FileStream(path, FileMode.Create));
                    cust.HotelImage = photoName;
                }

                cust.HotelName = p.HotelName;
                cust.HotelPhone = p.HotelPhone;
                cust.HotelRegionId = p.HotelRegionId;
                cust.HotelAddress = p.HotelAddress;
                cust.HotelImageDiscription = p.HotelImageDiscription;

                db.SaveChanges();
            }

            return RedirectToAction("List");
        }
    }
}
