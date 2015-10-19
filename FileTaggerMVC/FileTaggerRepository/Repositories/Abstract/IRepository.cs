using System.Collections.Generic;

namespace FileTaggerRepository.Repositories.Abstract
{
    interface IRepository<T> where T : class
    {
        void Add(T entity);
        void Update(T entity);
        void Delete(T entity);
        T GetById(int id);
        T GetByIdWithReferences(int id);
        IEnumerable<T> Get(string prop, string whereClause);
        IEnumerable<T> GetAll();
    }
}
