using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChurchVolunteer.Data
{
    public class Volunteer
    {
        [Key]
        public int VolunteerId { get; set; }
        
        public Guid UserId { get; set; }
        [Display(Name = "UserName")]
        public string LoginId { get; set; }
        [Required]
        [Display(Name = "First Name")]
        public string FirstName { get; set; }
        [Required]
        [Display(Name = "Last Name")]
        public string LastName { get; set; }
        [Required]
        [Display(Name = "Phone Number")]
        public string PhoneNumber { get; set; }
        [Required]
        [Display(Name = "Email Address")]
        public string EmailAddress { get; set; }
        [Required]
        [Display(Name = "Service Preference")]
        public ServiceDay Day { get; set; }
        [Required]
        [Display(Name = "Position  Preference")]
        public Placement Location { get; set; }
        public DateTimeOffset CreatedUtc { get; set; }
   

    }
}
