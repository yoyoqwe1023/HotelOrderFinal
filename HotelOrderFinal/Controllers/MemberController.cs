using HotelOrderFinal.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.Win32;
using System.Diagnostics.Metrics;


namespace HotelOrderFinal.Controllers
{
    public class MemberController : Controller
    {
        private HotelOrderContext db;
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
            //HttpContext.Session.SetString("LayoutMessage", "_LayoutMember");
            var member = db.RoomMember.Where(o => o.MemberPhone == model.MemberPhone && o.MemberPassword == model.MemberPassword).FirstOrDefault();
            if (member == null)
            {
                ViewBag.Message = "帳密錯誤，登入失敗";
                return View();
            }
            ViewBag.Message = model.MemberName + "，歡迎光臨";
            // 將使用者名字存入 ViewBag 或 ViewData 中
            ViewBag.UserName = model.MemberName;
            ViewData["UserID"] = member.MemberId;

            HttpContext.Session.SetString("UserName", member.MemberName);
            HttpContext.Session.SetString("UserID", member.MemberId);
            HttpContext.Session.SetString("LayoutMessage", "_LayoutMember");
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
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return View();
            }
        }

        // GET: MemberController/Edit/5
        public IActionResult Edit(string id)
        {
            if (HttpContext.Session.GetString("UserID") == null)
            {
                return RedirectToAction("Index", "Home");
            }
            id = HttpContext.Session.GetString("UserID");
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


        public IActionResult Logout()
        {
            // 登出
            
            HttpContext.Session.Clear();
            HttpContext.Session.SetString("LayoutMessage", "_Layout");

            // 重新導向至首頁
            return RedirectToAction("Index", "Home");
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
