using Sentinel.Core;
using System;
using System.Configuration;
using System.Data.Common;
using System.Linq;

namespace Sentinel.Tests.Database
{
    public class DatabaseTest : SentinelTestBase
    {
        private readonly string _connectionName;

        public DatabaseTest(string name, string description, string connectionName) : base(name, description)
        {
            if (string.IsNullOrWhiteSpace(connectionName))
                throw new ArgumentNullException(nameof(connectionName));

            _connectionName = connectionName;
        }

        protected override TestResult RunTest()
        {
            var connection = GetConnection();

            if (ReferenceEquals(connection, null))
                return TestResult.CreateFailed(GetName(), "No connections found.");

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
                return TestResult.CreateFailed(GetName(), e.Message);
            }

            return TestResult.CreateSuccess(GetName());
        }

        private ConnectionStringSettings GetConnection()
        {
            var predicate = PredicateBuilder.True<ConnectionStringSettings>();
            predicate = predicate.And(c => c.Name == _connectionName);

            return ConfigurationManager.ConnectionStrings.OfType<ConnectionStringSettings>().Where(predicate.Compile()).FirstOrDefault();
        }
    }
}