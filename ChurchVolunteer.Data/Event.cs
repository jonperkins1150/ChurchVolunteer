using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChurchVolunteer.Data
{
    public enum ServiceDay { Thursday = 1, [Display(Name = "Sunday First Service")] FirstSunday, [Display(Name = "Sunday Second Service")] SecondSunday, }

    public enum Placement { Doors = 1, Parking, Weeklies, Coffee, NoPreference }

    public class Event
    {
        [Key]
        public int EventId { get; set; }
        [Required]
        public Guid UserId { get; set; }




        [Required]
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
