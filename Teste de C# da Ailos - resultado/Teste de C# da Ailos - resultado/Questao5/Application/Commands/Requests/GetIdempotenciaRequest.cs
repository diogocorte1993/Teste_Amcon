using MediatR;
using Questao5.Domain.Entities;

namespace Questao5.Application.Commands.Requests
{
    public class GetIdempotenciaRequest : IRequest<Idempotencia>
    {
        public string ChaveIdempotencia { get; set; }
    }
}