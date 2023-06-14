using ForDevs.Application.Dtos.Avaliacao;
using ForDevs.Application.Dtos.Cliente;
using ForDevs.Application.Interfaces;
using ForDevs.Application.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ForDevs.Services.Api.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class AvaliacaoController : MainController
    {
        private readonly IAvaliacaoAppService _avaliacaoAppService;

        public AvaliacaoController(IAvaliacaoAppService avaliacaoAppService)
        {
            _avaliacaoAppService = avaliacaoAppService;
        }

        [HttpPost("registrar")]
        public async Task<IActionResult> Registrar([FromBody] RegistrarAvaliacaoDto viewMOdel)
        {
            return !ModelState.IsValid ? CustomResponse(ModelState) : CustomResponse(await _avaliacaoAppService.Registrar(viewMOdel));
        }

        [HttpPut("atualizar")]
        public async Task<IActionResult> Atualizar([FromBody] AtualizarAvaliacaoDto viewMOdel)
        {
            return !ModelState.IsValid ? CustomResponse(ModelState) : CustomResponse(await _avaliacaoAppService.Atualizar(viewMOdel));
        }

        [HttpDelete("remover/{id:guid}")]
        public async Task<IActionResult> Remover(Guid id)
        {
            return !ModelState.IsValid ? CustomResponse(ModelState) : CustomResponse(await _avaliacaoAppService.Remover(id));
        }

        [HttpGet("obter/{id:guid}")]
        public async Task<IActionResult> Obter(Guid id)
        {
            return !ModelState.IsValid ? CustomResponse(ModelState) : CustomResponse(await _avaliacaoAppService.ObterPorId(id));
        }

        [HttpGet("obter-listagem")]
        public async Task<IActionResult> ObterListagem()
        {
            return !ModelState.IsValid ? CustomResponse(ModelState) : CustomResponse(await _avaliacaoAppService.ObterLista());
        }
    }
}
