using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using LoginAuthBookAuthor.Data;
using LoginAuthBookAuthor.Models;
using LoginAuthBookAuthor.ViewModels;

namespace LoginAuthBookAuthor.Controllers
{

    //[Authorize] =>globally authorization is done in global.aspx
   
    public class AuthorController : Controller
    {
       
        public ActionResult Index()
        {
            using (var session = NHibernateHelper.CreateSession())
            {
                var data = session.Query<Author>().ToList();
                return View(data);
            }
        }
        // GET: Author

        [AllowAnonymous]
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        public ActionResult Login(LoginVM loginVM)
        {
            using (var session = NHibernateHelper.CreateSession())
            {
                using (var transaction = session.BeginTransaction())
                {
                    var user = session.Query<Author>().FirstOrDefault(u => u.UserName == loginVM.UserName);
                    if (user != null)
                    {
                        if (PasswordHelper.VerifyPassword(loginVM.Password, user.Password))
                        {
                            FormsAuthentication.SetAuthCookie(loginVM.UserName, true);
                            //storing the user id in the session
                            Session["AuthorId"] = user.Id;
                            return RedirectToAction("Details", "AuthorDetails");
                        }
                    }
                    ModelState.AddModelError("", "UserName/Password doesn't match");
                    return View();
                }
            }
        }

        
        [HttpGet]
        [AllowAnonymous]
        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        public ActionResult Register(Author author)
        {
            using(var session = NHibernateHelper.CreateSession())
            {
                using (var transaction = session.BeginTransaction())
                {
                    author.Password = PasswordHelper.HashPassword(author.Password);
                    author.AuthorDetails.Author = author;
                    session.Save(author);
                    transaction.Commit();
                    return RedirectToAction("Login");
                }
            }
        }

        
        [HttpGet]
        
        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            Session.Clear();
            return RedirectToAction("Login");
        }

        [AllowAnonymous]
        [HttpGet]
        public ActionResult GetAllBooks()
        {
            using( var session = NHibernateHelper.CreateSession())
            {
                var books = session.Query<Book>().ToList();
                return View(books);
            }
           
        }


        

    }
}