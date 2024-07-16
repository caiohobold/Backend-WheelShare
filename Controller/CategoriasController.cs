using EmprestimosAPI.DTO.Categoria;
using EmprestimosAPI.Interfaces.ServicesInterfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EmprestimosAPI.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriasController : ControllerBase
    {
        private readonly ICategoriaService _categoriaService;

        public CategoriasController(ICategoriaService categoriaService)
        {
            _categoriaService = categoriaService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<CategoriaReadDTO>>> Get(int pageNumber, int pageSize)
        {
            var idAssociacaoClaim = User.Claims.FirstOrDefault(c => c.Type == "idAssoc");
            if (idAssociacaoClaim == null)
            {
                return Unauthorized("ID da associação não encontrado no token.");
            }

            int idAssociacao = int.Parse(idAssociacaoClaim.Value);
            var categorias = await _categoriaService.GetAllAsync(pageNumber, pageSize, idAssociacao);
            return Ok(categorias);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<CategoriaReadDTO>> GetById(int id)
        {
            var idAssociacaoClaim = User.Claims.FirstOrDefault(c => c.Type == "idAssoc");
            if (idAssociacaoClaim == null)
            {
                return Unauthorized("ID da associação não encontrado no token.");
            }

            int idAssociacao = int.Parse(idAssociacaoClaim.Value);
            var categoria = await _categoriaService.GetByIdAsync(id, idAssociacao);
            if (categoria == null) return NotFound();

            return Ok(categoria);
        }

        [Authorize(Roles = "Associacao")]
        [HttpPost]
        public async Task<ActionResult<CategoriaReadDTO>> Post(CategoriaCreateDTO categoriaDTO)
        {
            var idAssociacaoClaim = User.Claims.FirstOrDefault(c => c.Type == "idAssoc");
            if (idAssociacaoClaim == null)
            {
                return Unauthorized("ID da associação não encontrado no token.");
            }

            int idAssociacao = int.Parse(idAssociacaoClaim.Value);
            categoriaDTO.idAssociacao = idAssociacao;
            var categoriaReadDto = await _categoriaService.CreateAsync(categoriaDTO);

            return CreatedAtAction(nameof(GetById), new { Id = categoriaReadDto.IdCategoria }, categoriaReadDto);
        }

        [Authorize(Roles = "Associacao")]
        [HttpPut("{id}")]
        public async Task<ActionResult> Put(int id, CategoriaUpdateDTO categoriaDTO)
        {
            var idAssociacaoClaim = User.Claims.FirstOrDefault(c => c.Type == "idAssoc");
            if (idAssociacaoClaim == null)
            {
                return Unauthorized("ID da associação não encontrado no token.");
            }

            int idAssociacao = int.Parse(idAssociacaoClaim.Value);
            categoriaDTO.idAssociacao = idAssociacao;
            await _categoriaService.UpdateAsync(id, categoriaDTO);
            return Ok();
        }

        [Authorize(Roles = "Associacao")]
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var idAssociacaoClaim = User.Claims.FirstOrDefault(c => c.Type == "idAssoc");
            if (idAssociacaoClaim == null)
            {
                return Unauthorized("ID da associação não encontrado no token.");
            }

            int idAssociacao = int.Parse(idAssociacaoClaim.Value);
            await _categoriaService.DeleteAsync(id, idAssociacao);
            return NoContent();
        }
    }
}
