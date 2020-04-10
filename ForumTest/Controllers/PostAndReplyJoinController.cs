using FT_Models.PostAndReplyJoinViewModels;
using FT_Services;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ForumTest.Controllers
{
    public class PostAndReplyJoinController : Controller
    {
        // GET: PostAndReplyJoin
        public ActionResult Index()
        {
            var userID = Guid.Parse(User.Identity.GetUserId());
            var service = new PostAndReplyJoinService(userID);
            var model = service.GetPostAndReplyJoins();

            return View(model);
        }

        //GET: /PostAndReplyJoin/Create
        [Authorize]
        public ActionResult Create()
        {
            return View();
        }

        // POST: /PostAndReplyJoin/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(PostAndReplyJoinCreate model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var service = CreatePostAndReplyJoinService();

            if (service.CreatePostAndReplyJoin(model))
            {
                // TempData removes information after it's accessed
                TempData["SaveResult"] = "Your join was created.";
                return RedirectToAction("Index");
            }

            ModelState.AddModelError("", "Join could not be created.");
            return View(model);
        }

        // GET: /PostAndReplyJoin/Details/{id}
        public ActionResult Details(int id)
        {
            var service = CreatePostAndReplyJoinService();
            var model = service.GetPostAndReplyJoinByID(id);

            return View(model);
        }

        // GET: /Post/Edit/{id}
        public ActionResult Edit(int id)
        {
            var service = CreatePostAndReplyJoinService();
            var detail = service.GetPostAndReplyJoinByID(id);
            var model = new PostAndReplyJoinEdit
            {
                ID = detail.ID,
                PostContent = detail.PostContent,
                ReplyContent = detail.ReplyContent
            };

            //return View(model);

            if (service.ValidateUser(id) == true)
                return View(model);

            return View("ValidationFailed");
        }

        // POST: /PostAndReplyJoin/Edit/{id}
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, PostAndReplyJoinEdit model)
        {
            if (!ModelState.IsValid)
                return View(model);

            if (model.ID != id)
            {
                ModelState.AddModelError("", "ID Mismatch");
                return View(model);
            }

            var service = CreatePostAndReplyJoinService();

            if (service.UpdatePostAndReplyJoin(model))
            {
                TempData["SaveResult"] = "Your join was updated.";
                return RedirectToAction("Index");
            }

            ModelState.AddModelError("", "Your join could not be updated.");
            return View(model);
        }

        // GET: /PostAndReplyJoin/Delete/{id}
        public ActionResult Delete(int id)
        {
            var service = CreatePostAndReplyJoinService();
            var model = service.GetPostAndReplyJoinByID(id);

            //return View(model);

            if (service.ValidateUser(id) == true)
                return View(model);

            return View("ValidationFailed");
        }

        // POST: /PostAndReplyJoin/Delete/{id}
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeletePost(int id)
        {
            var service = CreatePostAndReplyJoinService();

            service.DeletePostAndReplyJoin(id);

            TempData["SaveResult"] = "Your join was deleted.";

            return RedirectToAction("Index");
        }



        private PostAndReplyJoinService CreatePostAndReplyJoinService()
        {
            var userID = Guid.Parse(User.Identity.GetUserId());
            var service = new PostAndReplyJoinService(userID);
            return service;
        }

    }
}