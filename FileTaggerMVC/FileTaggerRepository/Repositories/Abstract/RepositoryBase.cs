using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SQLite;
using FileTaggerModel;
using System.Data;
using System.Text.RegularExpressions;

namespace FileTaggerRepository.Repositories.Abstract
{
    public abstract class RepositoryBase<T> : IRepository<T> where T : class, IEntity
    {
        private static string ConnectionString => ConfigurationManager.AppSettings["SqliteConnectionString"];

        protected abstract string AddQuery { get; }
        protected abstract void AddCommandBinder(SQLiteCommand cmd, T entity);
        public virtual void Add(T entity)
        {
            ExecuteQuery(AddQuery, AddCommandBinder, entity, cmd => { entity.Id = (int) (long) cmd.ExecuteScalar(); });
        }

        protected abstract string UpdateQuery { get; }
        protected abstract void UpdateCommandBinder(SQLiteCommand cmd, T entity);
        public void Update(T entity)
        {
            ExecuteQuery(UpdateQuery, UpdateCommandBinder, entity, cmd => cmd.ExecuteNonQuery());
        }

        protected abstract string DeleteQuery { get; }
        protected abstract void DeleteCommandBinder(SQLiteCommand cmd, T entity);
        public void Delete(T entity)
        {
            ExecuteQuery(DeleteQuery, DeleteCommandBinder, entity, cmd => cmd.ExecuteNonQuery());
        }

        protected void ExecuteQuery(string query, 
                                    Action<SQLiteCommand, T> commandBinder, 
                                    T entity, 
                                    Action<SQLiteCommand> execute) 
        {
            SQLiteConnection conn = null;
            SQLiteCommand cmd = null;
            try
            {
                conn = new SQLiteConnection(ConnectionString);
                cmd = new SQLiteCommand(query, conn);

                commandBinder(cmd, entity);
                conn.Open();
                execute(cmd);
            }
            finally
            {
                cmd?.Dispose();

                conn?.Close();
                conn?.Dispose();

                SQLiteConnection.ClearAllPools();
            }
        }

        protected abstract string GetByIdQuery { get; }
        protected abstract void GetByIdCommandBinder(SQLiteCommand cmd, int id);
        public T GetById(int id)
        {
            SQLiteConnection conn = null;
            SQLiteCommand cmd = null;
            SQLiteDataReader dr = null;
            try
            {
                conn = new SQLiteConnection(ConnectionString);
                cmd = new SQLiteCommand(GetByIdQuery, conn);

                GetByIdCommandBinder(cmd, id);

                conn.Open();
                dr = cmd.ExecuteReader();
                
                while (dr.Read())
                {
                    return Parse(dr);
                }
            }
            finally
            {
                dr?.Close();
                dr?.Dispose();

                cmd?.Dispose();

                conn?.Close();
                conn?.Dispose();

                SQLiteConnection.ClearAllPools();
            }

            return default(T);
        }

        protected abstract string GetByIdWithReferencesQuery { get; }

        public T GetByIdWithReferences(int id)
        {
            SQLiteConnection conn = null;
            SQLiteCommand cmd = null;
            SQLiteDataReader dr = null;
            try
            {
                conn = new SQLiteConnection(ConnectionString);
                cmd = new SQLiteCommand(GetByIdWithReferencesQuery, conn);

                GetByIdCommandBinder(cmd, id);

                conn.Open();
                dr = cmd.ExecuteReader();

                return ParseWithReferences(dr);
            }
            finally
            {
                dr?.Close();
                dr?.Dispose();

                cmd?.Dispose();

                conn?.Close();
                conn?.Dispose();

                SQLiteConnection.ClearAllPools();
            }
        }

        //TODO refactor
        protected virtual string GetByWhereQuery { get; }
        public IEnumerable<T> Get(string prop, string whereClause)
        {
            SQLiteConnection conn = null;
            SQLiteCommand cmd = null;
            SQLiteDataReader dr = null;
            try
            {
                conn = new SQLiteConnection(ConnectionString);

                string query = GetByWhereQuery;
                query = query.Replace("@Prop", prop);
                query = query.Replace("@Where", whereClause);
                cmd = new SQLiteCommand(query, conn);

                //cmd = new SQLiteCommand(GetByWhereQuery, conn);

                //cmd.Parameters.Add("@Prop", DbType.String).Value = prop;
                //cmd.Parameters.Add("@Where", DbType.String).Value = whereClause;

                conn.Open();
                dr = cmd.ExecuteReader();

                //while (dr.Read())
                //{
                    yield return ParseWithReferences(dr);
                //}
            }
            finally
            {
                dr?.Close();
                dr?.Dispose();

                cmd?.Dispose();

                conn?.Close();
                conn?.Dispose();

                SQLiteConnection.ClearAllPools();
            }
        }

        protected abstract string GetAllQuery { get; }
        public IEnumerable<T> GetAll()
        {
            SQLiteConnection conn = null;
            SQLiteCommand cmd = null;
            SQLiteDataReader dr = null;
            try
            {
                conn = new SQLiteConnection(ConnectionString);
                cmd = new SQLiteCommand(GetAllQuery, conn);
                
                conn.Open();
                dr = cmd.ExecuteReader();

                while (dr.Read())
                {
                    yield return Parse(dr);
                }
            }
            finally
            {
                dr?.Close();
                dr?.Dispose();

                cmd?.Dispose();
                
                conn?.Close();
                conn?.Dispose();

                SQLiteConnection.ClearAllPools();
            }
        }

        protected abstract T Parse(SQLiteDataReader dr);
        protected abstract T ParseWithReferences(SQLiteDataReader dr);
    }
}
