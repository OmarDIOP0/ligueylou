using Ligueylou.Server.Request;
using Ligueylou.Server.Services.Clients;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Ligueylou.Server.Models;

namespace Ligueylou.Server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ClientsController : ControllerBase
    {
        private readonly IClientService _service;

        public ClientsController(IClientService service)
        {
            _service = service;
        }

        [Authorize]
        [HttpGet]
        public async Task<IActionResult> GetAll()
            => Ok(await _service.GetAllClients());

        [Authorize]
        [HttpGet("{id:guid}")]
        public async Task<IActionResult> Get(Guid id)
        {
            var client = await _service.GetClientById(id);
            return client == null ? NotFound() : Ok(client);
        }

        [Authorize]
        [HttpGet("email/{email}")]
        public async Task<IActionResult> GetByEmail(string email)
        {
            var client = await _service.GetClientByEmail(email);
            return client == null ? NotFound() : Ok(client);
        }

        [Authorize]
        [HttpGet("telephone/{telephone}")]
        public async Task<IActionResult> GetByTelephone(string telephone)
        {
            var client = await _service.GetClientByTelephone(telephone);
            return client == null ? NotFound() : Ok(client);
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateUtilisateurDto dto)
        {
            var created = await _service.AddClient(dto);
            return CreatedAtAction(nameof(Get), new { id = created.Id }, created);
        }

        [Authorize]
        [HttpPut("{id:guid}")]
        public async Task<IActionResult> Update(Guid id, CreateUtilisateurDto dto)
        {
            var updated = await _service.UpdateClient(id, dto);
            return updated == null ? NotFound() : Ok(updated);
        }

        [Authorize]
        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var ok = await _service.DeleteClient(id);
            return ok ? NoContent() : NotFound();
        }

        //--------------------------
        // ADRESSE
        //--------------------------
        [Authorize]
        [HttpPut("{id:guid}/adresse")]
        public async Task<IActionResult> UpdateAdresse(Guid id, Adresse adresse)
        {
            var updated = await _service.UpdateAdresse(id, adresse);
            return updated == null ? NotFound() : Ok(updated);
        }

        //--------------------------
        // FAVORIS
        //--------------------------
        [Authorize]
        [HttpPost("{clientId:guid}/favoris")]
        public async Task<IActionResult> AddFavori(Guid clientId, Favori favori)
        {
            var updated = await _service.AddFavori(clientId, favori);
            return updated == null ? NotFound() : Ok(updated);
        }

        [Authorize]
        [HttpDelete("{clientId:guid}/favoris/{favoriId:guid}")]
        public async Task<IActionResult> RemoveFavori(Guid clientId, Guid favoriId)
        {
            var ok = await _service.RemoveFavori(clientId, favoriId);
            return ok ? NoContent() : NotFound();
        }

        [Authorize]
        [HttpGet("{clientId:guid}/favoris")]
        public async Task<IActionResult> GetFavoris(Guid clientId)
            => Ok(await _service.GetFavoris(clientId));

        //--------------------------
        // RÉSERVATIONS
        //--------------------------
        [Authorize]
        [HttpGet("{clientId:guid}/reservations")]
        public async Task<IActionResult> GetReservations(Guid clientId)
            => Ok(await _service.GetReservations(clientId));

        //--------------------------
        // ÉVALUATIONS
        //--------------------------
        [Authorize]
        [HttpGet("{clientId:guid}/evaluations")]
        public async Task<IActionResult> GetEvaluations(Guid clientId)
            => Ok(await _service.GetEvaluations(clientId));

        //--------------------------
        // FILTRAGE
        //--------------------------
        [Authorize]
        [HttpGet("filter")]
        public async Task<IActionResult> Filter([FromQuery] bool? actif, [FromQuery] string? email, [FromQuery] string? telephone)
        {
            return Ok(await _service.FilterClients(actif, email, telephone));
        }
    }
}
