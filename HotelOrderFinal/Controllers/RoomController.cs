using HotelOrderFinal.Models;
using HotelOrderFinal.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace HotelOrderFinal.Controllers
{
    public class RoomController : Controller
    {
        public IActionResult List()
        {
            HotelOrderContext db = new HotelOrderContext();
            var datas = from c in db.RoomClass
                        select c;
            return View(datas);
        }
        public IActionResult ListTest()
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
        public IActionResult SearchRooms()
        {
            HotelOrderContext db = new HotelOrderContext();
            var datas = from c in db.RoomClass
                        select c;
            return View(datas);

        }

    }
}
