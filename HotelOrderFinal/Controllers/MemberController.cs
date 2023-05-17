using HotelOrderFinal.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.Win32;
using System.Diagnostics.Metrics;
using System.Xml.Linq;
using System.Net.Mail;
using System.Web;
using HotelOrderFinal.ViewModels;
using System.Net.Mail;
using System.Net;

namespace HotelOrderFinal.Controllers
{
    public class MemberController : Controller
    {
        private HotelOrderContext db;
        private IWebHostEnvironment _enviro;
        // GET: MemberController
        public IActionResult Index(string id)
        {
            //驗證會員登入是否又錯誤，找不到該會員會回到首頁

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
                //ViewBag.Message = "帳密錯誤，登入失敗";
                TempData["ErrorMessage"] = "帳密錯誤，登入失敗";
                return RedirectToAction("Index", "Home");
            }
            ViewBag.Message = model.MemberName + "，歡迎光臨";
            TempData["SuccessMessage"] = model.MemberName + "，歡迎光臨";
            // 將使用者名字存入 ViewBag 或 ViewData 中
            ViewBag.UserName = model.MemberName;
            ViewData["UserID"] = member.MemberId;

            HttpContext.Session.SetString("UserName", member.MemberName);
            HttpContext.Session.SetString("UserID", member.MemberId);
            HttpContext.Session.SetString("UserPhone", member.MemberPhone);
            HttpContext.Session.SetString("LayoutMessage", "_LayoutMember");

            return RedirectToAction("Index", "Home");
        }

        public IActionResult ForgotPassword()
        {
            // 處理密碼重製
            return View();
        }

        [HttpPost]
        public IActionResult ResetPassword(string resetEmail)
        {
            // 处理密码重置的逻辑
            // 发送重置密码的电子邮件或短信等
            // 进行密码重置操作

            ViewBag.ResetMessage = "密码重置邮件已发送，请查看您的注册邮箱并按照提示重置密码。";
            return View("ForgotPassword");
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
            return PartialView();
        }

        // POST: MemberController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(RoomMember model)
        {
            if (!ModelState.IsValid)
            {
                return PartialView(model);
            }
            //if (!ModelState.IsValid)
            //{
            //    return Json(new { success = false, message = "驗證失敗" });
            //}
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
                //return RedirectToAction("Index", "Home");
                //TempData["SuccessMessage"] = "註冊成功，請重新登入，謝謝！";
                //return RedirectToAction("Index", "Home");
                //return RedirectToAction("Index", "Home", new { message = "註冊成功，請重新登入，謝謝 !" });
                return RedirectToAction("Index", "Home", new { message = HttpUtility.UrlEncode("註冊成功，請重新登入，謝謝 !") });


                //return Json(new { success = true, message = "註冊成功" });
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return PartialView();
                //return Json(new
                //{
                //    success = false,
                //    message = ex.Message
                //});
            }
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

                //if (ModelState.IsValid)
                //{

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

                //}

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



        //【修改密碼】==========================================================================================
        public IActionResult EditPassword(int id)
        {
            return View();
        }



        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult EditPassword(ChangePasswordViewModel model)
        {
            if (ModelState.IsValid)
            {
                // 在這裡進行舊密碼的驗證
                // 比對舊密碼是否與資料庫中的密碼匹配
                //bool isOldPasswordCorrect = ValidateOldPassword(model.OldPassword, model.UserId);
                bool isOldPasswordCorrect = true;

                if (isOldPasswordCorrect)
                {
                    if (model.NewPassword == model.ReNewPassword)
                    {
                        // 在這裡更新資料庫中的密碼
                        // 根據 model.UserId 找到對應的用戶，並將密碼更新為 model.NewPassword
                        //UpdatePasswordInDatabase(model.UserId, model.NewPassword);

                        return RedirectToAction("Success");
                    }
                    else
                    {
                        ModelState.AddModelError("ReNewPassword", "新密碼與確認密碼不一致");
                    }
                }
                else
                {
                    ModelState.AddModelError("OldPassword", "舊密碼不正確");
                }
            }

            return RedirectToAction("Index", "Home");
        }

        public IActionResult ShoppingCar(int id)
        {
            //沒有資料庫的時候測試用
            //List<Order> orders = new List<Order>();
            //for (var i = 1; i <= 10; i++)
            //{
            //    Order order = new Order();
            //    order.OrderId = i.ToString();
            //    order.MemberId = "Member_" + i.ToString();
            //    order.OrderDate = DateTime.Now;
            //    order.OrderTotalPrice = 100 + i;
            //    order.CheckInPeople = i + 6;
            //    order.ActivityId = i+10;
            //    order.OrderRemark = string.Empty;
            //    orders.Add(order);
            //}
            db = new HotelOrderContext();
            List<Order> orders = db.Order.Where(t => t.MemberId == HttpContext.Session.GetString("UserID")).ToList();
            return View(orders);
        }


        public IActionResult MemberTerms(int id)
        {
            return View();
        }

        public IActionResult MemberCard(int id)
        {
            return View();
        }
    }
}
