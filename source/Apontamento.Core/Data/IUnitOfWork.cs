namespace Apontamento.Core.Data
{
    public interface IUnitOfWork
    {
        Task<bool> Commit();
    }
}
