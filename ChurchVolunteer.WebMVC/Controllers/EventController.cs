using ChurchVolunteer.Model.Event;
using ChurchVolunteer.Service;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ChurchVolunteer.WebMVC.Controllers
{
    public class EventController : Controller
    {
        // GET: Event
        public ActionResult Index()
        {
            var userId = Guid.Parse(User.Identity.GetUserId());
            var service = new EventService(userId);
            var model = service.GetEvents();

            return View(model);
        }
        // CREATE: Event
        public ActionResult Create()
        {
            return View();
        }
        // PERSIST TO DATABASE:  Event
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(EventCreate model)
        {
            if (!ModelState.IsValid) return View(model);

            var service = CreateEventService();

            if (service.CreateEvent(model))
            {
                TempData["SaveResult"] = "The Event was created.";
                return RedirectToAction("Index");
            }
            ModelState.AddModelError("", "The Event could not be created.");

            return View(model);

        }

        // DETAILS: Event

        public ActionResult Details(int id)
        {
            var svc = CreateEventService();
            var model = svc.GetDetailById(id);

            return View(model);
        }

        private EventService CreateEventService()
        {
            var userId = Guid.Parse(User.Identity.GetUserId());
            var service = new EventService(userId);
            return service;
        }

        // EDIT: Event

        public ActionResult Edit(int id)
        {
            var service = CreateEventService();
            var detail = service.GetEventById(id);
            var model =
                new EventEdit
                {
                    EventId = detail.EventId,
                    UserId = detail.UserId,
                    Day = detail.Day,
                    ServiceDate = detail.ServiceDate,
                    Location = detail.Location,
                    RequiredVolunteers = detail.RequiredVolunteers,
                    CreatedUtc = detail.CreatedUtc,

    };
            return View(model);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, EventEdit model)
        {
            if (!ModelState.IsValid) return View(model);
            //if (model.EventId != id)
            //{
            //    ModelState.AddModelError("", "Id Mismatch");
            //    return View(model);
            //}

            var service = CreateEventService();

            if (service.UpdateEvent(model))
            {
                TempData["SaveResult"] = "The Service information has been updated.";
                return RedirectToAction("Index");
            }
            ModelState.AddModelError("", "The Service information could not be updated.");
            return View(model);
        }
        // DELETE: Volunteer
        [ActionName("Delete")]
        public ActionResult Delete(int id)
        {
            var svc = CreateEventService();
            var model = svc.GetDetailById(id);

            return View(model);
        }
        [HttpPost]
        [ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeletePost(int id)
        {
            var service = CreateEventService();

            service.DeleteEvent(id);
            TempData["SaveResult"] = "The Volunteer has been deleted";

            return RedirectToAction("Index");
        }

    }
}