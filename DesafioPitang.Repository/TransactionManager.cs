using DesafioPitang.Repository.Interface;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace DesafioPitang.Repository
{
    public class TransactionManager : ITransactionManager
    {
        private readonly Context _context;
        public TransactionManager(Context context) 
        {
            _context = context;
        }
        public async Task BeginTransactionAsync(IsolationLevel isolationLevel)
        {
            if (_context.Database.CurrentTransaction == null)
            {
                var connection = _context.Database.GetDbConnection();
                if (connection.State == ConnectionState.Closed)
                {
                    connection.Open();
                }
                var transaction = await connection.BeginTransactionAsync(isolationLevel);
                await _context.Database.UseTransactionAsync(transaction);
            }
        }

        public async Task CommitTransactionsAsync()
        {
            if (_context.ChangeTracker.HasChanges())
                await _context.SaveChangesAsync();

            var activeTransaction = _context.Database.CurrentTransaction;
            await activeTransaction.CommitAsync();
            await activeTransaction.DisposeAsync();
        }

        public async Task RollbackTransactionsAsync()
        {
            var activeTransaction = _context.Database.CurrentTransaction;
            if (activeTransaction != null)
            {
                await activeTransaction.RollbackAsync();
                await activeTransaction.DisposeAsync();
            }
        }
    }
}
