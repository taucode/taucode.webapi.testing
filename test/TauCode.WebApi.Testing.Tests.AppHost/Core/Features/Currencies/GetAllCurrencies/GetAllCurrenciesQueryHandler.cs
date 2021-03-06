﻿using NHibernate;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using TauCode.Cqrs.Queries;
using TauCode.WebApi.Testing.Tests.AppHost.Domain.Currencies;

namespace TauCode.WebApi.Testing.Tests.AppHost.Core.Features.Currencies.GetAllCurrencies
{
    public class GetAllCurrenciesQueryHandler : IQueryHandler<GetAllCurrenciesQuery>
    {
        private readonly ISession _session;

        public GetAllCurrenciesQueryHandler(ISession session)
        {
            _session = session;
        }

        public void Execute(GetAllCurrenciesQuery query)
        {
            var currencies = _session
                .Query<Currency>()
                .OrderBy(x => x.Name)
                .ToList();

            var queryResult = new GetAllCurrenciesQueryResult
            {
                Items = currencies
                    .Select(x => new GetAllCurrenciesQueryResult.CurrencyDto
                    {
                        Id = x.Id,
                        Name = x.Name,
                        Code = x.Code,
                    })
                    .ToList(),
            };
            query.SetResult(queryResult);
        }

        public Task ExecuteAsync(GetAllCurrenciesQuery query, CancellationToken cancellationToken = default)
        {
            this.Execute(query);
            return Task.CompletedTask;
        }
    }
}
