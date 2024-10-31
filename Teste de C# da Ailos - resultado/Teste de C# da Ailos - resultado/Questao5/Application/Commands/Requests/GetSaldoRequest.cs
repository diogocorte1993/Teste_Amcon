using MediatR;
using Questao5.Application.Commands.Responses;

namespace Questao5.Application.Commands.Requests
{
    public class GetSaldoRequest: IRequest<GetSaldoResponse>
    {
        public string IdContaCorrente { get; set; }
    }
}
