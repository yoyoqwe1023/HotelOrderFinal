using HotelOrderFinal.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.Win32;
using System.Diagnostics.Metrics;
using System.Xml.Linq;


namespace HotelOrderFinal.Controllers
{
    public class MemberController : Controller
    {
        private HotelOrderContext db;
        private IWebHostEnvironment _enviro;
        // GET: MemberController
        public IActionResult Index()
        {
            return PartialView("_MyAccount");
        }


        public IActionResult MyAccount()
        {
            return PartialView("_MyAccount");
        }



        //【登入】==========================================================================================
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


        //【　　】==========================================================================================
        // GET: MemberController/Details/5
//        public IActionResult Details(int id)
//        {
//            if (Request.IsAjaxRequest())
//            {
//                this.GetClientes();
//                var cliente = Clientes.FirstOrDefault(x => x.Id == id) ?? new Cliente();
//                if (cliente != null)
//                    return PartialView(cliente);
//                else
//                {
//                    Response.StatusCode - 403;
//                    return PartialView("Error");
//                }
//            }

//            Response.StatusCode = 500;
//            return PartialView("Error");
//            //return View();
//        }

//        private void GetClientes()
//        {
//            if (Session["clientes"] == null)
//            {
//                Clientes = new List<Cliente>()
//{
//new Cliente() ( Id = 1, Name = "Paco", Number = "111" ),
//new Cliente() ( Id = 2, Name = "Lucia", Number = "2222" )
//};
//                Session["clientes"] = Clientes;
//            }
//            Clientes = Session["clientes"] as List<Cliente>;
//        }





        //【新增】==========================================================================================
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
            //try
            //{
                db = new HotelOrderContext();

                //自動產生MemberID
                var q = db.RoomMember.Select(x => x.MemberId).ToList();
                int count = q.Count();
                maxMemberID = (count > 0) ? Convert.ToInt32(q.Max().Substring(2)) : 0;
                memberID_ = (count == 0) ? "MB00000" : "MB" + (maxMemberID + 1).ToString().PadLeft(5, '0');
                model.MemberId = memberID_;

                db.Add(model);
                //-fufu
                //db.SaveChanges();
                //return RedirectToAction("Index", "Home");
                //-fufu
                db.SaveChanges();

                //新增會員折扣
                RoomMember newmember = db.RoomMember.FirstOrDefault(x => x.MemberId == model.MemberId);
                Discount discount = new Discount();
                discount.MemberId = newmember.MemberId;
                discount.DiscountName = "新會員優惠";
                discount.DiscountStart = DateTime.Now;
                discount.DiscountEnd = DateTime.Now.AddYears(1); // 一年後到期
                discount.DiscountDiscount = (decimal)0.7; // 七折優惠
                db.Discount.Add(discount);

                db.SaveChanges();
                return RedirectToAction("Index", "Home");
            //}
            //catch (Exception ex)
            //{
            //    Console.WriteLine(ex.ToString());
            //    return View();
                
            //}

            
        }

        //【修改】==========================================================================================
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

        //【登出】==========================================================================================
        public IActionResult Logout()
        {
            // 登出
            
            HttpContext.Session.Clear();
            HttpContext.Session.SetString("LayoutMessage", "_Layout");

            // 重新導向至首頁
            return RedirectToAction("Index", "Home");
        }

        //【刪除】==========================================================================================
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
        //public IActionResult MyAction()
        //{
        //    // 這裡返回一個包含數據的Partial View
        //    return PartialView("_MyPartialView", myData);
        //}




    }
}
