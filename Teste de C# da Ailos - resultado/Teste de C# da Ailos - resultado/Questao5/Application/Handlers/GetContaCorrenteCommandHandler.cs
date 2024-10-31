using MediatR;
using Questao5.Application.Commands.Requests;
using Questao5.Domain.Entities;
using Questao5.Infrastructure.Sqlite;

namespace Questao5.Application.Handlers
{
    public class GetContaCorrenteCommandHandler : IRequestHandler<GetContaCorrenteRequest, ContaCorrente>
    {
        private readonly IDatabaseBootstrap database;

        public GetContaCorrenteCommandHandler(IDatabaseBootstrap databaseBootstrap)
        {
            database = databaseBootstrap;
        }

        public async Task<ContaCorrente> Handle(GetContaCorrenteRequest request, CancellationToken cancellationToken)
        {
            var contaCorrente = await database.QueryFirstOrDefaultAsync<ContaCorrente>($"SELECT * FROM contacorrente WHERE idcontacorrente = '{request.IdContaCorrente}'");

            if (contaCorrente == null)
                throw new Exception("INVALID_ACCOUNT");

            if(!contaCorrente.ativo)
                throw new Exception("INACTIVE_ACCOUNT");

            return contaCorrente!;
        }
    }
}