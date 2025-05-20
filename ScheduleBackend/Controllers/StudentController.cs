using Microsoft.AspNetCore.Mvc;
using ScheduleBackend.Models.Dto;
using ScheduleBackend.Models.Entity;
using ScheduleBackend.Services.Entity;

[Route("[controller]")]
[ApiController]
public class StudentController : ControllerBase
{
    private readonly StudentService _userService;
    private readonly ScheduleService _scheduleService;

    /// <summary>
    /// Конструктор UsersController.
    /// </summary>
    /// <param name="userService">Сервис для работы с пользователями.</param>
    public StudentController(StudentService userService, ScheduleService schedule)
    {
        _userService = userService;
        _scheduleService = schedule;
    }



    /// <summary>
    /// Добавить нового пользователя.
    /// </summary>
    /// <param name="userRequest">Данные пользователя.</param>
    /// <returns>Результат операции.</returns>
    [HttpPost("add")]
    [ProducesResponseType(typeof(bool), 200)]
    public async Task<IActionResult> Add([FromBody] StudentCreateResponse userRequest)
    {
        var result = await _userService.Add(userRequest);
        if (result.success)
        {
           //_scheduleService.Add(userRequest.Id); 
            return Ok(result);
        }
        return BadRequest(result);
    }

    /// <summary>
    /// Получить список всех пользователей.
    /// </summary>
    /// <returns>Список пользователей.</returns>
    [HttpGet("all")]
    [ProducesResponseType(typeof(List<Student>), 200)]
    public async Task<IActionResult> GetAll()
    {
        return Ok( await _userService.GetUsers());
    }





    /// <summary>
    /// Получить список всех пользователей.
    /// </summary>
    /// <returns>Список пользователей.</returns>
    [HttpGet("get/{guid:guid}")]
    public async Task<IActionResult> GetInfo([FromRoute] Guid guid)
    {
        var result = await _userService.GetUserByGuid(guid);
        if (result.success)
            return Ok(result.response);
        return NotFound(result.ex);
    }

    /// <summary>
    /// Удалить пользователя по ID.
    /// </summary>
    /// <param name="id">ID пользователя.</param>
    /// <returns>Результат операции.</returns>
    [HttpDelete("delete")]
    [ProducesResponseType(typeof(bool), 200)]
    public async Task<IActionResult> Delete([FromBody] Guid id)
    {
        var result = await _userService.Delete(id);
        if (result.success) return Ok(result);
        return BadRequest(result);
    }
}