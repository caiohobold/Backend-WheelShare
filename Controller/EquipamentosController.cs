using EmprestimosAPI.DTO.Equipamento;
using EmprestimosAPI.DTO.Local;
using EmprestimosAPI.Interfaces.ServicesInterfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EmprestimosAPI.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class EquipamentosController : ControllerBase
    {
        private readonly IEquipamentoService _equipamentoService;

        public EquipamentosController(IEquipamentoService equipamentoService)
        {
            _equipamentoService = equipamentoService;
        }

        [Authorize]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<EquipamentoReadDTO>>> GetAll(int pageNumber, int pageSize)
        {
            var idAssociacaoClaim = User.Claims.FirstOrDefault(c => c.Type == "idAssoc");
            if(idAssociacaoClaim == null)
            {
                return Unauthorized("ID da associação não encontrado no token.");
            }

            int idAssociacao = int.Parse(idAssociacaoClaim.Value);

            var equipamentos = await _equipamentoService.GetAllEquip(pageNumber, pageSize, idAssociacao);
            return Ok(equipamentos);
        }

        [Authorize]
        [HttpGet("available")]
        public async Task<ActionResult> GetOnlyAvailable(int pageNumber, int pageSize)
        {
            var idAssociacaoClaim = User.Claims.FirstOrDefault(c => c.Type == "idAssoc");
            if (idAssociacaoClaim == null)
            {
                return Unauthorized("ID da associação não encontrado no token.");
            }

            int idAssociacao = int.Parse(idAssociacaoClaim.Value);
            var equipamentos = await _equipamentoService.GetAllAvailableEquip(pageNumber, pageSize, idAssociacao);
            if (equipamentos == null)
            {
                return NotFound("Nenhum equipamento disponível encontrado.");
            }
            return Ok(equipamentos);
        }

        [Authorize]
        [HttpGet("{id}")]
        public async Task<ActionResult<EquipamentoReadDTO>> GetById(int id)
        {
            var idAssociacaoClaim = User.Claims.FirstOrDefault(c => c.Type == "idAssoc");
            if (idAssociacaoClaim == null)
            {
                return Unauthorized("ID da associação não encontrado no token.");
            }

            int idAssociacao = int.Parse(idAssociacaoClaim.Value);
            var equipamento = await _equipamentoService.GetEquipById(id, idAssociacao);
            if (equipamento == null)
            {
                return NotFound();
            }
            return Ok(equipamento);
        }

        [Authorize]
        [HttpPost]
        public async Task<ActionResult<EquipamentoReadDTO>> Post([FromForm] EquipamentoCreateDTO equipamentoDTO)
        {
            var idAssociacaoClaim = User.Claims.FirstOrDefault(c => c.Type == "idAssoc");
            if (idAssociacaoClaim == null)
            {
                return Unauthorized("ID da associação não encontrado no token.");
            }

            int idAssociacao = int.Parse(idAssociacaoClaim.Value);
            equipamentoDTO.idAssociacao = idAssociacao;
            try
            {
                var equipamentoReadDto = await _equipamentoService.AddEquip(equipamentoDTO);
                return CreatedAtAction(nameof(GetById), new { Id = equipamentoReadDto.IdEquipamento }, equipamentoReadDto);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Authorize(Roles = "Associacao")]
        [HttpPut("{id}")]
        public async Task<ActionResult> Put(int id, [FromForm] EquipamentoUpdateDTO equipamentoDTO)
        {
            var idAssociacaoClaim = User.Claims.FirstOrDefault(c => c.Type == "idAssoc");
            if (idAssociacaoClaim == null)
            {
                return Unauthorized("ID da associação não encontrado no token.");
            }

            int idAssociacao = int.Parse(idAssociacaoClaim.Value);
            equipamentoDTO.idAssociacao = idAssociacao;
            await _equipamentoService.UpdateEquip(id, equipamentoDTO);
            return Ok();
        }

        [HttpPatch("{id}/local")]
        public async Task<ActionResult> UpdateLocal(int id, [FromBody] UpdateLocalDTO updateLocalDTO)
        {
            var idAssociacaoClaim = User.Claims.FirstOrDefault(c => c.Type == "idAssoc");
            if (idAssociacaoClaim == null)
            {
                return Unauthorized("ID da associação não encontrado no token.");
            }

            int idAssociacao = int.Parse(idAssociacaoClaim.Value);
            try
            {
                await _equipamentoService.UpdateLocal(id, updateLocalDTO, idAssociacao);
                return Ok();
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (KeyNotFoundException)
            {
                return NotFound();
            }
        }

        [Authorize]
        [HttpPatch("{id}/estado-equipamento")]
        public async Task<ActionResult> UpdateEstado(int id, [FromBody] UpdateEstadoEquipamentoDTO updateEstadoDTO)
        {
            var idAssociacaoClaim = User.Claims.FirstOrDefault(c => c.Type == "idAssoc");
            if (idAssociacaoClaim == null)
            {
                return Unauthorized("ID da associação não encontrado no token.");
            }

            int idAssociacao = int.Parse(idAssociacaoClaim.Value);
            try
            {
                await _equipamentoService.UpdateEstado(id, updateEstadoDTO, idAssociacao);
                return Ok();
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (KeyNotFoundException)
            {
                return NotFound();
            }
        }

        [Authorize]
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var idAssociacaoClaim = User.Claims.FirstOrDefault(c => c.Type == "idAssoc");
            if (idAssociacaoClaim == null)
            {
                return Unauthorized("ID da associação não encontrado no token.");
            }

            int idAssociacao = int.Parse(idAssociacaoClaim.Value);
            await _equipamentoService.DeleteEquip(id, idAssociacao);
            return NoContent();
        }
    }
}
