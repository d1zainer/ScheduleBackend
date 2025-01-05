using Microsoft.AspNetCore.Mvc;
using ScheduleBackend.Models;

using ScheduleBackend.Services;


[Route("[controller]")]
[ApiController]
public class UsersController : ControllerBase
{
    private readonly UserService _userService;
    private readonly ScheduleService _scheduleService;

    /// <summary>
    /// Конструктор UsersController.
    /// </summary>
    /// <param name="userService">Сервис для работы с пользователями.</param>
    public UsersController(UserService userService, ScheduleService schedule)
    {
        _userService = userService;
        _scheduleService = schedule;
    }

    /// <summary>
    /// Авторизация пользователя.
    /// </summary>
    /// <param name="loginRequest">Данные для входа.</param>
    /// <returns>Информация о пользователе.</returns>
    [HttpPost("login")]
    [ProducesResponseType(typeof(UserLoginResponse), 200)]
    public IActionResult Login([FromBody] UserLoginRequest loginRequest)
    {
        var result = _userService.Authenticate(loginRequest.Username, loginRequest.Password);
        if (result.Item2)
        {
            return Ok(new UserLoginResponse(result.Item1.Id));
        }
        return BadRequest("Пользователя нет.");
    }

    /// <summary>
    /// Добавить нового пользователя.
    /// </summary>
    /// <param name="userRequest">Данные пользователя.</param>
    /// <returns>Результат операции.</returns>
    [HttpPost("add")]
    [ProducesResponseType(typeof(bool), 200)]
    public IActionResult Add([FromBody] User userRequest)
    {
        var result = _userService.Add(userRequest);
        if (result)
        {
            _scheduleService.Add(userRequest.Id);
            return Ok(result);
        }
        return BadRequest(result);
    }

    /// <summary>
    /// Получить список всех пользователей.
    /// </summary>
    /// <returns>Список пользователей.</returns>
    [HttpGet("all")]
    [ProducesResponseType(typeof(List<User>), 200)]
    public IActionResult GetAll()
    {
        return Ok(_userService.GetUsers());
    }

    /// <summary>
    /// Удалить пользователя по ID.
    /// </summary>
    /// <param name="id">ID пользователя.</param>
    /// <returns>Результат операции.</returns>
    [HttpDelete("delete")]
    [ProducesResponseType(typeof(bool), 200)]
    public IActionResult Delete([FromBody] int id)
    {
        var result = _userService.Delete(id);
        if (result)
        {
            return Ok(result);
        }
        return BadRequest(result);
    }
}
