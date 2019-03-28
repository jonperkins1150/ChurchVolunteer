using ChurchVolunteer.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChurchVolunteer.Model.Volunteer
{
    public class VolunteerDelete
    {
      
        public int VolunteerId { get; set; }
        
        public Guid UserId { get; set; }
        [Display(Name = "UserName")]
        public string LoginId { get; set; }   
        [Display(Name = "First Name")]
        public string FirstName { get; set; }      
        [Display(Name = "Last Name")]
        public string LastName { get; set; }   
        [Display(Name = "Phone Number")]
        public string PhoneNumber { get; set; }    
        [Display(Name = "Email Address")]
        public string EmailAddress { get; set; }     
        [Display(Name = "Service Preference")]
        public ServiceDay Day { get; set; }     
        [Display(Name = "Position  Preference")]
        public Placement Location { get; set; }
        [Required]
        public DateTimeOffset CreatedUtc { get; set; }
    }
}
