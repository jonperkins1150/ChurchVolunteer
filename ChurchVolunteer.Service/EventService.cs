using ChurchVolunteer.Data;
using ChurchVolunteer.Model.Event;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChurchVolunteer.Service
{
    public class EventService
    {
        private readonly Guid _userId;

        public EventService(Guid userId)
        {
            _userId = userId;
        }
        public bool CreateEvent(EventCreate model)
        {
            var entity =
                new Event()
                {
                    EventId = model.EventId,
                    UserId = _userId,   
                    Day = model.Day,
                    ServiceDate = model.ServiceDate,
                    Location = model.Location,
                    RequiredVolunteers = model.RequiredVolunteers,
                    RemainingNeed = model.RemainingNeed,
                };
            using (var ctx = new ApplicationDbContext())
            {
                ctx.Events.Add(entity);
                return ctx.SaveChanges() == 1;
            }
        }

        public IEnumerable<EventListItem> GetEvents()
        {
            using (var ctx = new ApplicationDbContext())
            {
                var query =
                    ctx
                        .Events
                        .Where(e => e.UserId == _userId)
                        .Select(
                            e =>
                                new EventListItem
                                {
                                    EventId = e.EventId,
                                    UserId = _userId,
                                    Day = e.Day,
                                    ServiceDate = e.ServiceDate,
                                    Location = e.Location,
                                    RequiredVolunteers = e.RequiredVolunteers,
                                    RemainingNeed = e.RemainingNeed,            
                                }
                        );
                return query.ToArray();
            }
        }
        public EventEdit GetEventById(int id)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity =
                    ctx
                        .Events
                        .Single(e => e.EventId == id);

                var signups =
                    ctx
                    .Events
                    .Where(s => s.EventId == id)
                    .ToList();

                return
                        new EventEdit
                        {
                            EventId = entity.EventId,
                            UserId = entity.UserId,
                            Day = entity.Day,
                            ServiceDate = entity.ServiceDate,
                            Location = entity.Location,
                            RequiredVolunteers = entity.RequiredVolunteers,
                            RemainingNeed = entity.RequiredVolunteers - 2,
                        };
            }
        }

        public EventDetail GetDetailById(int id)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity =
                    ctx
                        .Events
                        //.Where(e => e.UserId == _userId)
                        .Single(e => e.EventId == id);

                var signups =
                    ctx
                    .Events
                    .Where(s => s.EventId == id)
                    .ToList();

                return
                        new EventDetail
                        {
                            EventId = entity.EventId,
                            UserId = entity.UserId,
                            Day = entity.Day,
                            ServiceDate = entity.ServiceDate,
                            Location = entity.Location,
                            RequiredVolunteers = entity.RequiredVolunteers,
                            RemainingNeed = entity.RequiredVolunteers - signups.Count(),
                        };
            }
        }


        public bool UpdateEvent(EventEdit model)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity =
                ctx
                    .Events
                    .Single(e => e.EventId == model.EventId);

                var signups =
                  ctx
                  .SignUps
                  //.Where(s => s.EventId == id)
                  .ToList();

                entity.EventId = model.EventId;
                entity.UserId = _userId;
                entity.Day = model.Day;
                entity.ServiceDate = model.ServiceDate;
                entity.Location = model.Location;
                entity.RequiredVolunteers = model.RequiredVolunteers;
                entity.RemainingNeed = entity.RequiredVolunteers - signups.Count();

                return ctx.SaveChanges() == 1;
            }
        }

        public bool DeleteEvent(int id)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity =
                    ctx
                    .Events
                    .Single(e => e.EventId == id);

                ctx.Events.Remove(entity);

                return ctx.SaveChanges() == 1;
            }
        }


    }
}
