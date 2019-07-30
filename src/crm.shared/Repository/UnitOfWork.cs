using System;
using System.Data;

namespace CRM.Shared.Repository
{
    public class UnitOfWork : IUnitOfWork, IDisposable
    {
        private readonly Func<IDbConnection> _connFactory;
        private IDbConnection _dbConnection;
        private IDbTransaction _transaction;

        public UnitOfWork(Func<IDbConnection> connFactory)
        {
            _connFactory = connFactory;
        }

        public IDbConnection Connection
        {
            get
            {
                if (_dbConnection == null)
                {
                    _dbConnection = _connFactory();
                }
                return _dbConnection;
            }
        }

        public void BeginTransaction(IsolationLevel level = IsolationLevel.ReadCommitted)
        {
            _transaction = _dbConnection.BeginTransaction(level);
        }

        public void CommitTransaction()
        {
            _transaction.Commit();
        }

        public void RollbackTransaction()
        {
            _transaction.Rollback();
        }

        public void Dispose()
        {
            if (_transaction != null)
            {
                _transaction.Dispose();
            }
            _transaction = null;
            if (_dbConnection != null)
            {
                _dbConnection.Close();
                _dbConnection.Dispose();
            }
            _dbConnection = null;
        }
    }
}