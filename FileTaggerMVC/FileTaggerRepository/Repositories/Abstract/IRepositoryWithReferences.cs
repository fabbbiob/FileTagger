namespace FileTaggerRepository.Repositories.Abstract
{
    interface IRepositoryWithReferences<T> : IRepository<T> where T : class
    {
        void AddWithReferences(T entity);
    }
}
