using Microservices.Infra.Data;
using Microservices.Infra.Exceptions;
using Microsoft.EntityFrameworkCore.Storage;

namespace Microservices.Infra.UnityOfWok;
public class UnityOfWork : IUnityOfWork, IDisposable
{
    private AppDbContext _db { get; }
    private IDbContextTransaction _transaction;
    //private IRepository<Entity> EntityRepository { get; }

    public UnityOfWork(AppDbContext db)
    {
        _db = db;
    }

    //public IRepository<Entity> EntityRepository
    //{
    //    get
    //    {
    //        if(EntityRepository == null) EntityRepository = new Repository<Entity>(db);
    //        return EntityRepository;
    //    }
    //}

    public async Task BeginTransactionAsync() => _transaction = await _db.Database.BeginTransactionAsync();
    
    public async Task CommitTransactionAsync()
    {
        try
        {
            await _db.SaveChangesAsync();
            await _transaction.CommitAsync();
        }
        catch(Exception e) 
        {
            await _transaction.RollbackAsync();
            throw new UnityOfWorkException(e.Message);
        }
        finally
        {
            _transaction.Dispose();
        }
    }

    public async Task RollbackTransactionAsync()
    {
        await _transaction.RollbackAsync();
        _transaction.Dispose();
    }

    private bool disposed = false;
    protected virtual void Dispose(bool disposing)
    {
        if (!this.disposed)
        {
            if (disposing)
            {
                _db.Dispose();
            }
        }
        this.disposed = true;
    }
    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }
    public async Task Commit() => await _db.SaveChangesAsync();
}
