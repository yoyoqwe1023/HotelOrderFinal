using HotelOrderFinal.Models;
using HotelOrderFinal.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using NuGet.Frameworks;
using NuGet.Protocol;
using System.Globalization;
using System.Text.Json;
using JsonSerializer = System.Text.Json.JsonSerializer;

namespace HotelOrderFinal.Controllers
{
    public class OrderController : Controller
    {
        public IHttpContextAccessor _contextAccessor;

        public OrderController(IHttpContextAccessor contextAccessor)
        {
            this._contextAccessor = contextAccessor;
        }

        List<CShopCartViewModel> _cart = new List<CShopCartViewModel>();

        public IActionResult List()
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

            HotelOrderContext db = new HotelOrderContext();

            //查詢空閒房間方法
            //查詢指定時間區間內已被預訂的房間
            var reservedRooms = from od in db.OrderDetail
                                where !(od.CheckOutDate <= checkIn || od.CheckInDate >= checkOut)
                                select od.RoomId;
            var reservedRoomList = reservedRooms.ToList();

            //所有房間扣掉已預訂房間

            //var freeRooms = (from r in db.Room.Include(r => r.RoomClass)
            //                 where !reservedRooms.Contains(r.RoomId)
            //                 select r).Distinct();

            //var freeRooms = (from r in db.Room.Include(r => r.RoomClass)
            //                 where !reservedRooms.Contains(r.RoomId)
            //                 select r)
            //                 .Select(rs => new {rs.RoomClass.RoomClassId, rs.RoomClass.RoomClassName }).Distinct();

            //var freeRooms = from r in db.Room.Include(r => r.RoomClass).AsEnumerable()
            //                where !reservedRooms.Contains(r.RoomId)
            //                group r by r.RoomClass.RoomClassName into g
            //                select new { g.Key, g };

            var freeRooms = db.Room.Include(r => r.RoomClass).AsEnumerable()
                .Where(r => !reservedRoomList.Contains(r.RoomId))
                .GroupBy(r => r.RoomClass.RoomClassName)
                .Select(g => new { g.Key, g }).ToList();

            List<CSearchRoomViewModel> vmList = new List<CSearchRoomViewModel>();

            foreach (var room in freeRooms)
            {
                var f = room.g.First();

                CSearchRoomViewModel vm = new CSearchRoomViewModel();
                vm.RoomClassId = f.RoomClass.RoomClassId;
                vm.RoomClassPhoto1 = f.RoomClass.RoomClassPhoto1;
                vm.RoomClassDetail = f.RoomClass.RoomClassDetail;
                vm.WeekdayPrice = f.RoomClass.WeekdayPrice;
                vm.HolidayPrice = f.RoomClass.HolidayPrice;
                vm.AddPrice = f.RoomClass.AddPrice;
                vm.RoomClassName = f.RoomClass.RoomClassName;
                vm.RoomClassPeople = f.RoomClass.RoomClassPeople;
                vm.RoomClassSize = f.RoomClass.RoomClassSize;
                vmList.Add(vm);
            }

            if (freeRooms == null)
            {
                return View();
            }
            else
            {
                return View(vmList);
            }
        }

        [HttpPost]
        public IActionResult SearchRoom(string checkInDate, string checkOutDate)
        {

            DateTime checkIn = DateTime.ParseExact(checkInDate, "yyyy-MM-dd", CultureInfo.InvariantCulture);
            DateTime checkOut = DateTime.ParseExact(checkOutDate, "yyyy-MM-dd", CultureInfo.InvariantCulture);

            HttpContext.Session.SetString("CHECKINDATE", checkIn.ToString("yyyy-MM-dd"));
            HttpContext.Session.SetString("CHECKOUTDATE", checkOut.ToString("yyyy-MM-dd"));

            HotelOrderContext db = new HotelOrderContext();

            //查詢空閒房間方法
            //查詢指定時間區間內已被預訂的房間
            var reservedRooms = from od in db.OrderDetail
                                where !(od.CheckOutDate <= checkIn || od.CheckInDate >= checkOut)
                                select od.RoomId;
            var reservedRoomList = reservedRooms.ToList();

            //所有房間扣掉已預訂房間

            var freeRooms = db.Room.Include(r => r.RoomClass).AsEnumerable()
                .Where(r => !reservedRoomList.Contains(r.RoomId))
                .GroupBy(r => r.RoomClass.RoomClassName)
                .Select(g => new { g.Key, g }).ToList();

            List<CSearchRoomViewModel> vmList = new List<CSearchRoomViewModel>();

            foreach (var room in freeRooms)
            {
                var f = room.g.First();

                CSearchRoomViewModel vm = new CSearchRoomViewModel();
                vm.RoomClassId = f.RoomClassId;
                vm.RoomClassPhoto1 = f.RoomClass.RoomClassPhoto1;
                vm.RoomClassDetail = f.RoomClass.RoomClassDetail;
                vm.WeekdayPrice = f.RoomClass.WeekdayPrice;
                vm.HolidayPrice = f.RoomClass.HolidayPrice;
                vm.AddPrice = f.RoomClass.AddPrice;
                vm.RoomClassName = f.RoomClass.RoomClassName;
                vm.RoomClassPeople = f.RoomClass.RoomClassPeople;
                vm.RoomClassSize = f.RoomClass.RoomClassSize;
                vmList.Add(vm);
            }

            if (freeRooms != null)
            {
                return View();
            }
            else
            {
                return View(vmList);
            }
        }
        public IActionResult getActivitySession()
        {
            //讀取與設定入住日期
            string selectedActivityId = HttpContext.Session.GetString("SelectedActivityId");
            string selectedActivityTime = HttpContext.Session.GetString("ActivityTime");
            if (selectedActivityId != null && selectedActivityTime != null)
            {
                var jsonObject = new
                {
                    id = selectedActivityId,
                    time = selectedActivityTime
                };
                return Json(jsonObject);
            }
            return RedirectToAction("List");
        }

        //房間加入購物車
        public ActionResult AddShopCart(string RoomClassId)
        {
            HotelOrderContext db = new HotelOrderContext();
            var roomClass = db.RoomClass.Find(RoomClassId);

            string checkInDateStr = HttpContext.Session.GetString("CHECKINDATE");
            string checkOutDateStr = HttpContext.Session.GetString("CHECKOUTDATE");
                     
            if (roomClass != null)
            {
                DateTime checkInDate;
                DateTime checkOutDate;

                if (DateTime.TryParse(checkInDateStr, out checkInDate) && DateTime.TryParse(checkOutDateStr, out checkOutDate))
                {
                    string json = "";
                    List<CShopCartViewModel> cartList = null;
                    if (HttpContext.Session.Keys.Contains("CartData"))
                    { 
                        json = HttpContext.Session.GetString("CartData");
                        cartList = JsonSerializer.Deserialize<List<CShopCartViewModel>>(json);
                    }
                    else
                    {
                        cartList = new List<CShopCartViewModel>();
                    }

                    int fId = 0;

                    if (cartList.Count > 0)
                    {
                        fId = cartList.Max(item => item.FId) + 1;
                    }

                    CShopCartViewModel cartItem = new CShopCartViewModel();
                    {
                        cartItem.FId = fId;
                        cartItem.RoomClassName = roomClass.RoomClassName;
                        cartItem.RoomClassPhoto1 = roomClass.RoomClassPhoto1;
                        cartItem.RoomClassSize = roomClass.RoomClassSize;
                        cartItem.RoomClassId = roomClass.RoomClassId;
                        cartItem.RoomClassPeople = roomClass.RoomClassPeople;
                        cartItem.HolidayPrice = roomClass.HolidayPrice;
                        cartItem.WeekdayPrice = roomClass.WeekdayPrice;
                        cartItem.AddPrice = roomClass.AddPrice;
                        cartItem.CheckInDate = checkInDate;
                        cartItem.CheckOutDate = checkOutDate;
                    };
                    cartList.Add(cartItem);
                    json = JsonSerializer.Serialize(cartList);
                    HttpContext.Session.SetString("CartData", json);

                    return Json(json);
                }
            }
            return Json(null);
        }

        //網頁更新時顯示購物車session內容
        public ActionResult GetShopCartFromSession()
        {
            string json = HttpContext.Session.GetString("CartData");
            List<CShopCartViewModel> cartList = null;

            if (!string.IsNullOrEmpty(json))
            {
                cartList = JsonSerializer.Deserialize<List<CShopCartViewModel>>(json);
            }

            return Json(cartList);
        }

        //進入訂單明細顯示顯示購物車session內容
        public ActionResult GetShopCartOrderDetailSession()
        {
            string json = HttpContext.Session.GetString("CartData");
            List<CShopCartViewModel> cartList = null;

            if (!string.IsNullOrEmpty(json))
            {
                cartList = JsonSerializer.Deserialize<List<CShopCartViewModel>>(json);
            }

            return Json(cartList);
        }

        //刪除session
        [HttpPost]
        public void DeleteSessionData(int FId)
        {
            string json = HttpContext.Session.GetString("CartData");
            List<CShopCartViewModel> cartList = null;

            if (!string.IsNullOrEmpty(json))
            {
                cartList = JsonSerializer.Deserialize<List<CShopCartViewModel>>(json);

                // 查找并删除具有相同RoomClassName的项目
                cartList.RemoveAll(item => item.FId == FId);

                // 将更新后的数据重新存入会话
                string updatedJson = JsonSerializer.Serialize(cartList);
                HttpContext.Session.SetString("CartData", updatedJson);
            }
        }


        //訂房明細頁面
        public IActionResult Detail()
        {
            if (HttpContext.Session.GetString("UserID") == null)
            {
                return RedirectToAction("Login", "Member");
            }

            var userId = _contextAccessor.HttpContext.Session.GetString("UserID");
            HotelOrderContext db = new HotelOrderContext();
            IEnumerable<DiscountDetail> usesid = db.DiscountDetail.Where(x => x.MemberId == userId);

            var MemberDiscount = db.DiscountDetail.Include(x => x.Discount).Where(x => x.MemberId == userId).ToList();
            //var theater = movieContext.TSessions.Include(s => s.FTheater).FirstOrDefault(s => s.FSessionId == sessionID).FTheater;
            return View(MemberDiscount);
        }

        public IActionResult Create()
        {
            var userId = _contextAccessor.HttpContext.Session.GetString("UserID");
            HotelOrderContext db = new HotelOrderContext();
            IEnumerable<RoomMember> usersid = db.RoomMember.Where(x => x.MemberId == userId);

            return View(usersid);
        }

        //[HttpPost]
        //public IActionResult Create(Order p)
        //{
        //    string json;
        //    List<Order> detail = null;
        //    json = HttpContext.Session.GetString ( CDictionary.SK_PURCHASED_PRODUCTS_LIST );
        //    detail = JsonSerializer.Deserialize<List<Order>> ( json );
        //    return View ( );
        //}
        public IActionResult ShowOrder()
        {
            return View();
        }
    }
}
