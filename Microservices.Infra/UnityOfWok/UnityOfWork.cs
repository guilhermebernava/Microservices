using Microservices.Domain.Entities;
using Microservices.Domain.Repositories;
using Microservices.Infra.Data;
using Microservices.Infra.Exceptions;
using Microservices.Infra.Repositories;
using Microsoft.EntityFrameworkCore.Storage;

namespace Microservices.Infra.UnityOfWok;
public class UnityOfWork : IUnityOfWork, IDisposable
{
    private AppDbContext _db { get; }
    private IDbContextTransaction _transaction;
    private IRepository<Recomendation> _recomendationRepository;

    public UnityOfWork(AppDbContext db)
    {
        _db = db;
    }

    public IRepository<Recomendation> RecomendationRepository
    {
        get
        {
            if (_recomendationRepository == null)
                _recomendationRepository = new Repository<Recomendation>(_db);
            return _recomendationRepository;
        }
        set
        {
            _recomendationRepository = value;
        }
    }

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
