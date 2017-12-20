﻿using System.Linq;
using Umbraco.Core.Persistence.SqlSyntax;

namespace Umbraco.Core.Migrations.Upgrade.TargetVersionSevenSixZero
{
    [Migration("7.6.0", 0, Constants.System.UmbracoMigrationName)]
    public class AddIndexToUmbracoNodePath : MigrationBase
    {
        public AddIndexToUmbracoNodePath(IMigrationContext context)
            : base(context)
        { }

        public override void Up()
        {
            var dbIndexes = SqlSyntax.GetDefinedIndexesDefinitions(Context.Database);

            //make sure it doesn't already exist
            if (dbIndexes.Any(x => x.IndexName.InvariantEquals("IX_umbracoNodePath")) == false)
            {
                Create.Index("IX_umbracoNodePath").OnTable("umbracoNode")
                    .OnColumn("path")
                    .Ascending()
                    .WithOptions()
                    .NonClustered()
                    .Do();
            }
        }

        public override void Down()
        {
            Delete.Index("IX_umbracoNodePath").OnTable("umbracoNode").Do();
        }
    }
}