﻿using FluentMigrator;

namespace TauCode.WebApi.Testing.Tests.AppHost.DbMigrations
{
    [Migration(0)]
    public class M0_Baseline : AutoReversingMigration
    {
        public override void Up()
        {
            this.Create.Table("currency")
                .WithColumn("id")
                    .AsGuid()
                    .NotNullable()
                    .PrimaryKey("PK_currency")
                .WithColumn("code")
                    .AsAnsiString()
                    .NotNullable()
                    .Unique("UX_currency_code")
                .WithColumn("name")
                    .AsString()
                    .NotNullable();

            this.Create.Table("bid")
                .WithColumn("id")
                    .AsGuid()
                    .NotNullable()
                    .PrimaryKey("PK_bid")
                .WithColumn("name")
                    .AsAnsiString()
                    .NotNullable()
                .WithColumn("price")
                    .AsDecimal()
                    .NotNullable();
        }
    }
}

