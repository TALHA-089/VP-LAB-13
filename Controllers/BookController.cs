using Microsoft.AspNetCore.Mvc;
using VPLab13.Data;
using VPLab13.Models;
using System.Linq;

namespace VPLab13.Controllers
{
    public class BooksController : Controller
    {
        private readonly AppDbContext _context;

        public BooksController(AppDbContext context)
        {
            _context = context;
        }

        public IActionResult Index(string searchString)
        {
            var books = from b in _context.Books
                        select b;

            if (!string.IsNullOrEmpty(searchString))
            {
                books = books.Where(b => b.Title.Contains(searchString) || b.Author.Contains(searchString));
            }

            return View(books.ToList());
        }

        public IActionResult Create() => View();

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Book book)
        {
            if (ModelState.IsValid)
            {
                _context.Books.Add(book);
                _context.SaveChanges();
                TempData["SuccessMessage"] = "Book created successfully!";
                return RedirectToAction(nameof(Index));
            }
            TempData["ErrorMessage"] = "Failed to create book.";
            return View(book);
        }

        public IActionResult Edit(int? id)
        {
            if (id == null) return NotFound();
            var book = _context.Books.Find(id);
            if (book == null) return NotFound();
            return View(book);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, Book book)
        {
            if (id != book.Id) return NotFound();

            if (ModelState.IsValid)
            {
                _context.Books.Update(book);
                _context.SaveChanges();
                TempData["SuccessMessage"] = "Book updated successfully!";
                return RedirectToAction(nameof(Index));
            }
            TempData["ErrorMessage"] = "Failed to update book.";
            return View(book);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(int id)
        {
            var book = _context.Books.Find(id);
            if (book != null)
            {
                _context.Books.Remove(book);
                _context.SaveChanges();
                TempData["SuccessMessage"] = "Book deleted successfully!";
            }
            else
            {
                TempData["ErrorMessage"] = "Book not found.";
            }
            return RedirectToAction(nameof(Index));
        }
    }
}
