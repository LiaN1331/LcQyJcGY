// 代码生成时间: 2025-10-10 02:32:34
/// <summary>
/// Represents a mining pool manager for managing mining activities.
/// </summary>
using System;
using System.Collections.Generic;
using System.Linq;

namespace MiningApp
{
    /// <summary>
    /// Represents a mining pool with its properties and methods.
    /// </summary>
    public class MiningPool
    {
        public int PoolId { get; set; }
        public string Name { get; set; }
        public List<Worker> Workers { get; set; } = new List<Worker>();

        public MiningPool(int poolId, string name)
        {
            PoolId = poolId;
            Name = name;
        }

        /// <summary>
        /// Adds a worker to the mining pool.
        /// </summary>
        /// <param name="worker">The worker to add.</param>
        public void AddWorker(Worker worker)
        {
            if (worker == null)
                throw new ArgumentNullException(nameof(worker), "Worker cannot be null.");

            Workers.Add(worker);
        }

        /// <summary>
        /// Removes a worker from the mining pool.
        /// </summary>
        /// <param name="worker">The worker to remove.</param>
        public void RemoveWorker(Worker worker)
        {
            if (worker == null)
                throw new ArgumentNullException(nameof(worker), "Worker cannot be null.");

            Workers.Remove(worker);
        }
    }

    /// <summary>
    /// Represents a worker in the mining pool with its properties.
    /// </summary>
    public class Worker
    {
        public int WorkerId { get; set; }
        public string Name { get; set; }
        public string Hashrate { get; set; }
        public DateTime StartTime { get; set; }

        public Worker(int workerId, string name, string hashrate)
        {
            WorkerId = workerId;
            Name = name;
            Hashrate = hashrate;
            StartTime = DateTime.UtcNow;
        }
    }

    /// <summary>
    /// Provides functionality to manage mining pools.
    /// </summary>
    public class MiningPoolManager
    {
        private readonly Dictionary<int, MiningPool> _pools = new Dictionary<int, MiningPool>();

        /// <summary>
        /// Creates a new mining pool.
        /// </summary>
        /// <param name="poolId">The ID of the pool.</param>
        /// <param name="name">The name of the pool.</param>
        /// <returns>The created mining pool.</returns>
        public MiningPool CreatePool(int poolId, string name)
        {
            if (string.IsNullOrEmpty(name))
                throw new ArgumentException("Pool name cannot be null or empty.", nameof(name));

            if (_pools.ContainsKey(poolId))
                throw new InvalidOperationException("There is already a pool with the same ID.");

            var pool = new MiningPool(poolId, name);
            _pools.Add(poolId, pool);
            return pool;
        }

        /// <summary>
        /// Retrieves a mining pool by its ID.
        /// </summary>
        /// <param name="poolId">The ID of the pool to retrieve.</param>
        /// <returns>The mining pool if found; otherwise, null.</returns>
        public MiningPool GetPool(int poolId)
        {
            if (_pools.TryGetValue(poolId, out var pool))
                return pool;

            return null;
        }

        /// <summary>
        /// Removes a mining pool by its ID.
        /// </summary>
        /// <param name="poolId">The ID of the pool to remove.</param>
        public void RemovePool(int poolId)
        {
            if (!_pools.ContainsKey(poolId))
                throw new KeyNotFoundException("There is no pool with the given ID.");

            _pools.Remove(poolId);
        }
    }
}
