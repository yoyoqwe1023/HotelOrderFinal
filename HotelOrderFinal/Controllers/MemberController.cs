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
using Microsoft.EntityFrameworkCore;
using Microsoft.Data.SqlClient;
using System.Data;
using System.Security.Cryptography;
using System.Text;


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
            HttpContext.Session.SetString("UserPassword", member.MemberPassword);
            HttpContext.Session.SetString("LayoutMessage", "_LayoutMember");

            return RedirectToAction("Index", "Home");
        }
        //【忘記密碼】==========================================================================================

        public IActionResult ForgotPassword()
        {
            // 處理密碼重製
            return View();
        }

        [HttpPost]
        //public IActionResult ResetPassword(string resetEmail)
        //{
        //    // 处理密码重置的逻辑
        //    // 发送重置密码的电子邮件或短信等
        //    // 进行密码重置操作

        //    ViewBag.ResetMessage = "密码重置邮件已发送，请查看您的注册邮箱并按照提示重置密码。";
        //    return View("ForgotPassword");
        //}
        /// <summary>
        /// 寄送驗證碼
        /// </summary>
        /// <returns></returns>

        [ValidateAntiForgeryToken]
        public IActionResult ResetPassword(SendMailTokenIn inModel,string id)
        {
            SendMailTokenOut outModel = new SendMailTokenOut();

            // 檢查輸入來源
            if (string.IsNullOrEmpty(inModel.UserID))
            {
                outModel.ErrMsg = "請輸入帳號";
                return Json(outModel);
            }

            // 檢查資料庫是否有這個帳號

            // 取得資料庫連線字串
            db = new HotelOrderContext();

            id = HttpContext.Session.GetString("UserID");

            db = new HotelOrderContext();

            // 取得會員資料
            RoomMember cust = db.RoomMember.FirstOrDefault(t => t.MemberId == id);
            // 當程式碼離開 using 區塊時，會自動關閉連接
            //using (SqlConnection conn = new SqlConnection(connStr))
            //{
                //// 資料庫連線
                //conn.Open();

                //// 取得會員資料
                //string sql = "select * from Member where UserID = @UserID";
                //SqlCommand cmd = new SqlCommand();
                //cmd.CommandText = sql;
                //cmd.Connection = conn;

                // 使用參數化填值
                //cmd.Parameters.AddWithValue("@UserID", inModel.UserID);

                // 執行資料庫查詢動作
                //SqlDataAdapter adpt = new SqlDataAdapter();
                //adpt.SelectCommand = cmd;
                DataSet ds = new DataSet();
                //adpt.Fill(ds);
                DataTable dt = ds.Tables[0];

                if (dt.Rows.Count > 0)
                {
                    // 取出會員信箱
                    string UserEmail = dt.Rows[0]["UserEmail"].ToString();

                    // 取得系統自定密鑰，在 Web.config 設定
                    //string SecretKey = ConfigurationManager.AppSettings["SecretKey"];

                    // 產生帳號+時間驗證碼
                    string sVerify = inModel.UserID + "|" + DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss");

                //    // 將驗證碼使用 3DES 加密
                //    TripleDESCryptoServiceProvider DES = new TripleDESCryptoServiceProvider();
                //    MD5 md5 = new MD5CryptoServiceProvider();
                //byte[] buf = Encoding.UTF8.GetBytes(SecretKey);
                //byte[] result = md5.ComputeHash(buf);
                //string md5Key = BitConverter.ToString(result).Replace("-", "").ToLower().Substring(0, 24);
                //DES.Key = UTF8Encoding.UTF8.GetBytes(md5Key);
                //    DES.Mode = CipherMode.ECB;
                //    ICryptoTransform DESEncrypt = DES.CreateEncryptor();
                //    byte[] Buffer = UTF8Encoding.UTF8.GetBytes(sVerify);
                //    sVerify = Convert.ToBase64String(DESEncrypt.TransformFinalBlock(Buffer, 0, Buffer.Length)); // 3DES 加密後驗證碼

                    // 將加密後密碼使用網址編碼處理
                    sVerify = HttpUtility.UrlEncode(sVerify);

                    // 網站網址
                    //string webPath = Request.Url.Scheme + "://" + Request.Url.Authority + Url.Content("~/");
                    string webPath = $"{Request.Scheme}://{Request.Host}{Request.PathBase}";


                    // 從信件連結回到重設密碼頁面
                    string receivePage = "Member/ResetPwd";

                    // 信件內容範本
                    string mailContent = "請點擊以下連結，返回網站重新設定密碼，逾期 30 分鐘後，此連結將會失效。<br><br>";
                    mailContent = mailContent + "<a href='" + webPath + receivePage + "?verify=" + sVerify + "'  target='_blank'>點此連結</a>";

                    // 信件主題
                    string mailSubject = "重設密碼申請信";

                    // Google 發信帳號密碼
                    string GoogleMailUserID = "imapple1991@gmail.com"; //Google 發信帳號
                    string GoogleMailUserPwd = "notffmckaayqzchi"; //應用程式密碼

                    // 使用 Google Mail Server 發信
                    string SmtpServer = "smtp.gmail.com";
                    int SmtpPort = 587;
                    MailMessage mms = new MailMessage();
                    mms.From = new MailAddress(GoogleMailUserID);
                    mms.Subject = mailSubject;
                    mms.Body = mailContent;
                    mms.IsBodyHtml = true;
                    mms.SubjectEncoding = Encoding.UTF8;
                    mms.To.Add(new MailAddress(UserEmail));
                    using (SmtpClient client = new SmtpClient(SmtpServer, SmtpPort))
                    {
                        client.EnableSsl = true;
                        client.Credentials = new NetworkCredential(GoogleMailUserID, GoogleMailUserPwd);//寄信帳密 
                        client.Send(mms); //寄出信件
                    }
                    outModel.ResultMsg = "請於 30 分鐘內至你的信箱點擊連結重新設定密碼，逾期將無效";
                }
                else
                {
                    outModel.ErrMsg = "查無此帳號";
                }
            

            // 回傳 Json 給前端
            return Json(outModel);
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
                  
            string memberID_ = string.Empty;
            int maxMemberID = 0;
            model.AdminId = "AD00010";
            //性別處理
            string gender = model.MemberGender;
            string genderText = (gender == "male") ? "男" : "女";
            //生日處理
            DateTime birthday = model.MemberBirthday.Value;

            try
            {
                db = new HotelOrderContext();

                //自動產生MemberID
                var q = db.RoomMember.Select(x => x.MemberId).ToList();
                int count = q.Count();
                maxMemberID = (count > 0) ? Convert.ToInt32(q.Max().Substring(2)) : 0;
                memberID_ = (count == 0) ? "MB00000" : "MB" + (maxMemberID + 1).ToString().PadLeft(5, '0');
                model.MemberId = memberID_;
                // 設置性別屬性
                model.MemberGender = genderText;
                model.MemberBirthday = birthday;

                db.Add(model);
                db.SaveChanges();
                return RedirectToAction("Index", "Home", new { message = HttpUtility.UrlEncode("註冊成功，請重新登入，謝謝 !") });

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

        //【修改密碼】==========================================================================================


        public IActionResult EditPassword(string UserID)
        {
 
            if (HttpContext.Session.GetString("UserID") == null)
            {
                return RedirectToAction("Index", "Home");
            }

            UserID = HttpContext.Session.GetString("UserID");

            db = new HotelOrderContext();
            RoomMember cust = db.RoomMember.Where(t => t.MemberId == UserID).FirstOrDefault();
   
            if (cust == null)
                return RedirectToAction("Index", "Home");
            return View(cust);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult EditPassword(ChangePasswordViewModel change)
        {
            db = new HotelOrderContext();
               RoomMember cust = db.RoomMember.Where(o => o.MemberId == change.UserId).FirstOrDefault();
            if (cust == null )
            {                
                ModelState.AddModelError("OldPassword", "舊密碼不正確");
                TempData["ErrorMessage"] = "舊密碼不正確";
                return View("EditPassword", "model");
            }
            if (change.NewPassword != change.ReNewPassword)
            {
                ModelState.AddModelError("ReNewPassword", "新密碼兩次輸入不一致");
                TempData["ErrorMessage"] = "新密碼兩次輸入不一致";
                return View("EditPassword", "model");
            }
            if (cust.MemberPassword != change.OldPassword)
            {
                ModelState.AddModelError("OldPassword", "舊密碼不正確");
                TempData["ErrorMessage"] = "舊密碼不正確";
                return View("EditPassword", "model");
            }
            cust.MemberPassword = change.NewPassword;             
                db.SaveChanges();
                // 在ViewBag中设置密码重置成功的消息
                ViewBag.ResetSuccessMessage = "您的密碼已重設。";
                // 重定向到显示成功消息的页面
                return RedirectToAction("Index", "Member");

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


//【歷史訂單】==========================================================================================

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
