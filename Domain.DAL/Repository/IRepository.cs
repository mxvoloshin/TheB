namespace Domain.DAL.Repository
{
    public interface IRepository<TEntity, TKey>
        where TEntity : DomainEntity<TKey>
        where TKey : struct
    {
        TEntity Add(TEntity entity);
        void Remove(TEntity entity);
        void Update(TEntity entity);
    }
}