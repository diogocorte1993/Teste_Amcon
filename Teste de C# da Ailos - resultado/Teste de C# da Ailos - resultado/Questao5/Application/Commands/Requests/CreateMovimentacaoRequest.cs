using MediatR;

namespace Questao5.Application.Commands.Requests
{
    public class CreateMovimentacaoRequest: IRequest<string>
    {
        public string IdRequisicao { get; set; }
        public string IdContaCorrente { get; set; }
        public double Valor { get; set; }
        public char Tipo { get; set; }
    }
}
