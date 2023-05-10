using HotelOrderFinal.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Globalization;

namespace HotelOrderFinal.Controllers
{
    public class OrderController : Controller
    {
        public IActionResult Index()
        {
            //讀取與設定入住退房日
            string checkInDateStr = HttpContext.Session.GetString("CHECKINDATE");
            string checkOutDateStr = HttpContext.Session.GetString("CHECKOUTDATE");

            DateTime checkIn;
            DateTime checkOut;

            if (!string.IsNullOrEmpty(checkInDateStr) && !string.IsNullOrEmpty(checkOutDateStr))
            {
                checkIn = DateTime.ParseExact(checkInDateStr, "yyyy-MM-dd", CultureInfo.InvariantCulture);
                checkOut = DateTime.ParseExact(checkOutDateStr, "yyyy-MM-dd", CultureInfo.InvariantCulture);
            }
            else
            {
                checkIn = DateTime.Today;
                checkOut = DateTime.Today.AddDays(1);
                HttpContext.Session.SetString("CHECKINDATE", checkIn.ToString("yyyy-MM-dd"));
                HttpContext.Session.SetString("CHECKOUTDATE", checkOut.ToString("yyyy-MM-dd"));
            }

            ViewBag.CheckInDate = checkIn;
            ViewBag.CheckOutDate = checkOut;

            //HotelOrderContext db = new HotelOrderContext();

            ////查詢空閒房間方法
            //// 查詢指定時間區間內已被預訂的房間
            //var reservedRooms = from od in db.OrderDetail
            //                    where !(od.CheckOutDate <= checkIn || od.CheckInDate >= checkOut)
            //                    select od.RoomId;

            ////所有房間扣掉已預訂房間
            //var freeRooms = from r in db.Room
            //                where !reservedRooms.Contains(r.RoomId)
            //                select r;

            //if (freeRooms == null)
            //{
            //    //html不顯示列表顯示沒有房間文字;
            //    return View();
            //}
            //else
            //{
            //    return View(freeRooms);
            //}

            return View();
        }

        public IActionResult Detail()
        {
            if (HttpContext.Session.GetString("UserID") == null)
            {
                return RedirectToAction("Login", "Member");
            }
            return View();
        }

        public IActionResult Create()
        {
            
            return View();
        }

        public IActionResult ShowOrder()
        {
            return View();
        }
    }
}
