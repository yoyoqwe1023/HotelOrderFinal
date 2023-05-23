using HotelOrderFinal.Models;
using HotelOrderFinal.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Net;

namespace HotelOrderFinal.Controllers
{
    public class ActivityController : Controller
    {
        private HotelOrderContext db = new HotelOrderContext();
        private IWebHostEnvironment _enviro;
        
        
        public ActivityController(IWebHostEnvironment p)
        {
            _enviro = p;
        }
        public IActionResult Delete(int? id)
        {

            Activity cust = db.Activity.FirstOrDefault(t => t.ActivityId == id);
            if (cust != null)
            {
                db.Activity.Remove(cust);
                db.SaveChanges();
            }
            return RedirectToAction("ActivityByCreate");
        }
        public IActionResult Create()
        {            
            return View();
        }
        [HttpPost]
        public IActionResult Create(Activity p)
        {
            db.Activity.Add(p);
            db.SaveChanges();
            return RedirectToAction("ActivityByCreate");
        }
            public IActionResult ActivityByCreate(CKeywordViewModel vm)
        {
            HotelOrderContext db = new HotelOrderContext();
            IEnumerable<Activity> datas = null;
            if (string.IsNullOrEmpty(vm.txtKeyword))
                datas = from c in db.Activity                        
                        select c;
            else
                datas = db.Activity
                    .Where(p => p.ActivityDirections.Contains(vm.txtKeyword)); 
            if(datas == null)
            {
                return RedirectToAction("ActivityByCreate");
            }
            return View(datas);
        }
        public ActionResult ActivityByAcceding(int? id)
        {
            return View();
        }
        public ActionResult ActivityByDetails(int? id)
        {
            var cust = db.Activity.FirstOrDefault(t => t.ActivityId == id);
            //Activity cust = db.Activity.FirstOrDefault(t => t.ActivityId == id);
            if (cust == null)
                return RedirectToAction("List");
            return View(cust);
        }
        [HttpPost]
        public ActionResult setSessionByActivity(int id)
        {          
            var a = db.Activity.FirstOrDefault(t => t.ActivityId == id);
            if (a== null)
            {
                return RedirectToAction("ActivityByDetails");
            }
            if (HttpContext.Session.GetString("SelectedActivityId") == null)
            {
                HttpContext.Session.SetString("SelectedActivityId", a.ActivityId.ToString());
            }

            if (HttpContext.Session.GetString("ActivityTime") == null)
            {
                HttpContext.Session.SetString("ActivityTime", a.ActivityTime.ToString());
            }
            return RedirectToAction("List", "Order");
        }

        public IActionResult List(CKeywordViewModel vm)
        {
            HotelOrderContext db = new HotelOrderContext();
            IEnumerable<Activity> datas = null;
            if (string.IsNullOrEmpty(vm.txtKeyword))
                datas = from c in db.Activity                        
                        select c;
            return View(datas);
        }
        //public IActionResult List()
        //{    

        //    var datas = from c in db.Activity
        //                select c;
        //    return View(datas);
        //}
        public IActionResult Edit(int? id)
        {
            HotelOrderContext db = new HotelOrderContext();
            Activity cust = db.Activity.FirstOrDefault(t => t.ActivityId == id);
            if (cust == null)
                return RedirectToAction("List");
            return View(cust);
        }
        [HttpPost]
        public IActionResult Edit(CActivityWrap p)
        {
            
            Activity cust = db.Activity.FirstOrDefault(t => t.ActivityId == p.ActivityId);
            if (cust != null)
            {
                if (p.photo != null)
                {
                    string photoName = Guid.NewGuid().ToString() + ".jpg";
                    string path = _enviro.WebRootPath + "/image/" + photoName;
                    p.photo.CopyTo(new FileStream(path, FileMode.Create));
                    cust.ActivityImage = photoName;
                }

                cust.ActivityName = p.ActivityName;            
                cust.ActivityDirections = p.ActivityDirections;
                cust.ActivityTime = p.ActivityTime;
                cust.ActivityPlace = p.ActivityPlace;
                cust.ActivityPeople = p.ActivityPeople;
                cust.ActivityCost = p.ActivityCost;
                db.SaveChanges();
            }
            return RedirectToAction("List");
        }

        public IActionResult CheckLoginStatus()
        {
            if (HttpContext.Session.GetString("UserID") != null)
            {
                // 使用者已登入
                return Json(new { loggedIn = true });
            }
            else
            {
                // 使用者未登入
                return Json(new { loggedIn = false });
            }
        }

    }
}
