using HotelOrderFinal.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Win32;

namespace HotelOrderFinal.Controllers
{
    public class MemberController : Controller
    {
        private  HotelOrderContext db;
        private IWebHostEnvironment _enviro;
        // GET: MemberController
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(RoomMember model)
        {
            db = new HotelOrderContext();

            var member = db.RoomMember.Where(o => o.MemberPhone == model.MemberPhone && o.MemberPassword == model.MemberPassword).FirstOrDefault();
            if (member == null)
            {
                ViewBag.Message = "帳密錯誤，登入失敗";
                return View();
            }

            ViewBag.Message = model.MemberName + "，歡迎光臨";

            HttpContext.Session.SetString("UserName", model.MemberName);
            HttpContext.Session.SetString("UserID", member.MemberId);

            return RedirectToAction("Index", "Home");
         }
        // GET: MemberController/Details/5
        public IActionResult Details(int id)
        {
            return View();
        }

        // GET: MemberController/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: MemberController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(RoomMember model)
        {
            string memberID_ = string.Empty;
            int maxMemberID = 0;
            model.AdminId = "AD00010";
            try
            {
                db = new HotelOrderContext();

                //自動產生MemberID
                var q = db.RoomMember.Select(x => x.MemberId).ToList();
                int count = q.Count();
                maxMemberID = (count > 0) ? Convert.ToInt32(q.Max().Substring(2)) : 0;
                memberID_ = (count == 0) ? "MB00000" : "MB" + (maxMemberID + 1).ToString().PadLeft(5, '0');
                model.MemberId = memberID_;

                db.Add(model);
                db.SaveChanges();
                return RedirectToAction("Index", "Home");
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return View();
            }
        }

        // GET: MemberController/Edit/5
        public IActionResult Edit(string id = "MB00009")
        {
            db = new HotelOrderContext();
            RoomMember cust = db.RoomMember.FirstOrDefault(t => t.MemberId == id);
            if (cust == null)
                return RedirectToAction("Index", "Home");
            return View(cust);
        }

        // POST: MemberController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(RoomMember model)
        {
            try
            {
                db = new HotelOrderContext();
                RoomMember cust = db.RoomMember.FirstOrDefault(t => t.MemberId == model.MemberId);
                if (cust != null)
                {
                    cust.MemberName = model.MemberName;
                    cust.MemberPhone = model.MemberPhone;
                    cust.MemberEmail = model.MemberEmail;                    
                    cust.MemberPassword = model.MemberPassword;
                    db.SaveChanges();
                }
                return RedirectToAction("Index", "Home");
            }
            catch
            {
                return View();
            }
        }

        // GET: MemberController/Delete/5
        public IActionResult Delete(int id)
        {
            return View();
        }

        // POST: MemberController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
