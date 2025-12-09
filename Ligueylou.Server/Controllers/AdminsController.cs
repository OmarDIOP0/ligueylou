using Ligueylou.Server.Dtos;
using Ligueylou.Server.Identity;
using Ligueylou.Server.Models;
using Ligueylou.Server.Request;
using Ligueylou.Server.Services.Administrateurs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Ligueylou.Server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AdminsController : ControllerBase
    {
        private readonly IAdminService _service;

        public AdminsController(IAdminService service)
        {
            _service = service;
        }

        [Authorize(Policy = IdentityData.AdminPolicy)]
        [HttpGet]
        public async Task<IActionResult> GetAll()
            => Ok(await _service.GetAllAdmins());

        [Authorize(Policy = IdentityData.AdminPolicy)]
        [HttpGet("{id:guid}")]
        public async Task<IActionResult> Get(Guid id)
        {
            var admin = await _service.GetAdminById(id);
            return admin == null ? NotFound() : Ok(admin);
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateUtilisateurDto dto)
        {
            var created = await _service.AddAdmin(dto);
            return CreatedAtAction(nameof(Get), new { id = created.Id }, created);
        }

        [Authorize(Policy = IdentityData.AdminPolicy)]
        [HttpPut("{id:guid}")]
        public async Task<IActionResult> Update(Guid id, CreateUtilisateurDto dto)
        {
            var updated = await _service.UpdateAdmin(id, dto);
            return updated == null ? NotFound() : Ok(updated);
        }

        [Authorize(Policy = IdentityData.AdminPolicy)]
        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var ok = await _service.DeleteAdmin(id);
            return ok ? NoContent() : NotFound();
        }

        //--------------------------
        // ADRESSE
        //--------------------------
        [Authorize(Policy = IdentityData.AdminPolicy)]
        [HttpPut("{id:guid}/adresse")]
        public async Task<IActionResult> UpdateAdresse(Guid id, Adresse adresse)
        {
            var updated = await _service.UpdateAdresse(id, adresse);
            return updated == null ? NotFound() : Ok(updated);
        }

        //--------------------------
        // CODE SECRET
        //--------------------------
        [Authorize(Policy = IdentityData.AdminPolicy)]
        [HttpPost("{id:guid}/verifyCode")]
        public async Task<IActionResult> VerifyCode(Guid id, [FromBody] string codeSecret)
        {
            var ok = await _service.VerifyCodeSecret(id, codeSecret);
            return ok ? Ok(new { message = "Code valide" }) : Unauthorized(new { message = "Code invalide" });
        }

        [Authorize(Policy = IdentityData.AdminPolicy)]
        [HttpPut("{id:guid}/codeSecret")]
        public async Task<IActionResult> UpdateCode(Guid id, [FromBody] string newCode)
        {
            var ok = await _service.UpdateCodeSecret(id, newCode);
            return ok ? Ok(new { message = "Code mis à jour" }) : NotFound();
        }

        //--------------------------
        // FILTRAGE
        //--------------------------
        [Authorize(Policy = IdentityData.AdminPolicy)]
        [HttpGet("filter")]
        public async Task<IActionResult> Filter([FromQuery] string? ville, [FromQuery] bool? actif)
        {
            return Ok(await _service.FilterAdmins(ville, actif));
        }
    }
}
