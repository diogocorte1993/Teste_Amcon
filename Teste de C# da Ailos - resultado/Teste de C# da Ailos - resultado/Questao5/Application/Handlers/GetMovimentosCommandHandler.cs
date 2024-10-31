using MediatR;
using Questao5.Application.Commands.Requests;
using Questao5.Domain.Entities;
using Questao5.Infrastructure.Sqlite;

namespace Questao5.Application.Handlers
{
    public class GetMovimentosCommandHandler : IRequestHandler<GetMovimentosRequest, List<Movimento>>
    {
        private readonly IDatabaseBootstrap database;

        public GetMovimentosCommandHandler(IDatabaseBootstrap databaseBootstrap)
        {
            database = databaseBootstrap;
        }

        public async Task<List<Movimento>> Handle(GetMovimentosRequest request, CancellationToken cancellationToken)
        {
            return await database.QueryAsync<Movimento>($"SELECT * FROM movimento WHERE idcontacorrente = '{request.IdContaCorrente}'");
        }
    }
}