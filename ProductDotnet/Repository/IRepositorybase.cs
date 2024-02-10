namespace ProductDotnet.Repository
{
    public interface IRepositoryBase<TEntity>
    {
        Task<IQueryable<TEntity>> FindAll(bool trackChanges);

        Task<TEntity> FindById(int id, bool trackChanges);

        void Create(TEntity entity);

        void Update(TEntity entity);

        void Delete(TEntity entity);

        void Save();
    }
}
