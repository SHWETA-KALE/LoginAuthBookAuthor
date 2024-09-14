using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using LoginAuthBookAuthor.Data;
using LoginAuthBookAuthor.Models;

namespace LoginAuthBookAuthor.Controllers
{
    public class BookController : Controller
    {
        //showing books of that particular author which islogged in
        public ActionResult GetBooks()
        {
            //checking if the user is logged in 
            if (Session["AuthorId"] == null)
            {
                return RedirectToAction("Login", "Author");
            }
            Guid authorId = (Guid)Session["AuthorId"];

            using (var session = NHibernateHelper.CreateSession())
            {
                //fetch author if author is not null
                var author = session.Query<Author>().FirstOrDefault(a => a.Id == authorId);
                if (author != null)
                {
                    //fetch the books of the author 
                    var books = session.Query<Book>().Where(b => b.Author.Id == authorId).ToList();
                    return View(books);
                }
                else
                {
                    //no books
                    return RedirectToAction("Create");
                }

            }
        }


        [HttpGet]
        public ActionResult Create()
        {
            if (Session["AuthorId"] == null)
            {
                return RedirectToAction("Login", "Author");
            }
            Guid authorId = (Guid)Session["AuthorId"];
            return View();

        }
        [HttpPost]
        public ActionResult Create(Book book)
        {
            Guid authorId = (Guid)Session["AuthorId"];
            using (var session = NHibernateHelper.CreateSession())
            {
                using (var transaction = session.BeginTransaction())
                {
                    var author = session.Query<Author>().FirstOrDefault(a => a.Id == authorId);
                    if (author != null)
                    {
                        book.Author = author;
                        session.Save(book);
                        transaction.Commit();
                    }
                }
            }
            return RedirectToAction("GetBooks", new { authorId });
        }

        [HttpGet]

        public ActionResult Edit(Guid bookid)
        {
            if (Session["AuthorId"] == null)
            {
                return RedirectToAction("Login", "Author");
            }
            Guid authorId = (Guid)Session["AuthorId"];


            using (var session = NHibernateHelper.CreateSession())
            {

                //finding book 
                var bookToEdit = session.Query<Book>().FirstOrDefault(b => b.Id == bookid && b.Author.Id == authorId);
                if (bookToEdit == null)
                {
                    return HttpNotFound(); // Book not found or doesn't belong to the logged-in author
                }
                return View(bookToEdit);


            }
        }

        [HttpPost]
        public ActionResult Edit(Book book)
        {
            if (Session["AuthorId"] == null)
            {
                return RedirectToAction("Login", "Author");
            }
            Guid authorId = (Guid)Session["AuthorId"];
            using (var session = NHibernateHelper.CreateSession())
            {
                using (var transaction = session.BeginTransaction())
                {
                    var existingBook = session.Query<Book>().FirstOrDefault(b => b.Id == book.Id && b.Author.Id == authorId);
                    if (existingBook == null)
                    {
                        return HttpNotFound(); // Book not found or doesn't belong to the logged-in author
                    }
                    if (existingBook != null)
                    {
                        existingBook.Name = book.Name;
                        existingBook.Genre = book.Genre;
                        existingBook.Description = book.Description;
                        session.Update(existingBook);
                        transaction.Commit();
                    }

                }
                return RedirectToAction("GetBooks", new { authorId });
            }
        }

        [HttpGet]
        public ActionResult Delete(Guid id)
        {
            if (Session["AuthorId"] == null)
            {
                return RedirectToAction("Login", "Author");
            }
            Guid authorId = (Guid)Session["AuthorId"];

            using (var session = NHibernateHelper.CreateSession())
            {
                var bookToDelete = session.Query<Book>().FirstOrDefault(b => b.Id == id && b.Author.Id == authorId);
                if (bookToDelete == null)
                {
                    return HttpNotFound(); // Book not found or doesn't belong to the logged-in author
                }
                return View(bookToDelete); // Pass the book to the view for confirmation
            }
        }

        [HttpPost, ActionName("Delete")]

        public ActionResult DeleteConfirmed(Guid id)
        {
            if (Session["AuthorId"] == null)
            {
                return RedirectToAction("Login", "Author");
            }
            Guid authorId = (Guid)Session["AuthorId"];

            using (var session = NHibernateHelper.CreateSession())
            {
                using (var transaction = session.BeginTransaction())
                {
                    // Find the book and make sure it belongs to the logged-in author
                    var bookToDelete = session.Query<Book>().FirstOrDefault(b => b.Id == id && b.Author.Id == authorId);
                    if (bookToDelete == null)
                    {
                        return HttpNotFound(); // Book not found or doesn't belong to the logged-in author
                    }

                    session.Delete(bookToDelete); // Delete the book
                    transaction.Commit();
                }
            }

            return RedirectToAction("GetBooks");

        }
    }
}