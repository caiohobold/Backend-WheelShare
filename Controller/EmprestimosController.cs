using EmprestimosAPI.DTO.Emprestimo;
using EmprestimosAPI.Interfaces.ServicesInterfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EmprestimosAPI.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmprestimosController : ControllerBase
    {
        private readonly IEmprestimoService _emprestimoService;

        private int GetAssociacaoId()
        {
            var claim = User.Claims.FirstOrDefault(c => c.Type == "idAssoc");
            if (claim == null)
            {
                throw new UnauthorizedAccessException("Associação não encontrada no token.");
            }
            return int.Parse(claim.Value);
        }

        public EmprestimosController(IEmprestimoService emprestimoService)
        {
            _emprestimoService = emprestimoService;
        }

        [Authorize]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<EmprestimoReadDTO>>> GetAll(int pageNumber, int pageSize)
        {
            int idAssociacao = GetAssociacaoId();
            var emprestimos = await _emprestimoService.GetAllEmp(pageNumber, pageSize, idAssociacao);
            return Ok(emprestimos);
        }


        [Authorize]
        [HttpGet("{id}")]
        public async Task<ActionResult<EmprestimoReadDTO>> GetById(int id)
        {
            int idAssociacao = GetAssociacaoId();
            var emprestimo = await _emprestimoService.GetEmpById(id, idAssociacao);
            if (emprestimo == null)
            {
                return NotFound();
            }
            return Ok(emprestimo);
        }


        [Authorize]
        [HttpGet("pessoa/{idPessoa}")]
        public async Task<ActionResult<IEnumerable<EmprestimoReadDTO>>> GetEmpByPessoaId(int idPessoa)
        {
            int idAssociacao = GetAssociacaoId();
            var emprestimos = await _emprestimoService.GetEmpByPessoaId(idPessoa, idAssociacao);
            if (emprestimos == null || !emprestimos.Any())
            {
                return NotFound("Nenhum empréstimo encontrado para essa pessoa");
            }
            return Ok(emprestimos);
        }


        [Authorize]
        [HttpGet("active")]
        public async Task<ActionResult> GetOnlyActive(int pageNumber, int pageSize)
        {
            int idAssociacao = GetAssociacaoId();
            var emprestimos = await _emprestimoService.GetActiveEmp(pageNumber, pageSize, idAssociacao);
            if (emprestimos == null || !emprestimos.Any())
            {
                return NotFound("Nenhum empréstimo ativo encontrado.");
            }
            return Ok(emprestimos);
        }


        [Authorize]
        [HttpGet("atrasados")]
        public async Task<ActionResult<IEnumerable<EmprestimoReadDTO>>> GetEmprestimosAtrasados()
        {
            int idAssociacao = GetAssociacaoId();
            var emprestimos = await _emprestimoService.GetEmpAtrasados(idAssociacao);
            return Ok(emprestimos);
        }

        [Authorize]
        [HttpGet("agendados")]
        public async Task<ActionResult<IEnumerable<EmprestimoReadDTO>>> GetEmprestimosAgendados()
        {
            int idAssociacao = GetAssociacaoId();
            var emprestimos = await _emprestimoService.GetEmpAgendado(idAssociacao);
            return Ok(emprestimos);
        }


        [Authorize]
        [HttpPost]
        public async Task<ActionResult<EmprestimoReadDTO>> Post(EmprestimoCreateDTO emprestimoDTO)
        {
            int idAssociacao = GetAssociacaoId();
            emprestimoDTO.idAssociacao = idAssociacao;
            var emprestimoReadDto = await _emprestimoService.AddEmp(emprestimoDTO, idAssociacao);

            return CreatedAtAction(nameof(GetById), new { Id = emprestimoReadDto.Id }, emprestimoReadDto);
        }


        [Authorize]
        [HttpPut("{id}")]
        public async Task<ActionResult> Put(int id, EmprestimoUpdateDTO emprestimoDTO)
        {
            int idAssociacao = GetAssociacaoId();
            await _emprestimoService.UpdateEmp(id, emprestimoDTO, idAssociacao);
            return Ok();
        }


        [Authorize]
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            int idAssociacao = GetAssociacaoId();
            await _emprestimoService.DeleteEmp(id, idAssociacao);
            return NoContent();
        }


        [Authorize]
        [HttpPost("{id}/finalizar")]
        public async Task<ActionResult> FinalizarEmprestimo(int id)
        {
            int idAssociacao = GetAssociacaoId();
            try
            {
                await _emprestimoService.FinalizarEmp(id, idAssociacao);
                return Ok("Empréstimo finalizado com sucesso.");
            }
            catch (KeyNotFoundException knf)
            {
                return NotFound(knf.Message);
            }
            catch (InvalidOperationException ioe)
            {
                return BadRequest(ioe.Message);
            }
        }

    }
}
