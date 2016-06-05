using System;
using System.ComponentModel.DataAnnotations;

namespace AttendeeList
{
    public class Attendee
    {
        public int Id { get; set; }
        
        [Required]
        public string FirstName { get; set; }
        
        [Required]
        public string LastName { get; set; }
                
        public string Email { get; set; }
        
        [Required]
        public string Company { get; set; }     
    }
}
