using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SQLite;

namespace FileTaggerRepository
{
    public abstract class RepositoryBase<T> : IRepository<T> where T : class
    {
        private static string ConnectionString 
        {
            get 
            { 
                return ConfigurationManager.AppSettings["SqliteConnectionString"]; 
            }
        }

        protected abstract string AddQuery { get; }
        protected abstract void AddCommandBuilder(SQLiteCommand cmd, T entity);
        public void Add(T entity)
        {
            ExecuteNonQuery(AddQuery, AddCommandBuilder, entity);
        }

        protected abstract string UpdateQuery { get; }
        protected abstract void UpdateCommandBuilder(SQLiteCommand cmd, T entity);
        public void Update(T entity)
        {
            ExecuteNonQuery(UpdateQuery, UpdateCommandBuilder, entity);
        }

        protected abstract string DeleteQuery { get; }
        protected abstract void DeleteCommandBuilder(SQLiteCommand cmd, T entity);
        public void Delete(T entity)
        {
            ExecuteNonQuery(DeleteQuery, DeleteCommandBuilder, entity);
        }

        private void ExecuteNonQuery(string query, Action<SQLiteCommand, T> commandBuilder, T entity) 
        {
            using (SQLiteConnection conn = new SQLiteConnection(ConnectionString))
            {
                using (SQLiteCommand cmd = new SQLiteCommand(query, conn))
                {
                    commandBuilder(cmd, entity);

                    conn.Open();
                    cmd.ExecuteNonQuery();
                }
            }
        }

        protected abstract string GetByIdQuery { get; }
        protected abstract void GetByIdCommandBuilder(SQLiteCommand cmd, int id);
        public T GetById(int id)
        {
            using (SQLiteConnection conn = new SQLiteConnection(ConnectionString))
            {
                using (SQLiteCommand cmd = new SQLiteCommand(GetByIdQuery, conn))
                {
                    GetByIdCommandBuilder(cmd, id);

                    conn.Open();
                    using (SQLiteDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            return Parse(dr);
                        }
                    }
                }
            }

            return default(T);
        }

        protected abstract string GetAllQuery { get; }
        public IEnumerable<T> GetAll()
        {
            using (SQLiteConnection conn = new SQLiteConnection(ConnectionString))
            {
                using (SQLiteCommand cmd = new SQLiteCommand(GetAllQuery, conn))
                {
                    conn.Open();
                    using (SQLiteDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            yield return Parse(dr);
                        }
                    }
                }
            }
        }

        protected abstract T Parse(SQLiteDataReader dr);
    }
}
