using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Common;
using System.Linq;

namespace Sentinel.Tests.Database
{
    public class DatabaseTest : SentinelTestBase
    {
        public DatabaseTest(string name, string description) : base(name, description)
        {
        }

        protected override void RunTest()
        {
            var connections = GetConnections().ToList();

            if (!connections.Any())
            {
                SetResult(TestResult.CreateFailed(GetName(), "No connections found."));
                return;
            }

            foreach (var connection in connections)
            {
                try
                {
                    var factory = DbProviderFactories.GetFactory(connection.ProviderName);
                    using (var dbConnection = factory.CreateConnection())
                    {
                        if (dbConnection == null)
                            throw new InvalidOperationException("Connection cannot be null.");

                        dbConnection.ConnectionString = connection.ConnectionString;
                        dbConnection.Open();
                    }
                }
                catch (Exception e)
                {
                    SetResult(TestResult.CreateFailed(GetName(), e.Message));
                    return;
                }
            }

            SetResult(TestResult.CreateSuccess(GetName()));
        }

        static IEnumerable<ConnectionStringSettings> GetConnections()
        {
            var predicate = PredicateBuilder.True<ConnectionStringSettings>();
            predicate = predicate.And(c => !string.IsNullOrWhiteSpace(c.ConnectionString));

            return ConfigurationManager.ConnectionStrings.OfType<ConnectionStringSettings>().Where(predicate.Compile()).ToList();
        }
    }
}