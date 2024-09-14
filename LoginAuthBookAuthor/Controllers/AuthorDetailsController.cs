using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using LoginAuthBookAuthor.Data;
using LoginAuthBookAuthor.Models;
using LoginAuthBookAuthor.ViewModels;
using NHibernate.Linq;

namespace LoginAuthBookAuthor.Controllers
{
    public class AuthorDetailsController : Controller
    {
        // GET: AuthorDetail
        public ActionResult Details()
        {
            if (Session["AuthorId"] == null)
            {
                return RedirectToAction("Login", "Author");
            }
            Guid authorId = (Guid)Session["AuthorId"];
            using (var session = NHibernateHelper.CreateSession())
            {
                var authordetail = session.Query<AuthorDetails>().Fetch(ad => ad.Author).FirstOrDefault(ad => ad.Author.Id == authorId);
                return View(authordetail);
            }
        }

        public ActionResult Edit(Guid id)
        {
            if (Session["AuthorId"] == null)
            {
                return RedirectToAction("Login", "Author");
            }
            Guid authorId = (Guid)Session["AuthorId"];
            using (var session = NHibernateHelper.CreateSession())
            {
                var authordetail = session.Query<AuthorDetails>().FirstOrDefault(ad => ad.Author.Id == authorId && ad.Id == id);
                return View(authordetail);
            }
        }
        [HttpPost]
        public ActionResult Edit(AuthorDetails authorDetail)
        {
            if (Session["AuthorId"] == null)
            {
                return RedirectToAction("Login", "Author");
            }
            using (var session = NHibernateHelper.CreateSession())
            {
                using (var transaction = session.BeginTransaction())
                {
                    var existingAuthorDetail = session.Get<AuthorDetails>(authorDetail.Id);
                    existingAuthorDetail.Street = authorDetail.Street;
                    existingAuthorDetail.City = authorDetail.City;
                    existingAuthorDetail.State = authorDetail.State;
                    existingAuthorDetail.Country = authorDetail.Country;
                    existingAuthorDetail.IsActive = true;
                    session.Update(existingAuthorDetail);
                    transaction.Commit();
                    return RedirectToAction("Details");
                }
            }
        }

        public ActionResult Delete(Guid id)
        {
            if (Session["AuthorId"] == null)
            {
                return RedirectToAction("Login", "Author");
            }
            Guid authorId = (Guid)Session["AuthorId"];
            using (var session = NHibernateHelper.CreateSession())
            {
                var authordetail = session.Query<AuthorDetails>().FirstOrDefault(ad => ad.Author.Id == authorId && ad.Id == id);
                return View(authordetail);
            }
        }
        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(Guid id)
        {
            using (var session = NHibernateHelper.CreateSession())
            {
                using (var transaction = session.BeginTransaction())
                {
                    var existingAuthorDetail = session.Get<AuthorDetails>(id);
                    existingAuthorDetail.IsActive = false;
                    session.Update(existingAuthorDetail);
                    transaction.Commit();
                    return RedirectToAction("Details");
                }
            }
        }









































        //// GET: AuthorDetails
        //public ActionResult Details()
        //{
        //    //check if the user is logged in 
        //    if (Session["AuthorId"] == null)
        //    {
        //        return RedirectToAction("Login", "Author");
        //    }
        //    Guid authorId = (Guid)Session["AuthorId"];

        //    using (var session = NHibernateHelper.CreateSession())
        //    {
        //        //fetch the author and their details
        //        var author = session.Query<Author>().FirstOrDefault(a => a.Id == authorId);

        //        if (author != null)
        //        {
        //            var viewModel = new AuthorDetailsViewModel
        //            {
        //                Author = author,
        //                AuthorDetails = author.AuthorDetails,
        //            };
        //            return View(viewModel);
        //        }
        //        else
        //        {
        //            return HttpNotFound();
        //        }
        //    }

        //}


        //[HttpGet]
        //public ActionResult Edit(Guid id) { 

        //}

        //[HttpPost]


        //[HttpGet]
        //public ActionResult Edit(Guid id)
        //{
        //    //check if the user is logged in
        //    if (Session["AuthorId"] == null)
        //    {
        //        return RedirectToAction("Login", "Author");
        //    }


        //    using (var session = NHibernateHelper.CreateSession())
        //    {
        //        var author = session.Query<Author>().FirstOrDefault(a => a.Id == id);
        //        if (author != null)
        //        {
        //            var viewModel = new AuthorDetailsViewModel
        //            {
        //                Author = author,
        //                AuthorDetails = author.AuthorDetails,
        //            };
        //            return View(viewModel);
        //        }
        //        else
        //        {
        //            return HttpNotFound();
        //        }
        //    }
        //}


        //[HttpPost]
        //public ActionResult Edit(AuthorDetailsViewModel viewModel)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return View(viewModel);
        //    }
        //    using (var session = NHibernateHelper.CreateSession())
        //    {
        //        using (var transaction = session.BeginTransaction())
        //        {
        //            var author = session.Get<Author>(viewModel.Author.Id);
        //            if (author != null)
        //            {
        //                //updating the author props
        //                author.UserName = viewModel.Author.UserName;
        //                author.Age = viewModel.Author.Age;
        //                author.Email = viewModel.Author.Email;
        //                //author.Password = viewModel.Author.Password;

        //                // Update password only if it's provided //otherwise it will throw error of hashing
        //                if (!string.IsNullOrEmpty(viewModel.Author.Password))
        //                {
        //                    author.Password = PasswordHelper.HashPassword(viewModel.Author.Password);
        //                }

        //                //updatethe author details props
        //                if (viewModel.AuthorDetails != null)
        //                {
        //                    var details = session.Get<AuthorDetails>(viewModel.AuthorDetails.Id);
        //                    if (details != null)
        //                    {
        //                        details.Street = viewModel.AuthorDetails.Street;
        //                        details.City = viewModel.AuthorDetails.City;
        //                        details.State = viewModel.AuthorDetails.State;
        //                        details.Country = viewModel.AuthorDetails.Country;
        //                    }
        //                }

        //                session.Update(author);
        //                transaction.Commit();
        //                return RedirectToAction("Details");
        //            }
        //            else
        //            {
        //                return HttpNotFound();
        //            }
        //        }

        //    }

        //}

        ////deleting author details only and not author 
        //public ActionResult Delete(Guid id)
        //{
        //    //checking if the author is logged in 

        //    if (Session["AuthorId"] == null)
        //    {
        //        return RedirectToAction("Login", "Author");
        //    }
        //    using(var session = NHibernateHelper.CreateSession())
        //    {
        //        var authorDetails = session.Get<AuthorDetails>(id);
        //        return View(authorDetails);
        //    }
        //}

        //[HttpPost, ActionName("Delete")]
        //public ActionResult DeleteConfirmed(Guid id)
        //{
        //    //check if the author is logged in
        //    var authorIdFromSession = Session["AuthorId"];
        //    if(authorIdFromSession == null)
        //    {
        //        return RedirectToAction("Login", "Author");
        //    }
        //    using(var session = NHibernateHelper.CreateSession())
        //    {
        //        using(var transaction = session.BeginTransaction())
        //        {
        //            var authorDetailsId = session.Get<AuthorDetails>(id);
        //            if (authorDetailsId != null && authorDetailsId.Author.Id == (Guid)authorIdFromSession)
        //            {
        //                //session.Delete(authorDetailsId);
        //                authorDetailsId.IsDeleted = true; //soft delete 

        //                //authorDetailsId.Street = null; 
        //                //authorDetailsId.City = null;
        //                //authorDetailsId.State = null;
        //                //authorDetailsId.Country = null;
        //                session.Update(authorDetailsId);
        //                transaction.Commit();
        //            }
        //            return RedirectToAction("Details", "AuthorDetails", new { id = authorIdFromSession });
        //        //new {id = authorIdFromSession}
        //    }
        //}

        //}

        //[HttpGet]
        //public ActionResult Edit(Guid id)
        //{
        //    if (Session["AuthorId"] == null)
        //    {
        //        return RedirectToAction("Login", "Author");
        //    }
        //    Guid authorId = (Guid)Session["AuthorId"];
        //    using(var session = NHibernateHelper.CreateSession())
        //    {
        //        var authorDetails = session.Query<AuthorDetails>().FirstOrDefault(a => a.Author.Id == authorId && a.Id == id);
        //        return View(authorDetails);
        //    }
        //}

        //[HttpPost]
        //public ActionResult Edit(AuthorDetails authorDetails)
        //{
        //    if (Session["AuthorId"] == null)
        //    {
        //        return RedirectToAction("Login", "Author");
        //    }
        //    using(var session = NHibernateHelper.CreateSession())
        //    {
        //        using(var transaction = session.BeginTransaction())
        //        {
        //            var existingAuthorDetails = session.Get<AuthorDetails>(authorDetails.Id);
        //            if (existingAuthorDetails == null)
        //            {
        //                // Handle the case where the details do not exist
        //                ModelState.AddModelError("", "Author details not found.");
        //                return View(authorDetails); // Return the view with an error
        //            }
        //            existingAuthorDetails.Street = authorDetails.Street;
        //            existingAuthorDetails.City = authorDetails.City;
        //            existingAuthorDetails.State = authorDetails.State;
        //            existingAuthorDetails.Country = authorDetails.Country;
        //            existingAuthorDetails.IsDeleted = true;
        //            session.Update(existingAuthorDetails);
        //            transaction.Commit();
        //            return RedirectToAction("Details");
        //        }
        //    }
        //}

    }
   

}
