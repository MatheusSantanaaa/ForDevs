using ForDevs.Application.Dtos.Cliente;
using ForDevs.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ForDevs.Services.Api.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class ClienteController : MainController
    {
        private readonly IClienteAppService _clienteAppService;

        public ClienteController(IClienteAppService clienteAppService)
        {
            _clienteAppService = clienteAppService;
        }

        [HttpPost("registrar")]
        public async Task<IActionResult> Registrar([FromBody] RegistrarClienteDto viewMOdel)
        {
            return !ModelState.IsValid ? CustomResponse(ModelState) : CustomResponse(await _clienteAppService.Registrar(viewMOdel));
        }

        [HttpPut("atualizar")]
        public async Task<IActionResult> Atualizar([FromBody] AtualizarClienteDto viewMOdel)
        {
            return !ModelState.IsValid ? CustomResponse(ModelState) : CustomResponse(await _clienteAppService.Atualizar(viewMOdel));
        }

        [HttpDelete("remover/{id:guid}")]
        public async Task<IActionResult> Remover(Guid id)
        {
            return !ModelState.IsValid ? CustomResponse(ModelState) : CustomResponse(await _clienteAppService.Remover(id));
        }

        [HttpGet("obter/{id:guid}")]
        public async Task<IActionResult> Obter(Guid id)
        {
            return !ModelState.IsValid ? CustomResponse(ModelState) : CustomResponse(await _clienteAppService.ObterPorId(id));
        }

        [HttpGet("obter-listagem")]
        public async Task<IActionResult> ObterListagem()
        {
            return !ModelState.IsValid ? CustomResponse(ModelState) : CustomResponse(await _clienteAppService.ObterLista());
        }

        [AllowAnonymous]
        [HttpGet("obter-pelo-nome")]
        public async Task<IActionResult> Obter([FromQuery] string nome)
        {
            return !ModelState.IsValid ? CustomResponse(ModelState) : CustomResponse(await _clienteAppService.ObterPorNome(nome));
        }
    }
}
