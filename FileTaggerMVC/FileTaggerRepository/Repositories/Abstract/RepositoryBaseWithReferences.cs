using System.Data.SQLite;
using FileTaggerModel;

namespace FileTaggerRepository.Repositories.Abstract
{
    public abstract class RepositoryBaseWithReferences<T> 
        : RepositoryBase<T>, IRepositoryWithReferences<T> where T : class, IEntity
    {
        protected abstract string AddWithReferencesQuery(T entity);
        protected abstract void AddWithReferencesCommandBuilder(SQLiteCommand cmd, T entity);
        public void AddWithReferences(T entity)
        {
            //TODO Id
            entity.Id = ExecuteQuery(AddWithReferencesQuery(entity), AddWithReferencesCommandBuilder, entity);
        }
    }
}
