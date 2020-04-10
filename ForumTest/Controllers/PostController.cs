using FT_Models.PostViewModels;
using FT_Services;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ForumTest.Controllers
{
    public class PostController : Controller
    {
        //GET: /Post/ThreadPostIndex
        public ActionResult ThreadPostIndex(int threadID)
        {
            var service = new PostService();
            var model = service.GetPostsByThreadID(threadID);

            return View(model);
        }

        //GET: /Post/AllPostsIndex
        public ActionResult AllPostsIndex()
        {
            var userID = Guid.Parse(User.Identity.GetUserId());
            var service = new PostService(userID);
            var model = service.GetAllPosts();

            return View(model);
        }

        //GET: /Post/Index
        public ActionResult Index()
        {
            var userID = Guid.Parse(User.Identity.GetUserId());
            var service = new PostService(userID);
            var model = service.GetPosts();

            return View(model);
        }

        //GET: /Post/Create
        [Authorize]
        public ActionResult Create()
        {
            return View();
        }

        // POST: /Post/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(PostCreate model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var service = CreatePostService();

            if (service.CreatePost(model))
            {
                // TempData removes information after it's accessed
                TempData["SaveResult"] = "Your post was created.";
                return RedirectToAction("Index");
            }

            ModelState.AddModelError("", "Post could not be created.");
            return View(model);
        }

        // GET: /Post/Details/{id}
        public ActionResult Details(int id)
        {
            var service = CreatePostService();
            //var service = new ThreadService();
            var model = service.GetPostByID(id);

            return View(model);
        }

        // GET: /Post/Edit/{id}
        public ActionResult Edit(int id)
        {
            var service = CreatePostService();
            var detail = service.GetPostByID(id);
            var model = new PostEdit
            {
                PostID = detail.PostID,
                PostContent = detail.PostContent,
            };

            //return View(model);

            if (service.ValidateUser(id) == true)
                return View(model);

            return View("ValidationFailed");
        }

        // POST: /Post/Edit/{id}
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, PostEdit model)
        {
            if (!ModelState.IsValid)
                return View(model);

            if (model.PostID != id)
            {
                ModelState.AddModelError("", "ID Mismatch");
                return View(model);
            }

            var service = CreatePostService();

            if (service.UpdatePost(model))
            {
                TempData["SaveResult"] = "Your post was updated.";
                return RedirectToAction("Index");
            }

            ModelState.AddModelError("", "Your post could not be updated.");
            return View(model);
        }

        // GET: /Post/Delete/{id}
        [ActionName("Delete")]
        public ActionResult Delete(int id)
        {
            var service = CreatePostService();
            var model = service.GetPostByID(id);

            //return View(model);

            if (service.ValidateUser(id) == true)
                return View(model);

            return View("ValidationFailed");
        }

        // POST: /Post/Delete/{id}
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeletePost(int id)
        {
            var service = CreatePostService();

            service.DeletePost(id);

            TempData["SaveResult"] = "Your post was deleted.";

            return RedirectToAction("Index");
        }



        private PostService CreatePostService()
        {
            var userID = Guid.Parse(User.Identity.GetUserId());
            var service = new PostService(userID);
            return service;
        }
    }
}