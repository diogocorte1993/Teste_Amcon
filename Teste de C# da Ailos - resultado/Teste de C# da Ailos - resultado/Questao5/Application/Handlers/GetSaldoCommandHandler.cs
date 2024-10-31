using MediatR;
using Microsoft.VisualBasic;
using Questao5.Application.Commands.Requests;
using Questao5.Application.Commands.Responses;
using Questao5.Domain.Entities;
using Questao5.Infrastructure.Sqlite;

namespace Questao5.Application.Handlers
{
    public class GetSaldoCommandHandler : IRequestHandler<GetSaldoRequest, GetSaldoResponse>
    {
        private readonly IDatabaseBootstrap database;
        private readonly IMediator _mediator;

        public GetSaldoCommandHandler(IDatabaseBootstrap databaseBootstrap, IMediator mediator)
        {
            database = databaseBootstrap;
            _mediator = mediator;
        }

        public async Task<GetSaldoResponse> Handle(GetSaldoRequest request, CancellationToken cancellationToken)
        {
            GetContaCorrenteRequest getContaCorrenteRequest = new GetContaCorrenteRequest { IdContaCorrente = request.IdContaCorrente };
            ContaCorrente cc = await _mediator.Send(getContaCorrenteRequest);


            GetMovimentosRequest getMovimentosRequest = new GetMovimentosRequest { IdContaCorrente = request.IdContaCorrente };
            List<Movimento> movimentos = await _mediator.Send(getMovimentosRequest);


            double saldo = 0;
            double credito = 0;
            double debito = 0;
            foreach (var movimento in movimentos)
            {
                if (movimento.tipomovimento == 'C' || movimento.tipomovimento == 'c')
                    credito += movimento.valor;
                else
                    debito += movimento.valor;
            }

            saldo = credito - debito;

            return new GetSaldoResponse
            {
                DataHora = DateTime.Now,
                NumeroDaConta = cc.numero,
                TitularContaCorrente = cc.nome,
                Saldo = saldo
            };
        }
    }
}