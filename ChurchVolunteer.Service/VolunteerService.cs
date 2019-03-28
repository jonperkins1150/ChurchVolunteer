using ChurchVolunteer.Data;
using ChurchVolunteer.Model.Volunteer;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChurchVolunteer.Service
{
    public class VolunteerService
    {
        private readonly Guid _userId;

        public VolunteerService(Guid userId)
        {
            _userId = userId;
        }
        //-----------------------------------------------------------------------------------------------
        public bool CreateVolunteer(VolunteerCreate model)
        {
            var entity =
                new Volunteer()
                {
                    UserId = _userId,
                    LoginId = model.LoginId,
                    FirstName = model.FirstName,
                    LastName = model.LastName,
                    PhoneNumber = model.PhoneNumber,
                    EmailAddress = model.EmailAddress,
                    Day = model.Day,
                    Location = model.Location,
                    CreatedUtc = DateTimeOffset.Now,
                };
            using (var ctx = new ApplicationDbContext())
            {
                ctx.Volunteers.Add(entity);
                return ctx.SaveChanges() == 1;
            }
        }
        //-----------------------------------------------------------------------------------------------
        public IEnumerable<VolunteerListItem> GetVolunteers()
        {
            using (var ctx = new ApplicationDbContext())
            {
                var query =
                    ctx
                        .Volunteers
                        .Where(e => e.UserId == _userId)
                        .Select(
                            e =>
                                new VolunteerListItem
                                {
                                    VolunteerId = e.VolunteerId,
                                    UserId = e.UserId,
                                    LoginId = e.LoginId,
                                    FirstName = e.FirstName,
                                    LastName = e.LastName,
                                    PhoneNumber = e.PhoneNumber,
                                    EmailAddress = e.EmailAddress,
                                    Day = e.Day,
                                    Location = e.Location,
                                    CreatedUtc = e.CreatedUtc,
                                }
                        );
                return query.ToArray();
            }
        }
        //-----------------------------------------------------------------------------------------------
        public VolunteerDetail GetVolunteerById(int id)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity =
                    ctx
                        .Volunteers
                        .Where(e => e.UserId == _userId)
                        .Single(e => e.VolunteerId == id);
                return
                        new VolunteerDetail
                        {
                            VolunteerId = entity.VolunteerId,
                            UserId = _userId,
                            LoginId = entity.LoginId,
                            FirstName = entity.FirstName,
                            LastName = entity.LastName,
                            PhoneNumber = entity.PhoneNumber,
                            EmailAddress = entity.EmailAddress,
                            Day = entity.Day,
                            Location = entity.Location,
                            CreatedUtc = entity.CreatedUtc,
                        };
            }
        }
        //-----------------------------------------------------------------------------------------------
        public VolunteerDetail GetVolunteerByIdForEdit(int VolunteerId)
        {
            using (var ctx = new ApplicationDbContext())
            {

                var entity =
                      ctx
                         .Volunteers
                         .Single(e => e.VolunteerId == VolunteerId && e.UserId == _userId);
                return
                                 new VolunteerDetail
                                 {
                                     VolunteerId = entity.VolunteerId,
                                     UserId = entity.UserId,
                                     LoginId = entity.LoginId,
                                     FirstName = entity.FirstName,
                                     LastName = entity.LastName,
                                     PhoneNumber = entity.PhoneNumber,
                                     EmailAddress = entity.EmailAddress,
                                     Day = entity.Day,
                                     Location = entity.Location,
                                     CreatedUtc = entity.CreatedUtc,
                                 };
            }
        }
        //-----------------------------------------------------------------------------------------------
        public bool UpdateVolunteer(VolunteerEdit model)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity =
                    ctx
                    .Volunteers
                    .Single(e => e.VolunteerId == model.VolunteerId);

                entity.VolunteerId = entity.VolunteerId;
                entity.UserId = model.UserId;
                entity.LoginId = model.LoginId;
                entity.FirstName = model.FirstName;
                entity.LastName = model.LastName;
                entity.PhoneNumber = model.PhoneNumber;
                entity.EmailAddress = model.EmailAddress;
                entity.Day = model.Day;
                entity.Location = model.Location;
                entity.CreatedUtc = model.CreatedUtc;

                return ctx.SaveChanges() == 1;
            }
        }

        //-----------------------------------------------------------------------------------------------


        public bool DeleteVolunteer(int VolunteerId)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity =
                    ctx
                        .Volunteers
                        .Single(e => e.VolunteerId == VolunteerId && e.UserId == _userId);
                ctx.Volunteers.Remove(entity);

                return ctx.SaveChanges() == 1;
            }
        }
    }
}

        

