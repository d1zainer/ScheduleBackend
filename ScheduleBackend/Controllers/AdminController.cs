using Microsoft.AspNetCore.Mvc;
using ScheduleBackend.Models.Dto;
using ScheduleBackend.Services;
using ScheduleBackend.Models.Entity;

namespace ScheduleBackend.Controllers
{
    /// <summary>
    /// Контроллер для управления администраторами.
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class AdminController(AdminService adminService) : ControllerBase
    {

        [HttpPost]
        public async Task<IActionResult> AddAdmin([FromBody] AdminDto dto)
        {
            var (success, ex) = await adminService.Add(dto);
            if (!success)
                return BadRequest(ex?.Message);

            return Ok(success);
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Admin>>> GetAllAdmins()
        {
            var admins = await adminService.GetAll();
            return Ok(admins);
        }

        [HttpGet("{id:guid}")]
        public async Task<ActionResult<Admin>> GetAdminById(Guid id)
        {
            var admin = await adminService.GetById(id);
            if (admin is null)
                return NotFound("Admin not found");

            return Ok(admin);
        }

        [HttpPut("{id:guid}")]
        public async Task<IActionResult> UpdateAdmin(Guid id, [FromBody] AdminDto dto)
        {
            var (success, ex, updated) = await adminService.Update(id, dto);
            if (!success)
                return BadRequest(ex?.Message);

            return Ok(updated);
        }

        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> DeleteAdmin(Guid id)
        {
            var (success, ex) = await adminService.Delete(id);
            if (!success)
                return BadRequest(ex?.Message);

            return Ok("Admin deleted");
        }
    }
}