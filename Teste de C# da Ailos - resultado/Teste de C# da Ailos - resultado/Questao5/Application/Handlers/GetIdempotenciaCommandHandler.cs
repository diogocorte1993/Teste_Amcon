using MediatR;
using Questao5.Application.Commands.Requests;
using Questao5.Domain.Entities;
using Questao5.Infrastructure.Sqlite;

namespace Questao5.Application.Handlers
{
    public class GetIdempotenciaCommandHandler : IRequestHandler<GetIdempotenciaRequest, Idempotencia>
    {
        private readonly IDatabaseBootstrap database;

        public GetIdempotenciaCommandHandler(IDatabaseBootstrap databaseBootstrap)
        {
            database = databaseBootstrap;
        }

        public async Task<Idempotencia> Handle(GetIdempotenciaRequest request, CancellationToken cancellationToken)
        {
            return await database.QueryFirstOrDefaultAsync<Idempotencia>($"SELECT * FROM idempotencia WHERE chave_idempotencia = '{request.ChaveIdempotencia}'");
        }
    }
}