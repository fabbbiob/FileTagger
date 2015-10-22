using System.Data.SQLite;
using FileTaggerModel;

namespace FileTaggerRepository.Repositories.Abstract
{
    public abstract class RepositoryBaseWithReferences<T> 
        : RepositoryBase<T>, IRepositoryWithReferences<T> where T : class, IEntity
    {
        protected abstract string AddWithReferencesQuery(T entity);
        protected abstract void AddWithReferencesCommandBinder(SQLiteCommand cmd, T entity);
        public void AddWithReferences(T entity)
        {
            ExecuteQuery(AddWithReferencesQuery(entity), 
                         AddWithReferencesCommandBinder, 
                         entity, 
                         cmd => { entity.Id = (int)(long)cmd.ExecuteScalar();});
        }

        protected abstract string UpdateWithReferencesQuery(T entity);
        protected abstract void UpdateWithReferencesCommandBinder(SQLiteCommand cmd, T entity);
        public void UpdateWithReferences(T entity)
        {
            ExecuteQuery(UpdateWithReferencesQuery(entity),
                         UpdateWithReferencesCommandBinder,
                         entity,
                         cmd => cmd.ExecuteNonQuery() );
        }
    }
}
