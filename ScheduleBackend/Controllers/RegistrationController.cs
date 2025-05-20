using Microsoft.AspNetCore.Mvc;
using ScheduleBackend.Models.Dto;
using ScheduleBackend.Models.Entity;
using ScheduleBackend.Services.Entity;

namespace ScheduleBackend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RegistrationController(RegistrationService registrationService) : ControllerBase
    {

        /// <summary>
        /// Добавить новую заявку.
        /// </summary>
        /// <param name="userRequest">Данные пользователя.</param>
        /// <returns>Результат операции.</returns>
        [HttpPost("create")]
        [ProducesResponseType(typeof(bool), 200)]
        public async Task<IActionResult> Add([FromBody] RegistrationCreateRequest request)
        {
            var result = await registrationService.CreateNewRegistration(request);
            if (result.succes) return Ok(result.succes);
            return BadRequest(result);
        }

        /// <summary>
        /// Обновить статус заявки.
        /// </summary>
        /// <param name="userRequest">Данные пользователя.</param>
        /// <returns>Результат операции.</returns>
        [HttpPut("updateStatus")]
        [ProducesResponseType(typeof(bool), 200)]
        public async Task<IActionResult> UpdateStatus([FromBody] RegistrationStatusUpdateRequest request)
        {
            var result = await registrationService.UpdateRegistrationStatus(request);
            if (result.succes) return Ok(result.succes);
            return BadRequest(result);
        }




        /// <summary>
        /// Получить список заявок с необязательной фильтрацией по статусу.
        /// </summary>
        /// <param name="query">Параметры сортировки и фильтрации.</param>
        /// <returns>Список регистраций.</returns>
        [HttpGet("get")]
        [ProducesResponseType(typeof(IEnumerable<Registration>), 200)]
        public async Task<IActionResult> GetAll([FromQuery] RegistrationSortRequest query)
        {
            var (success, data) = await registrationService.GetAll(query.SortByStatus);
            if (!success || data == null) return StatusCode(500, "Ошибка при получении регистраций.");
            return Ok(data);
        }
    }
}
