using ChurchVolunteer.Model.SignUp;
using ChurchVolunteer.Service;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ChurchVolunteer.WebMVC.Controllers
{
    public class SignUpController : Controller
    {
        // GET: SIGNUP
        public ActionResult Index()
        {
            var userId = Guid.Parse(User.Identity.GetUserId());
            var service = new SignUpService(userId);
            var model = service.GetSignUp();

            return View(model);
        }
        // CREATE: SIGNUP
        public ActionResult Create()
        {
            return View();
        }
        // PERSIST TO DATABASE: SIGNUP
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(SignUpCreate model)
        {
            if (!ModelState.IsValid) return View(model);

            var service = CreateSignUpService();

            if (service.CreateSignUp(model))
            {
                TempData["SaveResult"] = "You Have Signed Up.";
                return RedirectToAction("Index");
            }
            ModelState.AddModelError("", "We were unable to complete your request.");

            return View(model);
        }
        private SignUpService CreateSignUpService()
        {
            var userId = Guid.Parse(User.Identity.GetUserId());
            var service = new SignUpService(userId);
            return service;
        }
        // DETAILS: SIGNUP
        public ActionResult Details(int id)
        {
            var svc = CreateSignUpService();
            var model = svc.GetSignUpById(id);
            return View(model);
        }

        // EDIT: SIGNUP
        public ActionResult Edit(int id)
        {
            var service = CreateSignUpService();
            var detail = service.GetSignUpById(id);
            var model =
                new SignUpEdit
                {
                    EventId = detail.EventId,
                    UserId = detail.UserId,
                    Day = detail.Day,
                    ServiceDate = detail.ServiceDate,
                    Location = detail.Location,
                  

                };
            return View(model);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, SignUpEdit model)
        {
            if (!ModelState.IsValid) return View(model);
            if (model.EventId != id)
            {
                ModelState.AddModelError("", "Id Mismatch");
                return View(model);
            }

            var service = CreateSignUpService();

            if (service.UpdateSignUp(model))
            {
                TempData["SaveResult"] = "Your Changes have been saved .";
                return RedirectToAction("Index");
            }
            ModelState.AddModelError("", "Your Changes could not be saved.");
            return View(model);
        }
        // DELETE: SIGNUP
        [ActionName("Delete")]
        public ActionResult Delete(int id)
        {
            var svc = CreateSignUpService();
            var model = svc.GetSignUpById(id);

            return View(model);
        }
        [HttpPost]
        [ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeletePost(int id)
        {
            var service = CreateSignUpService();

            service.DeleteSignUp(id);
            TempData["SaveResult"] = "The SignUp has been Deleted";

            return RedirectToAction("Index");
        }
    }
}

