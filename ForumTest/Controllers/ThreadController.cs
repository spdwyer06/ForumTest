using FT_Models.ThreadViewModels;
using FT_Services;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ForumTest.Controllers
{
    public class ThreadController : Controller
    {
        //GET: /Thread/ThreadPostIndex
        public ActionResult ThreadPostIndex(int threadID)
        {
            var service = new PostService();
            service.GetPostsByThreadID(threadID);
            //var model = service.GetPostsByThreadID(threadID);

            //return View(model);
            return RedirectToAction("ThreadPostIndex", "Post", new { threadID });

        }

        //GET: /Thread/Index
        public ActionResult Index()
        {
            var service = new ThreadService();
            var model = service.GetThreads();

            return View(model);
        }
        //GET: /Thread/MyThreadsIndex
        public ActionResult MyThreadsIndex()
        {
            var service = CreateThreadService();
            var model = service.GetMyThreads();

            return View(model);
        }

        //GET: /Thread/Create
        [Authorize]
        public ActionResult Create()
        {
            return View();
        }

        // POST: /Thread/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(ThreadCreate model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var service = CreateThreadService();

            if (service.CreateThread(model))
            {
                // TempData removes information after it's accessed
                TempData["SaveResult"] = "Your thread was created.";
                return RedirectToAction("Index");
            }

            ModelState.AddModelError("", "Thread could not be created.");
            return View(model);
        }

        // GET: /Thread/Details/{id}
        public ActionResult Details(int id)
        {
            //var service = CreateThreadService();
            var service = new ThreadService();
            var model = service.GetThreadByID(id);

            return View(model);
        }

        // GET: /Thread/MyThreadsDetails/{id}
        public ActionResult MyThreadsDetails(int id)
        {
            var service = CreateThreadService();
            var model = service.GetThreadByID(id);

            return View(model);
        }

        // GET: /Thread/Edit/{id}
        public ActionResult Edit(int id)
        {
            var service = CreateThreadService();
            var detail = service.GetThreadByID(id);
            var model = new ThreadEdit
            {
                ThreadID = detail.ThreadID,
                ThreadTitle = detail.ThreadTitle,
                ThreadDescription = detail.ThreadDescription
            };

            if (service.ValidateUser(id) == true)
                return View(model);

            return View("ValidationFailed");
        }

        // POST: /Thread/Edit/{id}
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, ThreadEdit model)
        {
            if (!ModelState.IsValid)
                return View(model);

            if (model.ThreadID != id)
            {
                ModelState.AddModelError("", "ID Mismatch");
                return View(model);
            }

            var service = CreateThreadService();

            if (service.UpdateThread(model))
            {
                TempData["SaveResult"] = "Your thread was updated.";
                return RedirectToAction("Index");
            }

            ModelState.AddModelError("", "Your thread could not be updated.");
            return View(model);
        }

        // GET: /Thread/MyThreadsEdit/{id}
        public ActionResult MyThreadsEdit(int id)
        {
            var service = CreateThreadService();
            var detail = service.GetThreadByID(id);
            var model = new ThreadEdit
            {
                ThreadID = detail.ThreadID,
                ThreadTitle = detail.ThreadTitle,
                ThreadDescription = detail.ThreadDescription
            };

            return View(model);
        }

        // POST: /Thread/MyThreadsEdit/{id}
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult MyThreadsEdit(int id, ThreadEdit model)
        {
            if (!ModelState.IsValid)
                return View(model);

            if (model.ThreadID != id)
            {
                ModelState.AddModelError("", "ID Mismatch");
                return View(model);
            }

            var service = CreateThreadService();

            if (service.UpdateThread(model))
            {
                TempData["SaveResult"] = "Your thread was updated.";
                return RedirectToAction("MyThreadsIndex");
            }

            ModelState.AddModelError("", "Your thread could not be updated.");
            return View(model);
        }

        // GET: /Thread/Delete/{id}
        public ActionResult Delete(int id)
        {
            var service = CreateThreadService();
            var model = service.GetThreadByID(id);

            if (service.ValidateUser(id) == true)
                return View(model);

            return View("ValidationFailed");
        }

        // POST: /Thread/Delete/{id}
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteThread(int id)
        {
            var service = CreateThreadService();

            service.DeleteThread(id);

            TempData["SaveResult"] = "Your thread was deleted.";

            return RedirectToAction("Index");
        }

        // GET: /Thread/MyThreadsDelete/{id}
        public ActionResult MyThreadsDelete(int id)
        {
            var service = CreateThreadService();
            var model = service.GetThreadByID(id);

            return View(model);
        }

        // POST: /Thread/MyThreadsDelete/{id}
        [HttpPost, ActionName("MyThreadsDelete")]
        [ValidateAntiForgeryToken]
        public ActionResult MyThreadsDeleteThread(int id)
        {
            var service = CreateThreadService();

            service.DeleteThread(id);

            TempData["SaveResult"] = "Your thread was deleted.";

            return RedirectToAction("MyThreadsIndex");
        }


        private ThreadService CreateThreadService()
        {
            var userID = Guid.Parse(User.Identity.GetUserId());
            var service = new ThreadService(userID);
            return service;
        }
    }
}