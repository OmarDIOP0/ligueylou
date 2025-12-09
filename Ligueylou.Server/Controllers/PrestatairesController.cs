using Ligueylou.Server.Models;
using Ligueylou.Server.Request;
using Ligueylou.Server.Services.Prestataires;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Ligueylou.Server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PrestatairesController : ControllerBase
    {
        private readonly IPrestataireService _service;

        public PrestatairesController(IPrestataireService service)
        {
            _service = service;
        }

        // --------------------------------------------------------------------
        // CREATE
        // --------------------------------------------------------------------
        [HttpPost]
        public async Task<IActionResult> CreatePrestataire([FromBody] Prestataire request)
        {
            try
            {
                var response = await _service.AddPrestataire(request);
                return CreatedAtAction(nameof(GetPrestataireById), new { id = response.Id }, response);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        // --------------------------------------------------------------------
        // GET BY ID
        // --------------------------------------------------------------------
        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetPrestataireById(Guid id)
        {
            var p = await _service.GetPrestataireById(id);
            return p == null ? NotFound() : Ok(p);
        }

        // --------------------------------------------------------------------
        // GET ALL
        // --------------------------------------------------------------------
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await _service.GetAllPrestataires());
        }

        // --------------------------------------------------------------------
        // UPDATE INFORMATIONS
        // --------------------------------------------------------------------
        [HttpPut("{id:guid}")]
        public async Task<IActionResult> UpdatePrestataire(Guid id, [FromBody] Prestataire dto)
        {
            try
            {
                var updated = await _service.UpdatePrestataire(id, dto);
                return updated == null ? NotFound() : Ok(updated);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // --------------------------------------------------------------------
        // DELETE (LOGIQUE)
        // --------------------------------------------------------------------
        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            try
            {
                var deleted = await _service.DeletePrestataire(id);
                return deleted ? NoContent() : NotFound();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // --------------------------------------------------------------------
        // UPDATE ADRESSE
        // --------------------------------------------------------------------
        [HttpPut("{id:guid}/adresse")]
        public async Task<IActionResult> UpdateAdresse(Guid id, [FromBody] Adresse adresse)
        {
            var p = await _service.UpdateAdresse(id, adresse);
            return p == null ? NotFound() : Ok(p);
        }

        // --------------------------------------------------------------------
        // ADD SPECIALITE
        // --------------------------------------------------------------------
        [HttpPost("{id:guid}/specialites")]
        public async Task<IActionResult> AddSpecialite(Guid id, [FromBody] Specialite specialite)
        {
            var p = await _service.AddSpecialite(id, specialite);
            return p == null ? NotFound() : Ok(p);
        }

        // --------------------------------------------------------------------
        // ADD RESERVATION
        // --------------------------------------------------------------------
        [HttpPost("{id:guid}/reservations")]
        public async Task<IActionResult> AddReservation(Guid id, [FromBody] Reservation reservation)
        {
            var p = await _service.AddReservation(id, reservation);
            return p == null ? NotFound() : Ok(p);
        }

        // --------------------------------------------------------------------
        // ACTIVATION
        // --------------------------------------------------------------------
        [HttpPut("{id:guid}/activate")]
        public async Task<IActionResult> Activate(Guid id)
        {
            var p = await _service.ActivatePrestataire(id);
            return p == null ? NotFound() : Ok(p);
        }

        // --------------------------------------------------------------------
        // STATUS
        // --------------------------------------------------------------------
        [HttpGet("{id:guid}/is-actif")]
        public async Task<IActionResult> IsActif(Guid id)
        {
            return Ok(await _service.IsActif(id));
        }

        // --------------------------------------------------------------------
        // SCORE
        // --------------------------------------------------------------------
        [HttpGet("{id:guid}/score")]
        public async Task<IActionResult> GetScore(Guid id)
        {
            return Ok(await _service.GetScore(id));
        }

        [HttpPut("{id:guid}/score")]
        public async Task<IActionResult> UpdateScore(Guid id, [FromBody] double score)
        {
            var p = await _service.UpdateScore(id, score);
            return p == null ? NotFound() : Ok(p);
        }

        // --------------------------------------------------------------------
        // SEARCH BY NAME
        // --------------------------------------------------------------------
        [HttpGet("search/{name}")]
        public async Task<IActionResult> Search(string name)
        {
            var results = await _service.SearchByName(name);
            return Ok(results);
        }

        // --------------------------------------------------------------------
        // FILTERING
        // --------------------------------------------------------------------
        [HttpGet("filter")]
        public async Task<IActionResult> Filter(
            [FromQuery] string? ville,
            [FromQuery] string? specialite,
            [FromQuery] bool? actif)
        {
            var results = await _service.Filter(ville, specialite, actif);
            return Ok(results);
        }

        // --------------------------------------------------------------------
        // TOP RATED
        // --------------------------------------------------------------------
        [HttpGet("top/{n:int}")]
        public async Task<IActionResult> GetTopRated(int n)
        {
            return Ok(await _service.GetTopRated(n));
        }

        // --------------------------------------------------------------------
        // PAGINATION
        // --------------------------------------------------------------------
        [HttpGet("paged")]
        public async Task<IActionResult> GetPaged(
            [FromQuery] int page = 1,
            [FromQuery] int size = 10)
        {
            var results = await _service.GetPaged(page, size);
            return Ok(results);
        }
    }
}
