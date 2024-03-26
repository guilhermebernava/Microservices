using Microservices.Domain.Entities;
using Microservices.Domain.Repositories;

namespace Microservices.Infra.UnityOfWok;
public interface IUnityOfWork
{
    IRepository<Recomendation> RecomendationRepository { get; set; }

    Task Commit();
    Task CommitTransactionAsync();
    Task RollbackTransactionAsync();
}
