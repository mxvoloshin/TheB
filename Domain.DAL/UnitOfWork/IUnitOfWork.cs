namespace Domain.DAL.UnitOfWork
{
    public interface IUnitOfWork
    {
        void CommitTransaction();
    }
}