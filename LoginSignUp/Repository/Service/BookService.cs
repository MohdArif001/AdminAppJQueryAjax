using LoginSignUp.Models;
using LoginSignUp.Repository.Interface;
using LoginSignUp.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LoginSignUp.Repository.Service
{
    public class BookService: GenericInterface<BookWithAuthorViewModel>,IBook
    {
        private ApplicationDbContext _dbContext;
        public BookService()
        {
            _dbContext = new ApplicationDbContext();
        }
        public List<BookWithAuthorViewModel> GetData()
        {
            var books = (from book in _dbContext.Books
                         join
                         author in _dbContext.Authors
                         on book.Author_Id equals author.Id
                         select new BookWithAuthorViewModel()
                         {
                             Id = book.Id,
                             Title = book.Title,
                             Price = book.Price,
                             Quantity = book.Quantity,
                             Published_On = book.Published_On,
                             AuthorName = author.Name,
                             AuthorEMail = author.Email,
                             AuthorMobile = author.Mobile

                         }).ToList();

            return books;
        }

        public bool DeleteBook(int id)
        {
            var book = _dbContext.Books.SingleOrDefault(e => e.Id == id);
            if (book != null)
            {
                _dbContext.Books.Remove(book);
                _dbContext.SaveChanges();
                return true;
            }
            else
            {
                return false;
            }

        }


    }
}
