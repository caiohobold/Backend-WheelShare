using EmprestimosAPI.DTO.Pessoa;
using EmprestimosAPI.Interfaces.ServicesInterfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace EmprestimosAPI.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class PessoasController : ControllerBase
    {
        private readonly IPessoaService _pessoaService;

        public PessoasController(IPessoaService pessoaService)
        {
            _pessoaService = pessoaService;
        }

        [Authorize]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<PessoaReadDTO>>> Get(int pageNumber, int pageSize)
        {
            var idAssociacaoClaim = User.Claims.FirstOrDefault(c => c.Type == "idAssoc");
            if (idAssociacaoClaim == null)
            {
                return Unauthorized("ID da associação não encontrado no token.");
            }

            int idAssociacao = int.Parse(idAssociacaoClaim.Value);

            var pessoas = await _pessoaService.GetAllPessoasAsync(pageNumber, pageSize, idAssociacao);
            return Ok(pessoas);
        }

        [Authorize]
        [HttpGet("{id}")]
        public async Task<ActionResult<PessoaReadDTO>> GetById(int id)
        {
            var idAssociacaoClaim = User.Claims.FirstOrDefault(c => c.Type == "idAssoc");
            if (idAssociacaoClaim == null)
            {
                return Unauthorized("ID da associação não encontrado no token.");
            }

            int idAssociacao = int.Parse(idAssociacaoClaim.Value);

            var pessoa = await _pessoaService.GetPessoaById(id, idAssociacao);
            if (pessoa == null) return NotFound();

            return Ok(pessoa);
        }

        [HttpPost]
        [Authorize]
        public async Task<ActionResult<PessoaReadDTO>> Post(PessoaCreateDTO pessoaDTO)
        {
            var idAssociacaoClaim = User.Claims.FirstOrDefault(c => c.Type == "idAssoc");
            if (idAssociacaoClaim == null)
            {
                return Unauthorized("ID da associação não encontrado no token.");
            }

            int idAssociacao = int.Parse(idAssociacaoClaim.Value);
            pessoaDTO.idAssociacao = idAssociacao;

            var pessoaReadDto = await _pessoaService.AddPessoaAsync(pessoaDTO);

            return CreatedAtAction(nameof(GetById), new { Id = pessoaReadDto.IdPessoa }, pessoaReadDto);
        }

        [Authorize(Roles = "Associacao, Usuario")]
        [HttpPut("{id}")]
        public async Task<ActionResult> Put(int id, PessoaUpdateDTO pessoaDTO)
        {
            var idAssociacaoClaim = User.Claims.FirstOrDefault(c => c.Type == "idAssoc");
            if (idAssociacaoClaim == null)
            {
                return Unauthorized("ID da associação não encontrado no token.");
            }

            int idAssociacao = int.Parse(idAssociacaoClaim.Value);
            pessoaDTO.idAssociacao = idAssociacao;

            await _pessoaService.UpdatePessoaAsync(id, pessoaDTO);
            return Ok();
        }

        [Authorize(Roles = "Associacao, Usuario")]
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var idAssociacaoClaim = User.Claims.FirstOrDefault(c => c.Type == "idAssoc");
            if (idAssociacaoClaim == null)
            {
                return Unauthorized("ID da associação não encontrado no token.");
            }

            int idAssociacao = int.Parse(idAssociacaoClaim.Value);

            await _pessoaService.DeletePessoaAsync(id, idAssociacao);
            return NoContent();
        }
    }
}
