using Microsoft.AspNetCore.Mvc;
using ScheduleBackend.Models;
using ScheduleBackend.Services.Entity;

namespace ScheduleBackend.Controllers
{
    /// <summary>
    /// Контроллер для управления учителями
    /// </summary>
    [Route("[controller]")]
    [ApiController]
    public class TeachersController : ControllerBase
    {
        private readonly TeachersService _teachersService;
        private readonly TeacherScheduleService _lessonsService;

        public TeachersController(TeachersService teachersService, TeacherScheduleService lessonsService)
        {
            _teachersService = teachersService;
            _lessonsService = lessonsService;
        }

        /// <summary>
        /// Добавить нового учителя
        /// </summary>
        /// <param name="teacher">Информация о новом учителе</param>
        /// <returns>Результат добавления учителя</returns>
        [HttpPost("add")]
        [ProducesResponseType(typeof(bool), 200)]
        public async Task<IActionResult> Add([FromBody] Teacher teacher)
        {
            var result = await _teachersService.Add(teacher);
            if (result.Success) return Ok(result);
            return BadRequest(result.Ex);
        }

        /// <summary>
        /// Получить список всех учителей
        /// </summary>
        /// <returns>Список всех учителей</returns>
        [HttpGet("all")]
        [ProducesResponseType(typeof(List<Teacher>), 200)]
        public async Task<IActionResult> GetAll()
        {
            var teachers = await _teachersService.GetAll();
            if (teachers.Any()) return Ok(teachers);
            return BadRequest(teachers);
        }

        /// <summary>
        /// Удалить учителя по его ID
        /// </summary>
        /// <param name="id">Идентификатор учителя</param>
        /// <returns>Результат удаления учителя</returns>
        [HttpDelete("delete")]
        [ProducesResponseType(typeof(bool), 200)]
        public async Task<IActionResult> Delete([FromBody] int id)
        {
            var result = await _teachersService.Delete(id);
            if (result.Success) return Ok(result); 
            return BadRequest(result.Ex);
        }

        /// <summary>
        /// Получить доступные слоты для конкретного учителя по его ID
        /// </summary>
        /// <param name="teacherId">Идентификатор учителя</param>
        /// <returns>Список доступных слотов</returns>
        [HttpGet("GetActiveSlots/{teacherId}")]
        [ProducesResponseType(typeof(int), 200)]
        public async Task<IActionResult> GetActiveSlots(int teacherId)
        {
            var activeSlots  = await _teachersService.GetActiveSlots(teacherId);
            if(activeSlots != null)
                return Ok(activeSlots); 
            if(activeSlots == 0)
                return NotFound("Нет доступных слотов для выбранного учителя.");
            return NotFound("Нет выбранного учителя.");
        }

        /// <summary>
        /// Обовить доступные слоты для конкретного учителя
        /// </summary>
        /// <param name="teacherId">Идентификатор учителя</param>
        /// <returns>Список доступных слотов</returns>
        [HttpPost("SetActiveSlots")]
        [ProducesResponseType(typeof(int), 200)]
        public async Task<IActionResult> UpdateActiveSlots(TeacherUpdateRequest teacherUpdate)
        {
            var teacherUpdateSlots = await _teachersService.UpdateActiveSlots(teacherUpdate);
            if (teacherUpdateSlots.Result) return Ok(teacherUpdateSlots);
            return BadRequest(teacherUpdateSlots);
        }

        /// <summary>
        /// Получить список учителей для курса
        /// </summary>
        /// <param name="teacherId">Идентификатор учителя</param>
        /// <returns>Список доступных слотов</returns>
        [HttpGet("GetTeachersByGroupID/{groupId}")]
        [ProducesResponseType(typeof(int), 200)]
        public async Task<IActionResult> GetTeachersByGroupID(int groupId)
        {
            var list = await _teachersService.GetTeachersByGroupId(groupId);
            if (!list.Any()) return BadRequest("В группе нет учителей");
            return Ok(list);
        }

        /// <summary>
        /// Получить список уроков для преподавателя (по айди)
        /// </summary>
        /// <param name="teacherId">Идентификатор учителя</param>
        /// <returns>Список доступных слотов</returns>
        [HttpGet("GetAllTeachersLessons")]
        [ProducesResponseType(typeof(int), 200)]
        public async Task<IActionResult> GetAllTeachersLessons() =>  Ok(await _lessonsService.GetTeachersSchedules());
 

        /// <summary>
        /// Получить список уроков для преподавателя (по айди)
        /// </summary>
        /// <param name="teacherId">Идентификатор учителя</param>
        /// <returns>Список доступных слотов</returns>
        [HttpGet("GetTeachersLessons/{teacherId}")]
        [ProducesResponseType(typeof(int), 200)]
        public async Task<IActionResult> GetTeachersLessons(int teacherId)
        {
            var list = await _lessonsService.GetLessons(teacherId);
            if (!list.Any()) return BadRequest("Занятия не найдены");
            return Ok(list);
        }
    }
}