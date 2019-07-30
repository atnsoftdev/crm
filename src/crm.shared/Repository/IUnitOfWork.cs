using System.Data;

namespace CRM.Shared.Repository
{
    public interface IUnitOfWork
    {
        IDbConnection Connection { get; }        
        void BeginTransaction(IsolationLevel level = IsolationLevel.ReadCommitted);
        void CommitTransaction();
        void RollbackTransaction();
    }
}