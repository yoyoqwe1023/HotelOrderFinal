using HotelOrderFinal.Models;
using HotelOrderFinal.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata;
using System.Linq;

namespace HotelOrderFinal.Controllers
{
    public class CommentController : Controller
    {
        public IHttpContextAccessor _contextAccessor;
        public CommentController(IHttpContextAccessor contextAccessor)
        {
            this._contextAccessor = contextAccessor;
        }
        public IActionResult List(CKeywordViewModel vm)
        {
            HotelOrderContext db = new HotelOrderContext();
            IEnumerable<Comment> datas = null;
            if (string.IsNullOrEmpty(vm.txtKeyword))
                datas = from c in db.Comment
                        orderby c.CommentDate descending
                        select c;
            else
                datas = db.Comment
                    .Where(p => p.CommentDetail.Contains(vm.txtKeyword))
                    .OrderByDescending(p => p.CommentDate);
            return View(datas);
        }
        public IActionResult ListByMember(string? UserID)
        {
            var userId = _contextAccessor.HttpContext.Session.GetString("UserID");
            HotelOrderContext db = new HotelOrderContext();
            IEnumerable<Comment> member = db.Comment
                                          .Where(t => t.MemberId == userId)
                                          .OrderByDescending(p => p.CommentDate);
            if (member == null)
                return RedirectToAction("List");
            return View(member);
        }
        public IActionResult Create(string? UserID)
        {
            var userId = _contextAccessor.HttpContext.Session.GetString("UserID");
            ViewBag.UserID = userId;
            return View();
        }
        [HttpPost]
        public IActionResult Create(Comment p)
        {
            HotelOrderContext db = new HotelOrderContext();
            db.Comment.Add(p);
            db.SaveChanges();
            return RedirectToAction("ListByMember");
        }
        public IActionResult Edit(int? id)
        {
            HotelOrderContext db = new HotelOrderContext();
            Comment comment = db.Comment.FirstOrDefault(t => t.CommentId == id);
            if (comment == null)
                return RedirectToAction("ListByMember");
            return View(comment);
        }
        [HttpPost]
        public IActionResult Edit(Comment p)
        {
            HotelOrderContext db = new HotelOrderContext();
            Comment comment = db.Comment.FirstOrDefault(t => t.CommentId == p.CommentId);
            if (comment != null)
            {
                comment.CommentDetail = p.CommentDetail;
                db.SaveChanges();
            }
            return RedirectToAction("ListByMember");
        }

        public IActionResult Delete(int? id)
        {
            HotelOrderContext db = new HotelOrderContext();
            Comment comment = db.Comment.FirstOrDefault(t => t.CommentId == id);
            if (comment != null)
            {
                db.Comment.Remove(comment);
                db.SaveChanges();
            }
            return RedirectToAction("ListByMember");
        }
    }
}
