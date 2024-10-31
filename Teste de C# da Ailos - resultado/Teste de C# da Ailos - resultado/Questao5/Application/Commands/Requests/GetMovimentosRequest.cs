using MediatR;
using Questao5.Domain.Entities;

namespace Questao5.Application.Commands.Requests
{
    public class GetMovimentosRequest : IRequest<List<Movimento>>
    {
        public string IdContaCorrente { get; set; }
    }
}