﻿using System.Threading;
using System.Threading.Tasks;
using TauCode.Cqrs.Commands;
using TauCode.WebApi.Testing.Tests.AppHost.Core.Exceptions;
using TauCode.WebApi.Testing.Tests.AppHost.Domain.Currencies;
using TauCode.WebApi.Testing.Tests.AppHost.Domain.Currencies.Exceptions;

namespace TauCode.WebApi.Testing.Tests.AppHost.Core.Features.Currencies.UpdateCurrency
{
    public class UpdateCurrencyCommandHandler : ICommandHandler<UpdateCurrencyCommand>
    {
        private readonly ICurrencyRepository _currencyRepository;

        public UpdateCurrencyCommandHandler(ICurrencyRepository currencyRepository)
        {
            _currencyRepository = currencyRepository;
        }

        public void Execute(UpdateCurrencyCommand command)
        {
            var currency = _currencyRepository.GetById(command.Id);
            if (currency == null)
            {
                throw new CurrencyNotFoundException();
            }

            if (command.Code != null)
            {
                var currencyWithSameCode = _currencyRepository.GetByCode(command.Code);
                if (currencyWithSameCode != null && currencyWithSameCode.Id != command.Id)
                {
                    throw new CodeAlreadyExistsException();
                }

                currency.ChangeCode(command.Code);
            }

            if (command.Name != null)
            {
                currency.ChangeName(command.Name);
            }

            _currencyRepository.Save(currency);
        }

        public Task ExecuteAsync(UpdateCurrencyCommand command, CancellationToken cancellationToken = default)
        {
            this.Execute(command);
            return Task.CompletedTask;
        }
    }
}
