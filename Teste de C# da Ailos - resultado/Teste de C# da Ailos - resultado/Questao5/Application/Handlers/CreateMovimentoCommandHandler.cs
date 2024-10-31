using MediatR;
using Newtonsoft.Json;
using Questao5.Application.Commands.Requests;
using Questao5.Domain.Entities;
using Questao5.Infrastructure.Sqlite;
using System.Data;
using System.Data.Common;

namespace Questao5.Application.Handlers
{
    public class CreateMovimentoCommandHandler : IRequestHandler<CreateMovimentacaoRequest, string>
    {
        private readonly IDatabaseBootstrap database;
        private readonly IMediator _mediator;
        public CreateMovimentoCommandHandler(IDatabaseBootstrap databaseBootstrap, IMediator mediator)
        {
            database = databaseBootstrap;
            _mediator = mediator;
        }

        public async Task<string> Handle(CreateMovimentacaoRequest request, CancellationToken cancellationToken)
        {
            if (request.Valor <= 0)
                throw new Exception("INVALID_VALUE");

            request.Tipo = request.Tipo.ToString().ToUpper()[0];
            if (request.Tipo != 'C' && request.Tipo != 'D')
                throw new Exception("INVALID_TYPE");

            GetContaCorrenteRequest getContaCorrenteRequest = new GetContaCorrenteRequest { IdContaCorrente = request.IdContaCorrente };
            ContaCorrente cc = await _mediator.Send(getContaCorrenteRequest);

            GetIdempotenciaRequest getIdempotenciaRequest = new GetIdempotenciaRequest { ChaveIdempotencia = request.IdRequisicao };
            Idempotencia idempotencia = await _mediator.Send(getIdempotenciaRequest);
            if (idempotencia != null)
                return idempotencia.resultado;

            Movimento movimento = new Movimento
            {
                idmovimento = Guid.NewGuid().ToString(),
                datamovimento = DateTime.Now,
                idcontacorrente = cc.idcontacorrente,
                tipomovimento = request.Tipo,
                valor = request.Valor
            };

            var sql = @"INSERT INTO movimento (idmovimento, idcontacorrente, datamovimento, tipomovimento, valor)
                        VALUES (@idmovimento, @idcontacorrente,@datamovimento, @tipomovimento, @valor)";
            await database.ExecuteAsync(sql, movimento);

            idempotencia = new Idempotencia
            {
                chave_idempotencia = request.IdRequisicao,
                requisicao = JsonConvert.SerializeObject(request),
                resultado = movimento.idmovimento
            };
            sql = @"INSERT INTO idempotencia (chave_idempotencia, requisicao, resultado) VALUES (@chave_idempotencia, @requisicao, @resultado)";
            await database.ExecuteAsync(sql, idempotencia);

            return movimento.idmovimento;
        }
    }
}