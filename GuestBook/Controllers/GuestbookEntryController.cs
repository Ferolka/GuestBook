using GuestBook.Models;
using GuestBook.Repositories;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GuestBook.Controllers
{
    public class GuestbookEntryController:Controller
    {
        private readonly IGuestbookRepository _guestbookRepository;
        public GuestbookEntryController(IGuestbookRepository guestbookRepository)
        {
            _guestbookRepository = guestbookRepository;
        }
        public ActionResult Add(GuestbookEntry guestbookEntry)
        {
            var res = _guestbookRepository.AddGuestBook(guestbookEntry);
            //if (_guestbookRepository.AddGuestBook(guestbookEntry))
            //{
            return RedirectToAction("List");
            //return Json(new { res = "OK" });
            //}
            //else
            //{
            //    return Json(new { res = "FAIL", reason="DbError" });
            //}
            
        }
        public IActionResult List()
        {
            var res = _guestbookRepository.List();
            return View(res);
        }
    }
}
