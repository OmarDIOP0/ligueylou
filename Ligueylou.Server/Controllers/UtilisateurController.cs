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
