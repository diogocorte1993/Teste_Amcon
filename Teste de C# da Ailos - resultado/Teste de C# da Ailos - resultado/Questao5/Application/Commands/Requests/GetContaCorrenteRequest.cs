using MediatR;
using Questao5.Domain.Entities;

namespace Questao5.Application.Commands.Requests
{
    public class GetContaCorrenteRequest : IRequest<ContaCorrente>
    {
        public string IdContaCorrente { get; set; }
    }
}