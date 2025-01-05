using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ScheduleBackend.Models;
using ScheduleBackend.Services;

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

        public TeachersController(TeachersService teachersService)
        {
            _teachersService = teachersService;
        }

        /// <summary>
        /// Добавить нового учителя
        /// </summary>
        /// <param name="teacher">Информация о новом учителе</param>
        /// <returns>Результат добавления учителя</returns>
        [HttpPost("add")]
        [ProducesResponseType(typeof(bool), 200)]
        public IActionResult Add([FromBody] Teacher teacher)
        {
            var result = _teachersService.Add(teacher);
            if (result)
            {
                return Ok(result);
            }
            else
            {
                return BadRequest(result);
            }
        }

        /// <summary>
        /// Получить список всех учителей
        /// </summary>
        /// <returns>Список всех учителей</returns>
        [HttpGet("all")]
        [ProducesResponseType(typeof(List<Teacher>), 200)]
        public IActionResult GetAll()
        {
            return Ok(_teachersService.GetUsers());
        }

        /// <summary>
        /// Удалить учителя по его ID
        /// </summary>
        /// <param name="id">Идентификатор учителя</param>
        /// <returns>Результат удаления учителя</returns>
        [HttpDelete("delete")]
        [ProducesResponseType(typeof(bool), 200)]
        public IActionResult Delete([FromBody] int id)
        {
            var result = _teachersService.Delete(id);
            if (result)
            {
                return Ok(result);
            }
            else
            {
                return BadRequest(result);
            }
        }

        /// <summary>
        /// Получить доступные слоты для конкретного учителя по его ID
        /// </summary>
        /// <param name="teacherId">Идентификатор учителя</param>
        /// <returns>Список доступных слотов</returns>
        [HttpGet("GetActiveSlots/{teacherId}")]
        [ProducesResponseType(typeof(int), 200)]
        public IActionResult GetActiveSlots(int teacherId)
        {
            var activeSlots = _teachersService.GetActiveSlots(teacherId);
            if (activeSlots == null) 
            {
                return NotFound("Нет доступных слотов для выбранного учителя.");
            }
            return Ok(activeSlots);
        }



        /// <summary>
        /// Обовить доступные слоты для конкретного учителя
        /// </summary>
        /// <param name="teacherId">Идентификатор учителя</param>
        /// <returns>Список доступных слотов</returns>
        [HttpPost("SetActiveSlots")]
        [ProducesResponseType(typeof(int), 200)]
        public IActionResult UpdateActiveSlots(TeacherUpdateRequest teacherUpdate)
        {
            var teacher = _teachersService.UpdateActiveSlots(teacherUpdate);
            if (teacher.Result == true)
                return Ok(teacher);
            else
                return BadRequest(teacher);

        }
    }
}
