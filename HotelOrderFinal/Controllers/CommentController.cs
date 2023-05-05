using HotelOrderFinal.Models;
using Microsoft.AspNetCore.Mvc;

namespace HotelOrderFinal.Controllers
{
    public class CommentController : Controller
    {
        public IActionResult List()
        {
            HotelOrderContext db = new HotelOrderContext();
            var datas = from c in db.Comments
                        select c;
            //var datas = db.Comments.Include("RoomMembers");
            return View(datas);
        }
    }
}
