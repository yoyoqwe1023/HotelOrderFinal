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
            HotelOrderContext db = new HotelOrderContext();
            db.Discount.Add(p);
            db.SaveChanges();
            return RedirectToAction("List");
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
