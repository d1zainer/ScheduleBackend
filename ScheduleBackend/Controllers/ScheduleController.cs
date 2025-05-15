using Microsoft.AspNetCore.Mvc;
using ScheduleBackend.Models;
using ScheduleBackend.Services.Entity;
using Swashbuckle.AspNetCore.Annotations;
using Swashbuckle.AspNetCore.Filters;
using System.Net;

namespace ScheduleBackend.Controllers
{
    /// <summary>
    /// Контроллер для управления расписанием пользователей
    /// </summary>
    [Route("[controller]")]
    [ApiController]
    public class ScheduleController : ControllerBase
    {
        private readonly ScheduleService _scheduleService;

        public ScheduleController(ScheduleService scheduleService)
        {
            _scheduleService = scheduleService;
        }

        /// <summary>
        /// Получить расписание пользователя по его ID
        /// </summary>
        /// <param name="userId">Идентификатор пользователя</param>
        /// <returns>Расписание пользователя, если оно найдено</returns>
        [HttpGet("{userId}")]
        [ProducesResponseType(typeof(Schedule), 200)]
        public IActionResult GetSchedule(int userId)
        {
            var userSchedule = _scheduleService.GetScheduleByUserId(userId);
            if (userSchedule == null)
            {
                return NotFound("Расписание не найдено.");
            }
            return Ok(userSchedule);
        }

        /// <summary>
        /// Обновить расписание пользователя
        /// </summary>
        /// <param name="updateRequest">Запрос на обновление расписания</param>
        /// <returns>Обновленное расписание</returns>
        [HttpPost("update")]
        [ProducesResponseType(typeof(Schedule), 200)]
        public IActionResult UpdateSchedule([FromBody] ScheduleUpdateRequest updateRequest)
        {
            var updatedSchedule = _scheduleService.UpdateSchedule(updateRequest);
            if (updatedSchedule == null)
            {
                return BadRequest("Не удалось обновить расписание.");
            }
            return Ok(updatedSchedule);
        }

        /// <summary>
        /// Обновить расписание (несколько занятий)
        /// </summary>
        /// <param name="updateRequest">Список запросов на обновление расписания</param>
        /// <returns>Список обновленных расписаний</returns>
        [HttpPost("updateList")]
        [ProducesResponseType(typeof(List<Schedule>), 200)]
        public async Task<IActionResult> UpdateSchedule([FromBody] List<ScheduleUpdateRequest> updateRequest)
        {
            List<Schedule> res = new List<Schedule>();
            foreach (var req in updateRequest)
            {
                var updatedSchedule = await _scheduleService.UpdateSchedule(req);
                if (updatedSchedule == null) return BadRequest("Не удалось обновить расписание.");
                res.Add(updatedSchedule);
            }
            return Ok(res);
        }

        /// <summary>
        /// Проверить доступность списка активностей
        /// </summary>
        /// <param name="updateRequest">Список запросов для проверки доступности активностей</param>
        /// <returns>Информация о доступности активностей и о забронированных активностях</returns>
        [HttpPost("checkListActivities")]
        [ProducesResponseType(typeof(object), 200)]
        public async Task<IActionResult> CheckActivities([FromBody] List<ActivityCheck> updateRequest)
        {
            var (isSuccess, bookedActivities) = await _scheduleService.CheckActivities(updateRequest);

            if (isSuccess)
            {
                return Ok(new
                {
                    success = true,
                    message = "Все активности доступны.",
                    bookedActivities = (List<ActivityCheck>?)null
                });
            }

            return BadRequest(new
            {
                success = false,
                message = "Некоторые активности уже забронированы.",
                bookedActivities
            });
        }

        /// <summary>
        /// Проверить доступность конкретной активности
        /// </summary>
        /// <param name="updateRequest">Запрос на проверку доступности активности</param>
        /// <returns>Результат проверки доступности</returns>
        [HttpPost("checkActivity")]
        [ProducesResponseType(typeof(bool), 200)]
        public async Task<IActionResult> CheckActivities(ActivityCheck updateRequest)
        {
            var result = await _scheduleService.CheckActivity(updateRequest);
            if (result) return Ok(result);
            return BadRequest(result);
        }

        [HttpPost("checkCourse")]
        [ProducesResponseType(typeof(object), 200)]
        [ProducesResponseType(typeof(object), 400)]
        [SwaggerOperation(Summary = "Проверяет, записался ли пользователь на конкретный курс",
            Description = "Возвращает информацию о доступности активностей и забронированных активностях.")]
        [SwaggerResponseExample((int)HttpStatusCode.OK, typeof(CourseCheckOkResponseExample))] // Пример для ответа 200
        [SwaggerResponseExample((int)HttpStatusCode.BadRequest, typeof(CourseCheckErrorResponseExample))] // Пример для ответа 200
        
        public async Task<IActionResult> CheckCourse([FromBody] CourseCheckRequest updateRequest)
        {
            var result =  await _scheduleService.CheckCourse(updateRequest);
            if (result.Result) return Ok(result);
            return BadRequest(result);
        }
    }
}