namespace Microservices.Infra.UnityOfWok;
public interface IUnityOfWork
{
    //IRepository<Entity> EntityRepository { get; }

    Task Commit();
    Task CommitTransactionAsync();
    Task RollbackTransactionAsync();
}
