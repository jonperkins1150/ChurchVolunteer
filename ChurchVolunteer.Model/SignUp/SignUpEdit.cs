﻿using ChurchVolunteer.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChurchVolunteer.Model.SignUp
{
    public class SignUpEdit
    {
        public int SignUpId { get; set; }
        public Guid UserId { get; set; }
        public int VolunteerId { get; set; }
        public int EventId { get; set; }

        public string LoginId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        [DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}")]
        [Display(Name = "Service Date")]
        public DateTime ServiceDate { get; set; }
        [Display(Name = "Service Day")]
        public ServiceDay Day { get; set; }
        [Display(Name = "Position")]
        public Placement Location { get; set; }
        [Display(Name = "Number of Volunteers Needed")]
        public int RequiredVolunteers { get; set; }
        [Display(Name = "Remaining Open Need")]
        public int RemainingNeed { get; set; }

        public DateTimeOffset CreatedUtc { get; set; }
    }
}
