using Apontamento.Core.DomainObjects;

namespace Apontamento.Core.Data
{
    public interface IRepository<TRepository> : IDisposable where TRepository : IAggregateRoot
    {
        IUnitOfWork UnitOfWork { get; }
    }
}
