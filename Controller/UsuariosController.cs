using EmprestimosAPI.DTO.Usuario;
using EmprestimosAPI.Interfaces.Account;
using EmprestimosAPI.Interfaces.Services;
using EmprestimosAPI.Interfaces.ServicesInterfaces;
using EmprestimosAPI.Models;
using EmprestimosAPI.Token;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;
using System.Security.Claims;
using WheelShareAPI.DTO.Usuario;

namespace EmprestimosAPI.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuariosController : ControllerBase
    {
        private readonly IUsuarioService _usuarioService;
        private readonly IAuthenticate _authenticateService;
        private readonly IEmailService _emailService;

        public UsuariosController(IUsuarioService usuarioService, IAuthenticate authenticateService, IEmailService emailService)
        {
            _usuarioService = usuarioService;
            _authenticateService = authenticateService;
            _emailService = emailService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<UsuarioReadDTO>>> GetAll(int pageNumber, int pageSize)
        {
            var idAssociacaoClaim = User.Claims.FirstOrDefault(c => c.Type == "idAssoc");
            if (idAssociacaoClaim == null)
            {
                Console.WriteLine("ID da associação não encontrado no token.");
                foreach (var claim in User.Claims)
                {
                    Console.WriteLine($"Claim Type: {claim.Type}, Claim Value: {claim.Value}");
                }
                return Unauthorized("ID da associação não encontrado no token.");
            }

            int idAssociacao = int.Parse(idAssociacaoClaim.Value);
            var users = await _usuarioService.GetAllUsers(pageNumber, pageSize, idAssociacao);
            return Ok(users);
        }

        [Authorize]
        [HttpGet("{id}")]
        public async Task<ActionResult<UsuarioReadDTO>> GetById(int id)
        {
            var idAssociacaoClaim = User.Claims.FirstOrDefault(c => c.Type == "idAssoc");
            if (idAssociacaoClaim == null)
            {
                return Unauthorized("ID da associação não encontrado no token.");
            }

            int idAssociacao = int.Parse(idAssociacaoClaim.Value);
            var usuario = await _usuarioService.GetUserById(id, idAssociacao);
            if (usuario == null)
            {
                return NotFound();
            }
            return Ok(usuario);
        }

        [Authorize(Roles = "Associacao")]
        [HttpPost("register-user")]
        public async Task<ActionResult<UserToken>> Post(UsuarioCreateDTO usuarioDTO)
        {
            if (usuarioDTO == null)
            {
                return BadRequest("Dados inválidos");
            }

            var emailExiste = await _authenticateService.UserExists(usuarioDTO.EmailPessoal);

            if (emailExiste)
            {
                return BadRequest("Este e-mail já possui um cadastro.");
            }

            var idAssociacaoClaim = User.Claims.FirstOrDefault(c => c.Type == "idAssoc");
            if (idAssociacaoClaim == null)
            {
                return Unauthorized("ID da associação não encontrado no token.");
            }

            int idAssociacao = int.Parse(idAssociacaoClaim.Value);
            usuarioDTO.IdAssociacao = idAssociacao;

            var usuarioReadDto = await _usuarioService.AddUser(usuarioDTO, idAssociacao);
            if (usuarioReadDto == null)
            {
                return BadRequest("Ocorreu um erro ao cadastrar.");
            }

            var token = _authenticateService.GenerateToken(usuarioReadDto.IdUsuario, usuarioReadDto.NomeCompleto, usuarioReadDto.EmailPessoal, "Usuario", usuarioReadDto.IdAssociacao, usuarioReadDto.AssociacaoNomeFantasia);

            return new UserToken
            {
                Token = token
            };
        }

        [HttpPost("login")]
        public async Task<ActionResult<UserToken>> Selecionar(LoginModel loginModel)
        {
            var existe = await _authenticateService.UserExists(loginModel.Email);
            if (!existe)
            {
                return Unauthorized("Usuário não existe.");
            }

            var result = await _authenticateService.AuthenticateAsync(loginModel.Email, loginModel.Senha);
            if (!result)
            {
                return Unauthorized("E-mail ou senha inválidos!");
            }

            var usuario = await _authenticateService.GetUserByEmail(loginModel.Email);
            var token = _authenticateService.GenerateToken(usuario.IdUsuario, usuario.NomeCompleto, usuario.EmailPessoal, "Usuario", usuario.IdAssociacao, usuario.Associacao.NomeFantasia);

            return new UserToken { Token = token };
        }

        [Authorize(Roles = "Associacao")]
        [HttpPut("{id}")]
        public async Task<ActionResult> Put(int id, UsuarioUpdateDTO usuarioDTO)
        {
            var idAssociacaoClaim = User.Claims.FirstOrDefault(c => c.Type == "idAssoc");
            if (idAssociacaoClaim == null)
            {
                return Unauthorized("ID da associação não encontrado no token.");
            }

            int idAssociacao = int.Parse(idAssociacaoClaim.Value);
            await _usuarioService.UpdateUser(id, usuarioDTO, idAssociacao);
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
            await _usuarioService.DeleteUser(id, idAssociacao);
            return NoContent();
        }

        [Authorize]
        [HttpGet("me")]
        public async Task<ActionResult<UsuarioReadDTO>> GetMe()
        {
            var userIdClaim = User.Claims.FirstOrDefault(c => c.Type == "id")?.Value;
            var idAssociacaoClaim = User.Claims.FirstOrDefault(c => c.Type == "idAssoc")?.Value;

            if (string.IsNullOrEmpty(userIdClaim) || !int.TryParse(userIdClaim, out int userId))
            {
                return BadRequest("Invalid token");
            }

            if (string.IsNullOrEmpty(idAssociacaoClaim) || !int.TryParse(idAssociacaoClaim, out int idAssociacao))
            {
                return BadRequest("ID da associação não encontrado no token.");
            }

            var usuario = await _usuarioService.GetUserById(userId, idAssociacao);
            if (usuario == null)
            {
                return NotFound("User not found");
            }

            return Ok(usuario);
        }

        [Authorize]
        [HttpPut("me")]
        public async Task<ActionResult> UpdateMe([FromBody] UsuarioUpdateDTO usuarioDTO)
        {
            var userIdClaim = User.Claims.FirstOrDefault(c => c.Type == "id")?.Value;
            var idAssociacaoClaim = User.Claims.FirstOrDefault(c => c.Type == "idAssoc")?.Value;

            if (string.IsNullOrEmpty(userIdClaim) || !int.TryParse(userIdClaim, out int userId))
            {
                return BadRequest("Invalid token");
            }

            if (string.IsNullOrEmpty(idAssociacaoClaim) || !int.TryParse(idAssociacaoClaim, out int idAssociacao))
            {
                return BadRequest("ID da associação não encontrado no token.");
            }

            await _usuarioService.UpdateUser(userId, usuarioDTO, idAssociacao);
            return NoContent();
        }

        [HttpPut("{id}/change-password")]
        public async Task<ActionResult> ChangePassword(int id, [FromBody] ChangePasswordDTO changePasswordDTO)
        {
            if (changePasswordDTO == null || string.IsNullOrWhiteSpace(changePasswordDTO.NovaSenha))
            {
                return BadRequest("Senha inválida.");
            }

            var idAssociacaoClaim = User.Claims.FirstOrDefault(c => c.Type == "idAssoc");
            if (idAssociacaoClaim == null)
            {
                return Unauthorized("ID da associação não encontrado no token.");
            }

            int idAssociacao = int.Parse(idAssociacaoClaim.Value);
            await _usuarioService.ChangeUserPassword(id, changePasswordDTO.NovaSenha, idAssociacao);
            return NoContent();
        }

        [HttpPost("forgot-password")]
        public async Task<IActionResult> ForgotPassword([FromBody] ForgotPasswordDTO forgotPasswordDTO)
        {
            if (string.IsNullOrEmpty(forgotPasswordDTO.Email))
            {
                return BadRequest("Email is required.");
            }

            var result = await _usuarioService.ResetPasswordAsync(forgotPasswordDTO.Email);
            if (!result)
            {
                return NotFound("O e-mail informado não está vinculado a nenhuma conta.");
            }

            return Ok("Uma nova senha foi encaminhada para o seu e-mail!");
        }
    }
}
