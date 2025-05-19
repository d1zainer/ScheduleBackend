using System.ComponentModel.DataAnnotations;

namespace ScheduleBackend.Models.Dto
{
    
    public class StudentCreateResponse
    {
        [Required]
        public required string Login { get; set; }

        [Required]
        public required string Password { get; set; }

        [Required]
        public required string LastName { get; set; }

        [Required]
        public required string FirstName { get; set; }
        [Required]
        public required string MiddleName { get; set; }

        [Required]
        public required  DateTime DateOfBirth { get; set; }
   
        public string? Email { get; set; }
     
        public string? PhoneNumber { get; set; }
    }

}
