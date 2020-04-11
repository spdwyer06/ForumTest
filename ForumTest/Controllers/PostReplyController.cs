using FT_Models.PostReplyViewModels;
using FT_Services;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ForumTest.Controllers
{
    public class PostReplyController : Controller
    {
        public ActionResult MyRepliesIndex()
        {
            var userID = Guid.Parse(User.Identity.GetUserId());
            var service = new PostReplyService(userID);
            var model = service.GetReplies();

            return View(model);
        }

        // GET: /PostReply
        public ActionResult Index()
        {
            var userID = Guid.Parse(User.Identity.GetUserId());
            var service = new PostReplyService(userID);
            var model = service.GetAllReplies();

            return View(model);
        }

        //GET: /PostReply/Create
        [Authorize]
        public ActionResult Create()
        {
            return View();
        }

        // POST: /PostReply/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(PostReplyCreate model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var service = CreateReplyService();

            if (service.CreatePostReply(model))
            {
                // TempData removes information after it's accessed
                TempData["SaveResult"] = "Your reply was created.";
                return RedirectToAction("Index");
            }

            ModelState.AddModelError("", "Reply could not be created.");
            return View(model);
        }

        // GET: /PostReply/Details/{id}
        public ActionResult Details(int id)
        {
            var service = CreateReplyService();
            var model = service.GetReplyByID(id);

            return View(model);
        }

        // GET: /PostReply/Edit/{id}
        public ActionResult Edit(int id)
        {
            var service = CreateReplyService();
            var detail = service.GetReplyByID(id);
            var model = new PostReplyEdit
            {
                ReplyID = detail.ReplyID,
                ReplyContent = detail.ReplyContent,
            };

            if (service.ValidateUser(id) == true)
                return View(model);

            return View("ValidationFailed");

        }

        // POST: /PostReply/Edit/{id}
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, PostReplyEdit model)
        {
            if (!ModelState.IsValid)
                return View(model);

            if (model.ReplyID != id)
            {
                ModelState.AddModelError("", "ID Mismatch");
                return View(model);
            }

            var service = CreateReplyService();

            if (service.UpdateReply(model))
            {
                TempData["SaveResult"] = "Your reply was updated.";
                return RedirectToAction("Index");
            }

            ModelState.AddModelError("", "Your reply could not be updated.");
            return View(model);
        }

        // GET: /PostReply/Delete/{id}
        [ActionName("Delete")]
        public ActionResult Delete(int id)
        {
            var service = CreateReplyService();
            var model = service.GetReplyByID(id);

            if (service.ValidateUser(id) == true)
                return View(model);

            return View("ValidationFailed");
        }

        // POST: /PostReply/Delete/{id}
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteReply(int id)
        {
            var service = CreateReplyService();

            service.DeleteReply(id);

            TempData["SaveResult"] = "Your reply was deleted.";

            return RedirectToAction("Index");
        }



        private PostReplyService CreateReplyService()
        {
            var userID = Guid.Parse(User.Identity.GetUserId());
            var service = new PostReplyService(userID);
            return service;
        }
    }
}