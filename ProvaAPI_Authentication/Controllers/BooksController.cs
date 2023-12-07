using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProvaAPI_Authentication.Database;
using ProvaAPI_Authentication.DTO;
using ProvaAPI_Authentication.Entity;
using ProvaAPI_Authentication.Model;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ProvaAPI_Authentication.Controllers
{
    [Route("api/[controller]")]
    public class BooksController : Controller
    {
        private readonly MyDbContext _dbContext;

        public BooksController(MyDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpGet]
        [Route("{idBook}")]
        public IActionResult GetBookById(int idBook)
        {
            var book = _dbContext.Books
                .Include(b => b.User)
                .FirstOrDefault(b => b.IdBook == idBook); ;

            if (book == null)
                return NotFound("Libro non trovato");

            var b = MapBookEntityToBookModel(book);

            return Ok(b);
        }

        [HttpGet]
        public IActionResult GetAllBooks(string? title, string? author)
        {
            IQueryable<BookEntity> query = _dbContext.Books
                .Include(b => b.User);

            if (!string.IsNullOrEmpty(title))
            {
                query = query.Where(book => book.Title.Contains(title));
            }

            if (!string.IsNullOrEmpty(author))
            {
                query = query.Where(book => book.Author.Contains(author));
            }

            var books = query
                .Select(MapBookEntityToBookModel)
                .ToList();

            if (books.Count == 0)
            {
                return NotFound();
            }

            return Ok(books);
        }

        [HttpPost]
        public IActionResult AddBook([FromBody] BookDTO bookDTO)
        {
            var book = new BookEntity
            {
                Title = bookDTO.Title,
                Author = bookDTO.Author,
                PublicationDate = bookDTO.PublicationDate
            };

            _dbContext.Add(book);
            _dbContext.SaveChanges();

            return Ok(book);
        }

        [HttpPut]
        [Route("{idBook}")]
        public IActionResult UpdateUser([FromBody] BookDTO bookDTO, int idBook)
        {
            var existingBook = _dbContext.Books.FirstOrDefault(b => b.IdBook == idBook);

            if (existingBook != null)
            {
                existingBook.Title = bookDTO.Title;
                existingBook.Author = bookDTO.Author;
                existingBook.PublicationDate = bookDTO.PublicationDate;

                if(bookDTO.IdUser != 0)
                {
                    var existingUser = _dbContext.Users.FirstOrDefault(u => u.IdUser == bookDTO.IdUser);

                    if (existingUser == null)
                        return NotFound();

                    existingBook.IdUser = bookDTO.IdUser;
                }
                else
                {
                    existingBook.IdUser = null;
                }

                _dbContext.SaveChanges();

                return Ok(idBook);
            }

            return NotFound();
        }

        [HttpDelete]
        [Route("{idBook}")]
        public IActionResult DeleteUser(int idBook)
        {
            var book = _dbContext.Books.FirstOrDefault(b => b.IdBook == idBook);

            if (book != null)
            {
                _dbContext.Books.Remove(book);
                _dbContext.SaveChanges();

                return Ok(idBook);
            }

            return NotFound();

        }

        private BookModel MapBookEntityToBookModel(BookEntity book)
        {
            if(book.User != null)
            {
                return new BookModel
                {
                    IdBook = book.IdBook,
                    Title = book.Title,
                    Author = book.Author,
                    PublicationDate = book.PublicationDate,
                    User = new UserModel
                    {
                        IdUser = book.User.IdUser,
                        UserName = book.User?.UserName,
                        Password = book.User?.Password,
                        Name = book.User?.Name,
                        Surname = book.User?.Surname
                    }
                };
            }
            else
            {
                return new BookModel
                {
                    IdBook = book.IdBook,
                    Title = book.Title,
                    Author = book.Author,
                    PublicationDate = book.PublicationDate,
                    User = null
                };
            }
            
        }
    }
}

