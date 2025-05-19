using ScheduleBackend.Models.Entity;
using Swashbuckle.AspNetCore.Filters;

namespace ScheduleBackend.Models.Dto
{
    /// <summary>
    /// Запрос для обновления информации о занятии в расписании.
    /// </summary>
    public class ScheduleUpdateRequest
    {
        /// <summary>
        /// ID расписания, которое нужно обновить.
        /// </summary>
        public Guid ScheduleId { get; set; }

        /// <summary>
        /// Номер дня в расписании, в котором нужно обновить занятие.
        /// </summary>
        public int DayNumber { get; set; }

        /// <summary>
        /// Номер занятия, которое нужно обновить.
        /// </summary>
        public int ActivityNumber { get; set; }

        /// <summary>
        /// Новое название курса для занятия, или 0, если запись отменена.
        /// </summary>
        public string? NewLessonName { get; set; }

        /// <summary>
        /// Статус бронирования занятия.
        /// </summary>
        public bool IsBooked { get; set; }
    }

    public class CourseCheckRequest
    {
        /// <summary>
        /// Название курса или занятия. Может быть пустым, если не задано.
        /// </summary>
        public string LessonName { get; set; }

        /// <summary>
        /// Список занятий в курсе
        /// </summary>
        public List<ActivityCheck> Activities { get; set; }
    }

    public class CourseCheckResponse
    {
        /// <summary>
        /// Результат
        /// </summary>
        public bool Result { get; set; }

        /// <summary>
        /// Список занятий в курсе
        /// </summary>
        public List<ActivityCheck>? BookedActivities { get; set; }
    }


    public class CourseCheckOkResponseExample : IExamplesProvider<CourseCheckResponse>
    {
        public CourseCheckResponse GetExamples()
        {
            return new CourseCheckResponse
            {
                Result = true,
                BookedActivities = null
            };
        }
    }




    public class CourseCheckErrorResponseExample : IExamplesProvider<CourseCheckResponse>
    {
        public CourseCheckResponse GetExamples()
        {
            return new CourseCheckResponse
            {
                Result = true,
                BookedActivities = new List<ActivityCheck>
                {
                    new ActivityCheck
                    {
                        ScheduleId = Guid.NewGuid(),
                        DayNumber = 2,
                        ActivityNumber = 3,
                        LessonName = "Урок Абобы"
                    }
                }

            };
        }
    }
}
