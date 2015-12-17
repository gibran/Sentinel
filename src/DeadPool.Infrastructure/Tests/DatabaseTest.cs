using DeadPool.Infrastructure.Enums;
using DeadPool.Infrastructure.Interfaces;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Common;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace DeadPool.Infrastructure.Tests
{
    public class DatabaseTest : ITest
    {
        public string Description => "Teste de Banco";
        public string Name => @"DatabaseTest";

        readonly string[] _excludeConnections;

        public DatabaseTest(params string[] excludeConnections)
        {
            _excludeConnections = excludeConnections;
        }

        public async Task<bool> Test(ITestContext context)
        {
            var connections = GetConnections().ToList();
            var failedTest = new ConcurrentBag<ConnectionStringSettings>();

            if (!connections.Any()) return await Task.FromResult(false);

            Parallel.ForEach(connections.ToList(), async connectionSetting =>
                {
                    try
                    {
                        var factory = DbProviderFactories.GetFactory(connectionSetting.ProviderName);
                        using (var cnn = factory.CreateConnection())
                        {
                            if (cnn == null)
                                throw new InvalidOperationException("Connection cannot be null.");

                            cnn.ConnectionString = connectionSetting.ConnectionString;
                            await cnn.OpenAsync();
                        }

                        context.RaiseEvent(EventType.Success, $"Connection test ( {connectionSetting.Name} ) success"); //TODO: Incluir erro correto
                    }
                    catch (Exception ex)
                    {
                        context.RaiseEvent(EventType.Fail, ex.Message);
                        failedTest.Add(connectionSetting);
                    }
                });

            if (!failedTest.Any()) return await Task.FromResult(true);

            var msg = $"Connection test ( {string.Join(", ", failedTest.Select(x => x.Name).ToArray())} ) failed";
            throw new Exception(msg);
        }

        IEnumerable<ConnectionStringSettings> GetConnections()
        {
            var predicate = PredicateBuilder.True<ConnectionStringSettings>();

            if (_excludeConnections != null)
            {
                foreach (var exclude in _excludeConnections)
                    predicate = predicate.And(c => c.Name != exclude);
            }

            predicate = predicate.And(c => !string.IsNullOrWhiteSpace(c.ConnectionString));

            return ConfigurationManager.ConnectionStrings.OfType<ConnectionStringSettings>().Where(predicate.Compile()).ToList();
        }
    }

    public static class PredicateBuilder
    {
        public static Expression<Func<T, bool>> True<T>()
        {
            return f => true;
        }

        public static Expression<Func<T, bool>> False<T>()
        {
            return f => false;
        }

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