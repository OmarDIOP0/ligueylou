using Ligueylou.Server.Request;
using Ligueylou.Server.Services.Utilisateurs;
using Microsoft.AspNetCore.Mvc;


namespace Ligueylou.Server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UtilisateursController : ControllerBase
    {
        private readonly IUtilisateurService _service;

        public UtilisateursController(IUtilisateurService service)
        {
            _service = service;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] CreateUtilisateurDto request)
        {
            try
            {
                var response = await _service.Register(request);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequestDto request)
        {
            try
            {
                var response = await _service.Login(request);
                return Ok(response);
            }
            catch (UnauthorizedAccessException ex)
            {
                return Unauthorized(new { message = ex.Message });
            }
            catch (Exception ex)
            {

                return BadRequest(new { message = ex.Message });
            }
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateUtilisateur(Guid id, [FromBody] CreateUtilisateurDto dto)
        {
            try
            {
                var updatedUser = await _service.UpdateUtilisateur(id, dto);
                return Ok(updatedUser);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUtilisateur(Guid id)
        {
            try
            {
                await _service.DeleteUtilisateur(id);
                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("{id:guid}")]
        public async Task<IActionResult> Get(Guid id)
        {
            var user = await _service.GetUtilisateurById(id);
            return user == null ? NotFound() : Ok(user);
        }

        [HttpGet("email/{email}")]
        public async Task<IActionResult> GetByEmail(string email)
        {
            var user = await _service.GetUtilisateurByEmail(email);
            return user == null ? NotFound() : Ok(user);
        }
        [HttpGet("telephone/{telephone}")]
        public async Task<IActionResult> GetByTelephone(string telephone)
        {
            var user = await _service.GetUtilisateurByTelephone(telephone);
            return user == null ? NotFound() : Ok(user);
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await _service.GetAllUtilisateurs());
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateUtilisateurDto dto)
        {
            var created = await _service.AddUtilisateur(dto);
            return CreatedAtAction(nameof(Get), new { id = created.Id }, created);
        }
    }

}
