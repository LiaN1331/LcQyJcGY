// 代码生成时间: 2025-09-23 10:20:41
using System;
using System.Collections.Concurrent;
using System.Data;
using System.Data.Common;

namespace ConnectionPoolDemo
{
    /// <summary>
    /// A simple database connection pool manager to manage database connections.
    /// </summary>
    public class DatabaseConnectionPoolManager
    {
        private readonly ConcurrentBag<DbConnection> _connectionPool;
        private readonly string _connectionString;
        private readonly int _maxConnections;
        private readonly Type _dbProviderType;

        /// <summary>
        /// Initializes a new instance of the <see cref="DatabaseConnectionPoolManager"/> class.
        /// </summary>
        /// <param name="connectionString">The database connection string.</param>
        /// <param name="maxConnections">The maximum number of connections in the pool.</param>
        /// <param name="dbProviderType">The type of the database provider.</param>
        public DatabaseConnectionPoolManager(string connectionString, int maxConnections, Type dbProviderType)
        {
            _connectionPool = new ConcurrentBag<DbConnection>();
            _connectionString = connectionString;
            _maxConnections = maxConnections;
            _dbProviderType = dbProviderType;

            // Initialize the pool with a specified number of connections.
            for (int i = 0; i < maxConnections; i++)
            {
                var connection = CreateConnection();
                if (connection != null)
                {
                    _connectionPool.Add(connection);
                }
            }
        }

        /// <summary>
        /// Acquires a database connection from the pool.
        /// </summary>
        /// <returns>A database connection.</returns>
        public DbConnection AcquireConnection()
        {
            if (_connectionPool.TryTake(out var connection))
            {
                if (connection.State != ConnectionState.Open)
                {
                    connection.Open();
                }
                return connection;
            }
            else
            {
                // If pool is empty, create a new connection.
                return CreateConnection();
            }
        }

        /// <summary>
        /// Releases a database connection back to the pool.
        /// </summary>
        /// <param name="connection">The database connection to release.</param>
        public void ReleaseConnection(DbConnection connection)
        {
            if (connection != null)
            {
                try
                {
                    connection.Close();
                    _connectionPool.Add(connection);
                }
                catch (Exception ex)
                {
                    // Handle the exception, e.g., log it or throw a custom error.
                    Console.WriteLine($"Error releasing connection: {ex.Message}");
                }
            }
        }

        private DbConnection CreateConnection()
        {
            try
            {
                DbConnection connection = (DbConnection)Activator.CreateInstance(_dbProviderType, _connectionString);
                connection.StateChange += (sender, args) =>
                {
                    if (args.CurrentState == ConnectionState.Closed)
                    {
                        // Handle closed connection scenario, if necessary.
                    }
                };
                return connection;
            }
            catch (Exception ex)
            {
                // Handle the exception, e.g., log it or throw a custom error.
                Console.WriteLine($"Error creating connection: {ex.Message}");
                return null;
            }
        }
    }
}
