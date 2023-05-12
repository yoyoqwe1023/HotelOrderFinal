using HotelOrderFinal.Models;
using HotelOrderFinal.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace HotelOrderFinal.Controllers
{
    public class RoomController : Controller
    {
        private IWebHostEnvironment _enviro;
        public RoomController(IWebHostEnvironment p)
        {
            _enviro = p;
        }
        public IActionResult List()
        {
            HotelOrderContext db = new HotelOrderContext();
            var datas = from c in db.RoomClass
                        select c;
            return View(datas);
        }
      
       
        public IActionResult Detail(string id)
        {

            HotelOrderContext db = new HotelOrderContext();
            var room = db.RoomClass.FirstOrDefault(r => r.RoomClassId == id);
            if (room == null)
            {
                return View("List");
            }

            var facilities = db.MultipleRoomFacility
                .Where(mrf => mrf.RoomClassId == id)
                .Select(mrf => mrf.Facility)
                .ToList();

            var viewModel = new CRoomClassViewModel
            {
                RoomClass = room,
                Facility = facilities
            };

            return View(viewModel);
        }

        public IActionResult Edit(string id)
        {
            HotelOrderContext db = new HotelOrderContext();
            RoomClass room = db.RoomClass.FirstOrDefault(t => t.RoomClassId == id);
            if (room == null)
                return RedirectToAction("List");
            return View(room);
        }
        [HttpPost]
        public IActionResult Edit(CRoomClassWrap p)
        {
            HotelOrderContext db = new HotelOrderContext();
            RoomClass room = db.RoomClass.FirstOrDefault(t => t.RoomClassId == p.RoomClassId);
            if (room != null)
            {
                if (p.photo != null)
                {
                    string photoName = Guid.NewGuid().ToString() + ".jpg";
                    string path = _enviro.WebRootPath + "/image/" + photoName;
                    p.photo.CopyTo(new FileStream(path, FileMode.Create));
                    room.RoomClassPhoto3 = photoName;
                }

                room.RoomClassName = p.RoomClassName;
                room.RoomClassDetail = p.RoomClassDetail;
                room.WeekdayPrice = p.WeekdayPrice;
                room.HolidayPrice = p.HolidayPrice;
                room.AddPrice = p.AddPrice;
                
                db.SaveChanges();
            }
            return RedirectToAction("List");
        }

        public IActionResult SearchRooms()
        {
            HotelOrderContext db = new HotelOrderContext();
            var datas = from c in db.RoomClass
                        select c;
            return View(datas);

        }

    }
}
