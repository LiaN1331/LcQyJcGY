// 代码生成时间: 2025-10-04 02:08:21
using System;
using System.Collections.Generic;
using System.Linq;

namespace AMLSystem
{
    /// <summary>
    /// Represents a simple Anti-Money Laundering (AML) system.
    /// </summary>
    public class AMLSystem
    {
        private readonly List<Transaction> transactions;

        /// <summary>
        /// Initializes a new instance of the AMLSystem class.
        /// </summary>
        public AMLSystem()
        {
            transactions = new List<Transaction>();
        }

        /// <summary>
        /// Adds a transaction to the system for processing.
        /// </summary>
        /// <param name="transaction">The transaction to add.</param>
        public void AddTransaction(Transaction transaction)
        {
            if (transaction == null)
            {
                throw new ArgumentNullException(nameof(transaction), "Transaction cannot be null.");
            }

            transactions.Add(transaction);
        }

        /// <summary>
        /// Processes all transactions for AML compliance.
        /// </summary>
        /// <returns>A list of transactions flagged as suspicious.</returns>
        public List<Transaction> ProcessTransactions()
        {
            List<Transaction> suspiciousTransactions = new List<Transaction>();

            foreach (Transaction transaction in transactions)
            {
                if (transaction.Amount > 10000)
                {
                    // Flag the transaction as suspicious due to high amount.
                    transaction.Status = TransactionStatus.Suspect;
                    suspiciousTransactions.Add(transaction);
                }
                else if (transaction.Currency != "USD")
                {
                    // Flag the transaction as suspicious due to non-USD currency.
                    transaction.Status = TransactionStatus.Suspect;
                    suspiciousTransactions.Add(transaction);
                }
            }

            return suspiciousTransactions;
        }

        /// <summary>
        /// Clears all transactions from the system.
        /// </summary>
        public void ClearTransactions()
        {
            transactions.Clear();
        }
    }

    /// <summary>
    /// Represents a financial transaction.
    /// </summary>
    public class Transaction
    {
        public decimal Amount { get; set; }
        public string Currency { get; set; }
        public TransactionStatus Status { get; set; }
    }

    /// <summary>
    /// Represents the status of a transaction.
    /// </summary>
    public enum TransactionStatus
    {
        Normal,
        Suspect
    }
}
