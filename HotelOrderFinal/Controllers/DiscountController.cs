using HotelOrderFinal.Models;
using HotelOrderFinal.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace HotelOrderFinal.Controllers
{
    public class DiscountController : Controller
    {
        public IHttpContextAccessor _contextAccessor;

        public DiscountController(IHttpContextAccessor contextAccessor)
        {
            this._contextAccessor = contextAccessor;
        }

        public IActionResult List(CKeywordViewModel vm)
        {
            HotelOrderContext db = new HotelOrderContext();
            IEnumerable<Discount> datas = null;
            if (string.IsNullOrEmpty(vm.txtKeyword))
                datas = from c in db.Discount
                        select c;
            else
                return RedirectToAction("Index", "Home");
            return View(datas);
        }

        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(Discount p)
        {
            try
            {
                HotelOrderContext db = new HotelOrderContext();

                // 取得當時的會員數量
                int memberCount = db.RoomMember.Count();
                // 取得優惠開始時間
                var discountStart = db.Discount.Where(x => x.DiscountName == "新會員優惠").Select(x => x.DiscountStart).FirstOrDefault();

                // 判斷是否有符合條件的會員，若無則直接回傳
                if (memberCount == 0 || discountStart > DateTime.Now)
                {
                    return RedirectToAction("List");
                }

                // 取得符合條件的會員，並加入優惠資料
                //var members = db.RoomMember.ToList();
                //foreach (var member in members)
                //{
                //    if (member.CreateTime <= discountStart)
                //    {
                //        MemberDiscount memberDiscount = new MemberDiscount()
                //        {
                //            MemberId = member.MemberId,
                //            DiscountId = p.DiscountId
                //        };
                //        db.MemberDiscount.Add(memberDiscount);
                //    }
                //}

                db.Discount.Add(p);
                db.SaveChanges();
                return RedirectToAction("List");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return View();
            }
        }
    
        public IActionResult Delete(int? id)
        {
            HotelOrderContext db = new HotelOrderContext();
            Discount cust = db.Discount.FirstOrDefault(t => t.DiscountId == id);
            if (cust != null)
            {
                db.Discount.Remove(cust);
                db.SaveChanges();
            }
            return RedirectToAction("List");
        }
        public IActionResult Edit(int? id)
        {
            HotelOrderContext db = new HotelOrderContext();
            Discount cust = db.Discount.FirstOrDefault(t => t.DiscountId == id);
            if (cust == null)
                return RedirectToAction("List");
            return View(cust);
        }
        [HttpPost]
        public IActionResult Edit(Discount p)
        {
            HotelOrderContext db = new HotelOrderContext();
            Discount cust = db.Discount.FirstOrDefault(t => t.DiscountId == p.DiscountId);
            if (cust != null)
            {
                cust.DiscountName = p.DiscountName;
                cust.DiscountImage = p.DiscountImage;
                cust.DiscountDirections = p.DiscountDirections;
                cust.DiscountStart = p.DiscountStart;
                cust.DiscountEnd = p.DiscountEnd;
                cust.DiscountUse = p.DiscountUse;
                db.SaveChanges();
            }
            return RedirectToAction("List");
        }

        public IActionResult DiscountByMember()
        {
            var userId = _contextAccessor.HttpContext.Session.GetString("UserID");
            HotelOrderContext db = new HotelOrderContext();
            DateTime now = DateTime.Now;
            IEnumerable<Discount> usesid = db.Discount.Where(x => x.MemberId == userId && x.DiscountStart <= now && x.DiscountEnd >= now);
            //IEnumerable<Discount> usesid = db.Discount.Where(x => x.MemberId == userId);
            return View(usesid);            
        }
    }
}
