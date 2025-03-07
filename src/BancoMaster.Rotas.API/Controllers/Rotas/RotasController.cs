using BancoMaster.Rotas.Application.Rotas.AdcionarRotas.Interface;
using BancoMaster.Rotas.Application.Rotas.CalcularRotas.Interface;
using BancoMaster.Rotas.Application.Rotas.EditarRotas.Interface;
using BancoMaster.Rotas.Application.Rotas.ExcluirRota.Interface;
using BancoMaster.Rotas.Application.Rotas.ListarRotas.Interface;
using BancoMaster.Rotas.DataTransfer.Rotas.Request;
using Microsoft.AspNetCore.Mvc;

namespace BancoMaster.Rotas.API.Controllers.Rotas
{
    [Route("api/[controller]")]
    [ApiController]
    public class RotasController(ICalcularMelhorRotaServico calcularMelhorRotaUseCase, IAdicionarRotaServico adicionarRotaUseCase, IListarRotaServico listarRotasUseCase, IExcluirRotaServico excluirRotaUseCase, IEditarRotaServico editarRotaUseCase) : ControllerBase
    {
        private readonly ICalcularMelhorRotaServico _calcularMelhorRotaUseCase = calcularMelhorRotaUseCase;
        private readonly IAdicionarRotaServico _adicionarRotaUseCase = adicionarRotaUseCase;
        private readonly IListarRotaServico _listarRotasUseCase = listarRotasUseCase;
        private readonly IExcluirRotaServico _excluirRotaUseCase = excluirRotaUseCase;
        private readonly IEditarRotaServico _editarRotaUseCase = editarRotaUseCase;

        /// <summary>
        /// Obtem lista de rotas
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> ListarRotas()
        {
            var response = await _listarRotasUseCase.ExecuteAsync(new ListarRotasRequest());
            return Ok(response.Rotas);
        }

        /// <summary>
        /// Cria nova rotas
        /// </summary>
        /// <param name="rota"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> AdicionarRota([FromBody] AdicionarRotaRequest rota)
        {
            if (rota == null)
                return BadRequest("A rota não pode ser nula.");

            var respose = await _adicionarRotaUseCase.ExecuteAsync(rota);
            return CreatedAtAction(nameof(AdicionarRota), new { respose });
        }

        /// <summary>
        /// Atualiza uma rota
        /// </summary>
        /// <param name="id"></param>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPut("{id}")]
        public async Task<IActionResult> EditarRota(int id, [FromBody] EditarRotaRequest request)
        {
            try
            {
                request.RotaId = id;
                var response = await _editarRotaUseCase.ExecuteAsync(request);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return BadRequest(new { ex.Message });
            }
        }

        /// <summary>
        /// Exclui uma rota
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> ExcluirRota(int id)
        {
            try
            {
                var request = new ExcluirRotaRequest { RotaId = id };
                var response = await _excluirRotaUseCase.ExecuteAsync(request);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return BadRequest(new { ex.Message });
            }
        }

        /// <summary>
        /// Calcula a melhor rota
        /// </summary>
        /// <param name="origem"></param>
        /// <param name="destino"></param>
        /// <returns></returns>
        [HttpGet("calcular-melhor-rota/{origem}/{destino}")]
        public async Task<IActionResult> CalcularMelhorRota(string origem, string destino)
        {
            var response = await _calcularMelhorRotaUseCase.ExecuteAsync(new CalcularMelhorRotaRequest() { Destino = destino, Origem = origem });
            return Ok(response);
        }
    }
}
