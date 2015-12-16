using DeadPool.Infrastructure.Interfaces;
using System.Linq;
using System.Configuration;
using System.Collections.Generic;
using System.Linq.Expressions;
using System;
using System.Data.Common;
using System.Threading.Tasks;
using DeadPool.Infrastructure.Enums;
using System.Collections.Concurrent;

namespace DeadPool.Infrastructure.Tests
{
    public class DatabaseTest : ITest
    {
        public string Description
        {
            get { return "Teste de Banco"; }
        }

        public string Name
        {
            get { return @"DatabaseTest"; }
        }

        private string[] _excludeConnections;

        public DatabaseTest()
        {
            _excludeConnections = null;
        }

        public DatabaseTest(params string[] excludeConnections)
        {
            _excludeConnections = excludeConnections;
        }

        public void Test(ITestContext context)
        {
            var connections = GetConnections();
            var failedTest = new ConcurrentBag<ConnectionStringSettings>();

            if (!connections.Any()) return;

            Parallel.ForEach(connections.ToList(), connectionSetting =>
                {
                    try
                    {
                        var factory = DbProviderFactories.GetFactory(connectionSetting.ProviderName);

                        using (var cnn = factory.CreateConnection())
                        {
                            cnn.ConnectionString = connectionSetting.ConnectionString;
                            cnn.Open();
                        }

                        context.RaiseEvent(EventType.Success, string.Format("Connection test ( {0} ) success", connectionSetting.Name)); //TODO: Incluir erro correto
                    }
                    catch (Exception ex)
                    {
                        context.RaiseEvent(EventType.Fail, ex.Message);
                        failedTest.Add(connectionSetting);
                    }
                });

            if (failedTest.Any())
            {
                var msg = string.Format("Connection test ( {0} ) failed", string.Join(", ", failedTest.Select(x => x.Name).ToArray()));
                throw new Exception(msg);
            }
        }

        private IEnumerable<ConnectionStringSettings> GetConnections()
        {
            var predicate = PredicateBuilder.True<ConnectionStringSettings>();

            if (_excludeConnections != null)
            {
                foreach (var exclude in _excludeConnections)
                    predicate = predicate.And(c => c.Name != exclude);
            }

            return ConfigurationManager.ConnectionStrings.OfType<ConnectionStringSettings>().Where(predicate.Compile()).ToList();
        }
    }

    public static class PredicateBuilder
    {
        public static Expression<Func<T, bool>> True<T>() { return f => true; }
        public static Expression<Func<T, bool>> False<T>() { return f => false; }

        public static Expression<Func<T, bool>> Or<T>(this Expression<Func<T, bool>> expr1, Expression<Func<T, bool>> expr2)
        {
            var invokedExpr = Expression.Invoke(expr2, expr1.Parameters.Cast<Expression>());
            return Expression.Lambda<Func<T, bool>>(Expression.OrElse(expr1.Body, invokedExpr), expr1.Parameters);
        }

        public static Expression<Func<T, bool>> And<T>(this Expression<Func<T, bool>> expr1, Expression<Func<T, bool>> expr2)
        {
            var invokedExpr = Expression.Invoke(expr2, expr1.Parameters.Cast<Expression>());
            return Expression.Lambda<Func<T, bool>>(Expression.AndAlso(expr1.Body, invokedExpr), expr1.Parameters);
        }
    }
}