﻿using TauCode.Cqrs.Commands;
using TauCode.WebApi.Testing.Tests.AppHost.Domain.Currencies;

namespace TauCode.WebApi.Testing.Tests.AppHost.Core.Features.Currencies.CreateCurrency
{
    public class CreateCurrencyCommand : Command<CurrencyId>
    {
        public string Name { get; set; }
        public string Code { get; set; }
    }
}
