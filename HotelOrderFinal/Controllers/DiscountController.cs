using HotelOrderFinal.Models;
using HotelOrderFinal.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace HotelOrderFinal.Controllers
{
    public class DiscountController : Controller
    {
        private HotelOrderContext db = new HotelOrderContext();
        public IHttpContextAccessor _contextAccessor;

        public DiscountController(IHttpContextAccessor contextAccessor)
        {
            this._contextAccessor = contextAccessor;
        }

        public IActionResult List(CKeywordViewModel vm)
        {

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

                // 取得優惠是否存在
                p.DiscountExist = true;

                db.Discount.Add(p);
                db.SaveChanges();


                // 取得當時的會員數量
                int memberCount = db.RoomMember.Count();

                if (p.DiscountExist == true && memberCount > 0)
                {
                    //所有的會員ID
                    var members = db.RoomMember.Select(x => x.MemberId);
                    int lastID = db.Discount.OrderByDescending(x => x.DiscountId).FirstOrDefault().DiscountId;
                    foreach (var itme in members)
                    {

                        DiscountDetail detail = new DiscountDetail();
                        detail.DiscountId = lastID;
                        detail.DiscountStart = DateTime.Now;
                        detail.DiscountEnd = DateTime.Now.AddMonths(3);
                        detail.DiscountUse = 0;
                        detail.MemberId = itme;
                        db.DiscountDetail.Add(detail);

                    }
                    db.SaveChanges();
                    return RedirectToAction("List");
                }

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
            Discount cust = db.Discount.FirstOrDefault(t => t.DiscountId == id);
            if (cust == null)
                return RedirectToAction("List");
            return View(cust);
        }
        [HttpPost]
        public IActionResult Edit(Discount p)
        {           
            Discount cust = db.Discount.FirstOrDefault(t => t.DiscountId == p.DiscountId);
            if (cust != null)
            {
                cust.DiscountName = p.DiscountName;
                cust.DiscountImage = p.DiscountImage;
                cust.DiscountDirections = p.DiscountDirections;
                cust.DiscountDiscount = p.DiscountDiscount;
                db.SaveChanges();
            }
            return RedirectToAction("List");
        }

        public IActionResult DiscountByMember()
        {
            var userId = _contextAccessor.HttpContext.Session.GetString("UserID");
            
            IEnumerable<DiscountDetail> usesid = db.DiscountDetail.Where(x => x.MemberId == userId);

            //IEnumerable<Discount> usesid = db.Discount.Where(x => x.MemberId == userId);
            return View(usesid);
        }
    }
}
