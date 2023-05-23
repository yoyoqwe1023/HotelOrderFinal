using HotelOrderFinal.Models;
using Microsoft.AspNetCore.Mvc;


namespace HotelOrderFinal.Controllers
{
    public class MemberMangeController : Controller
    {
        public IActionResult List()
        {
            //ViewBag.SelectedItemContent = "會員管理的內容";
            HotelOrderContext db = new HotelOrderContext();
            var datas = from c in db.RoomMember
                        select c;
            return View(datas);
        }

        //public IActionResult List(int pageNumber = 1, int pageSize = 10)
        //{
        //    ViewBag.SelectedItemContent = "會員管理的內容";
        //    HotelOrderContext db = new HotelOrderContext();

        //    var totalItems = db.RoomMember.Count();
        //    var totalPages = (int)Math.Ceiling((double)totalItems / pageSize);

        //    var pagedData = db.RoomMember
        //        .Skip((pageNumber - 1) * pageSize)
        //        .Take(pageSize)
        //        .ToList();

        //    ViewBag.TotalPages = totalPages;
        //    ViewBag.CurrentPage = pageNumber;

        //    return View(pagedData);
        //}
        public IActionResult Edit(string? id)
        {
            HotelOrderContext db = new HotelOrderContext();
            RoomMember member = db.RoomMember.FirstOrDefault(t => t.MemberId == id);
            if (member == null)
                return RedirectToAction("List");
            return View(member);
        }
        [HttpPost]
        public IActionResult Edit(RoomMember p)
        {
            HotelOrderContext db = new HotelOrderContext();
            RoomMember member = db.RoomMember.FirstOrDefault(t => t.MemberId == p.MemberId);
            if (member != null)
            {
                member.MemberName = p.MemberName;
                member.MemberBirthday = p.MemberBirthday;
                member.MemberGender = p.MemberGender;
               member.MemberPhone = p.MemberPhone;

                db.SaveChanges();
            }
            return RedirectToAction("Index");
        }
        public IActionResult Delete(string? id)
        {
            HotelOrderContext db = new HotelOrderContext();
            RoomMember member = db.RoomMember.FirstOrDefault(a => a.MemberId == id);
            if (member != null)
            {
                db.RoomMember.Remove(member);
                db.SaveChanges();
            }
            return RedirectToAction("List");
        }

    }   
}
