using ChurchVolunteer.Model.Volunteer;
using ChurchVolunteer.Service;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ChurchVolunteer.WebMVC.Controllers
{
    public class VolunteerController : Controller
    {
        // GET: Volunteer
        public ActionResult Index()
        {
            var userId = Guid.Parse(User.Identity.GetUserId());
            var service = new VolunteerService(userId);
            var model = service.GetVolunteers();

            return View(model);
        }
//-----------------------------------------------------------------------------------------------
        //CREATE VOLUNTEER
        public ActionResult Create()
        {
            return View();
        }
        // PERSIST TO DATABASE: VOLUNTEER
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(VolunteerCreate model)
        {
            if (!ModelState.IsValid) return View(model);

            var service = CreateVolunteerService();

            if (service.CreateVolunteer(model))
            {
                TempData["SaveResult"] = "The Volunteer was created.";
                return RedirectToAction("Index");
            }
            ModelState.AddModelError("", "The Volunteer could not be created.");

            return View(model);
        }
        private VolunteerService CreateVolunteerService()
        {
            var userId = Guid.Parse(User.Identity.GetUserId());
            var service = new VolunteerService(userId);
            return service;
        }
//-----------------------------------------------------------------------------------------------

        // DETAILS: VOLUNTEER
        public ActionResult Details(int id)
        {
            var svc = CreateVolunteerService();
            var model = svc.GetVolunteerById(id);
            return View(model);
        }
//-----------------------------------------------------------------------------------------------
        // EDIT: VOLUNTEER
        public ActionResult Edit(int id)
        {
            var service = CreateVolunteerService();
            var detail = service.GetVolunteerByIdForEdit(id);
            var model =
                new VolunteerEdit
                {
                    VolunteerId = detail.VolunteerId,
                    UserId = detail.UserId,
                    LoginId = detail.LoginId,
                    FirstName = detail.FirstName,
                    LastName = detail.LastName,
                    PhoneNumber = detail.PhoneNumber,
                    EmailAddress = detail.EmailAddress,
                    Day = detail.Day,
                    Location = detail.Location,
                    CreatedUtc = detail.CreatedUtc,

                };
            return View(model);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, VolunteerEdit model)
        {
            if (!ModelState.IsValid) return View(model);
            if (model.VolunteerId != id)
            {
                ModelState.AddModelError("", "Id Mismatch");
                return View(model);
            }

            var service = CreateVolunteerService();

            if (service.UpdateVolunteer(model))
            {
                TempData["SaveResult"] = "The Volunteer has been updated.";
                return RedirectToAction("Index");
            }
            ModelState.AddModelError("", "The Volunteer could not be updated.");
            return View();
        }
//-----------------------------------------------------------------------------------------------
        // DELETE: VOLUNTEER
        public ActionResult Delete(int id)
        {
            var svc = CreateVolunteerService();
            var model = svc.GetVolunteerById(id);
            return View(model);
        }
        [HttpPost]
        [ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeletePost(int id)
        {
            var service = CreateVolunteerService();

            service.DeleteVolunteer(id);

            TempData["SaveResult"] = "The Volunteer has been deleted.";

            return RedirectToAction("Index");
        }

    }
}