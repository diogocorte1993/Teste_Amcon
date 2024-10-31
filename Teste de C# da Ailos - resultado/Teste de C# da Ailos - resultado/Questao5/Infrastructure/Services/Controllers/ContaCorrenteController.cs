using MediatR;
using Microsoft.AspNetCore.Mvc;
using Questao5.Application.Commands.Requests;

namespace Questao5.Infrastructure.Services.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ContaCorrenteController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ContaCorrenteController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("Movimento")]

        public async Task<IActionResult>CreateMovimento(CreateMovimentacaoRequest request)
        {
            try
            {
                return Ok(await _mediator.Send(request));
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message, type = "INTERNAL_ERROR" });
            }
        }

        [HttpGet("Saldo")]

        public async Task<IActionResult> GetSaldo(string idContaCorrente)
        {
            try
            {
                return Ok(await _mediator.Send(new GetSaldoRequest { IdContaCorrente = idContaCorrente }));
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message, type = "INTERNAL_ERROR" });
            }
        }
    }
}