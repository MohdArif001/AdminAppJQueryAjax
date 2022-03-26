using LoginSignUp.Repository.Interface;
using LoginSignUp.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LoginSignUp.Controllers
{
    public class BookController : Controller
    {
        public IBook Book { get; }
        public GenericInterface<BookWithAuthorViewModel> BookServices { get; }
        public BookController(GenericInterface<BookWithAuthorViewModel> _book, IBook book)
        {
            BookServices = _book;
            Book = book;
        }
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public JsonResult DeleteBook(int id)
        {
            var result = Book.DeleteBook(id);
            if (result)
            {
                return Json(new { mesage = "Book Deleted Successfully", ok = true });
            }
            else
            {
                return Json(new { mesage = "Book Not Deleted Successfully ", ok = false });
            }
        }

        public JsonResult GetBooks()
        {
            var books = BookServices.GetData();
            return Json(books);
        }
     
    }
}
