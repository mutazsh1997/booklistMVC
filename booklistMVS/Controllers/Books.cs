using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Threading.Tasks;
using booklistMVS.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace booklistMVS.Controllers
{
    public class Books : Controller
    {
        private readonly DataContenxt _db;
        [BindProperty]
        public Book book { get; set; }

        public Books(DataContenxt db)
        {
            _db = db;
        }

        public IActionResult Index()
        {
            return View();
        } 
        public IActionResult Upsert(Guid? id)
        {
            book = new Book();
            if(id == null)
            {
                return View(book);
            }else
            {
                book = _db.books.FirstOrDefault(bID => bID.Id == id);
                if(book == null)
                {
                    return NotFound();
                }
            }
            return View(book);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
         public IActionResult Upsert()
        {

            if (ModelState.IsValid)
            {
                if(book.Id.ToString() == "00000000-0000-0000-0000-000000000000")
                {
                    _db.books.Add(book);
                }else
                {
                    _db.books.Update(book);
                }
                _db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(book);
        }

        #region API Calls
        [HttpGet]
        public async Task<IActionResult> getAll()
        {
            return Json(new { data = await _db.books.ToListAsync() });
        }

        [HttpDelete]
        public async Task<IActionResult> delete(Guid id)
        {
            var bookfromDB = await _db.books.FirstOrDefaultAsync(b => b.Id == id);
            if (bookfromDB == null)
            {
                return Json(new { success = false, message = "Error while deleteing" });
            }
            _db.books.Remove(bookfromDB);
            await _db.SaveChangesAsync();
            return Json(new { success = true, message = "remove book successfuly" });
        }
        #endregion
    }
}
