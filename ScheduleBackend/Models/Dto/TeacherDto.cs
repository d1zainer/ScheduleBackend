using System.ComponentModel.DataAnnotations;
using ScheduleBackend.Models.Entity;

namespace ScheduleBackend.Models.Dto
{


    public class TeacherCreateRequest
    {
        [Required]
        public required int GroupId { get; set; }

        [Required]
        [MaxLength(50)]
        public required string Login { get; set; }

        [Required]
        [MaxLength(50)]
        public required string Password { get; set; }

        [Required]
        [MaxLength(100)]
        public required string LastName { get; set; }

        [Required]
        [MaxLength(100)]
        public required string FirstName { get; set; }

        [Required]
        [MaxLength(100)]
        public required string MiddleName { get; set; }

        [Required]
        [EmailAddress]
        [MaxLength(100)]
        public required string Email { get; set; }

        [Phone]
        [MaxLength(100)]
        public string? PhoneNumber { get; set; }

    }



    public class TeacherUpdateRequest
        {
            /// <summary>
            /// Уникальный идентификатор учителя.
            /// </summary>
            public Guid Id { get; set; }

            /// <summary>
            /// Если 0 - прибавляем 1, если - 1 - отнимаем (инкримент и декримент)
            /// </summary>
            public int Action { get; set; }
        }

        public class TeacherUpdateResponse
        {
            /// <summary>
            /// Учитель
            /// </summary>
            public Teacher Teacher { get; set; }

            /// <summary>
            /// сообщение
            /// </summary>
            public string Message { get; set; }

            /// <summary>
            /// Результат
            /// </summary>
            public bool Result { get; set; }

            public TeacherUpdateResponse(Teacher teacher, string message, bool result)
            {
                Teacher = teacher;
                Message = message;
                Result = result;
            }
        }
    
}
