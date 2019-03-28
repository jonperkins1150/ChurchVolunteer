using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChurchVolunteer.Data
{
    public class SignUp
    {
        [Key]
        public int SignUpId { get; set; }
        [Required]
        public Guid UserId { get; set; }
        [Required]
        public int VolunteerId { get; set; }
        [Required]
        public int EventId { get; set; }
        [Required]
        public DateTimeOffset CreatedUtc { get; set; }
        public DateTimeOffset? ModifiedUtc { get; set; }
        public virtual Event Event { get; set; }
        public virtual Volunteer Volunteer { get; set; }
    }

}

