using HotelOrderFinal.Models;
using Microsoft.AspNetCore.Mvc;


namespace HotelOrderFinal.Controllers
{
    public class MemberMangeController : Controller
    {
        //public IActionResult List()
        //{
        //    ViewBag.SelectedItemContent = "會員管理的內容";
        //    HotelOrderContext db = new HotelOrderContext();
        //    var datas = from c in db.RoomMember
        //                select c;
        //    return View(datas);
        //}

        public IActionResult List(int pageNumber = 1, int pageSize = 10)
        {
            ViewBag.SelectedItemContent = "會員管理的內容";
            HotelOrderContext db = new HotelOrderContext();

            var totalItems = db.RoomMember.Count();
            var totalPages = (int)Math.Ceiling((double)totalItems / pageSize);

            var pagedData = db.RoomMember
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToList();

            ViewBag.TotalPages = totalPages;
            ViewBag.CurrentPage = pageNumber;

            return View(pagedData);
        }

        public IActionResult Edit()
        {
            return View();
        }
        public IActionResult Delete()
        {
            return View();
        }


    }
}
