using ChurchVolunteer.Data;
using ChurchVolunteer.Model.Event;
using ChurchVolunteer.Model.SignUp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChurchVolunteer.Service
{
    public class SignUpService
    {
        private readonly Guid _userId;

        public SignUpService(Guid userId)
        {
            _userId = userId;
        }

        public bool CreateSignUp(SignUpCreate model)
        {
            var entity =
                new SignUp()
                {
                    UserId = _userId,
                    EventId = model.EventId,
                    VolunteerId = model.VolunteerId,
                    CreatedUtc = DateTimeOffset.Now,
                };

            using (var ctx = new ApplicationDbContext())
            {
                ctx.SignUps.Add(entity);
                return ctx.SaveChanges() == 1;
            }
        } //TODO
        public IEnumerable<SignUpListItem> GetSignUp()
        {
            using (var ctx = new ApplicationDbContext())
            {

                var query =
                    ctx
                        .SignUps
                        .Where(e => e.UserId == _userId)
                        .Select(
                            e =>
                                new SignUpListItem
                                {
                                    SignUpId = e.SignUpId,
                                    EventId = e.EventId,
                                    VolunteerId = e.VolunteerId,
                                    UserId = e.UserId,

                                }
                        );
                return query.ToArray();


            }
        }
        public IEnumerable<SignUpListItem> GetSignUpsByEventId(EventEdit eventDetail)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var query =
                    ctx
                    .SignUps
                    .Where(e => e.EventId == eventDetail.EventId)
                    .Select(e => new SignUpListItem
                    {
                        SignUpId = e.SignUpId,
                        UserId = e.UserId,
                        EventId = eventDetail.EventId,
                        VolunteerId = e.VolunteerId,

                    }
                     );
                return query.ToArray();
            }
        }

        public SignUpDetail GetSignUpById(int id)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity =
                    ctx
                        .SignUps
                        .Where(e => e.UserId == _userId)
                        .Single(e =>e.SignUpId == id);
                var volunteer =
                    ctx
                    .Volunteers
                    .Where(v => v.UserId == _userId)
                    .Single(v => v.VolunteerId == entity.VolunteerId);
                var eventz =
                     ctx
                    .Events
                    //.Where(t => t.UserId == _userId)
                    .Single(t => t.EventId == entity.EventId);
                return
                        new SignUpDetail
                        {
                            SignUpId = entity.SignUpId,
                            VolunteerId = entity.VolunteerId,
                            EventId = entity.EventId,
                            UserId = _userId,
                            LoginId = volunteer.LoginId,
                            FirstName = volunteer.FirstName,
                            LastName = volunteer.LastName,
                            PhoneNumber = volunteer.PhoneNumber,
                            EmailAddress = volunteer.EmailAddress,
                            Day = eventz.Day,
                            ServiceDate = eventz.ServiceDate,
                            Location = eventz.Location,
                            CreatedUtc = entity.CreatedUtc,

                        };
            }
        }
        public bool UpdateSignUp(SignUpEdit model)
        {
            using (var ctx = new ApplicationDbContext())
            {


                var signups =
                  ctx
                  .SignUps
                   .Where(s => s.SignUpId == model.SignUpId)
                  .ToList();

                 var entity =
                ctx
                    .Events
                    .Single(e => e.EventId == model.EventId);

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

        public bool DeleteSignUp(int id)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity =
                    ctx
                    .SignUps
                    .Single(e => e.SignUpId == id);

                ctx.SignUps.Remove(entity);

                return ctx.SaveChanges() == 1;
            }
        }
    }
}
