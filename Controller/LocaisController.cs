using EmprestimosAPI.DTO.Local;
using EmprestimosAPI.Interfaces.ServicesInterfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EmprestimosAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LocaisController : ControllerBase
    {
        private readonly ILocalService _localService;

        public LocaisController(ILocalService localService)
        {
            _localService = localService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<LocalReadDTO>>> GetAllLocais([FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 10)
        {
            var idAssociacaoClaim = User.Claims.FirstOrDefault(c => c.Type == "idAssoc");
            if (idAssociacaoClaim == null)
            {
                return Unauthorized("ID da associação não encontrado no token.");
            }

            int idAssociacao = int.Parse(idAssociacaoClaim.Value);
            var locais = await _localService.GetAllLocaisAsync(pageNumber, pageSize, idAssociacao);
            return Ok(locais);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<LocalReadDTO>> GetLocalById(int id)
        {
            var idAssociacaoClaim = User.Claims.FirstOrDefault(c => c.Type == "idAssoc");
            if (idAssociacaoClaim == null)
            {
                return Unauthorized("ID da associação não encontrado no token.");
            }

            int idAssociacao = int.Parse(idAssociacaoClaim.Value);
            var local = await _localService.GetLocalByIdAsync(id, idAssociacao);
            if (local == null)
            {
                return NotFound();
            }

            return Ok(local);
        }

        [Authorize]
        [HttpPost]
        public async Task<ActionResult<LocalReadDTO>> AddLocal(LocalCreateDTO localDTO)
        {
            var idAssociacaoClaim = User.Claims.FirstOrDefault(c => c.Type == "idAssoc");
            if (idAssociacaoClaim == null)
            {
                return Unauthorized("ID da associação não encontrado no token.");
            }

            int idAssociacao = int.Parse(idAssociacaoClaim.Value);
            localDTO.idAssociacao = idAssociacao;
            var local = await _localService.AddLocalAsync(localDTO);
            return CreatedAtAction(nameof(GetLocalById), new { id = local.IdLocal }, local);
        }

        [Authorize]
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateLocal(int id, LocalUpdateDTO localDTO)
        {
            var idAssociacaoClaim = User.Claims.FirstOrDefault(c => c.Type == "idAssoc");
            if (idAssociacaoClaim == null)
            {
                return Unauthorized("ID da associação não encontrado no token.");
            }

            int idAssociacao = int.Parse(idAssociacaoClaim.Value);
            localDTO.idAssociacao = idAssociacao;
            try
            {
                await _localService.UpdateLocalAsync(id, localDTO);
            }
            catch (KeyNotFoundException)
            {
                return NotFound();
            }

            return NoContent();
        }

        [Authorize]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteLocal(int id)
        {
            var idAssociacaoClaim = User.Claims.FirstOrDefault(c => c.Type == "idAssoc");
            if (idAssociacaoClaim == null)
            {
                return Unauthorized("ID da associação não encontrado no token.");
            }

            int idAssociacao = int.Parse(idAssociacaoClaim.Value);
            try
            {
                await _localService.DeleteLocalAsync(id, idAssociacao);
            }
            catch (KeyNotFoundException)
            {
                return NotFound();
            }

            return NoContent();
        }
    }
}
