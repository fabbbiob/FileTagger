using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SQLite;
using FileTaggerModel;

namespace FileTaggerRepository.Repositories.Abstract
{
    public abstract class RepositoryBase<T> : IRepository<T> where T : class, IEntity
    {
        private static string ConnectionString => ConfigurationManager.AppSettings["SqliteConnectionString"];

        protected abstract string AddQuery { get; }
        protected abstract void AddCommandBuilder(SQLiteCommand cmd, T entity);
        public virtual void Add(T entity)
        {
            entity.Id = ExecuteQuery(AddQuery, AddCommandBuilder, entity);
        }

        protected abstract string UpdateQuery { get; }
        protected abstract void UpdateCommandBuilder(SQLiteCommand cmd, T entity);
        public void Update(T entity)
        {
            ExecuteQuery(UpdateQuery, UpdateCommandBuilder, entity);
        }

        protected abstract string DeleteQuery { get; }
        protected abstract void DeleteCommandBuilder(SQLiteCommand cmd, T entity);
        public void Delete(T entity)
        {
            ExecuteQuery(DeleteQuery, DeleteCommandBuilder, entity);
        }

        protected int ExecuteQuery(string query, Action<SQLiteCommand, T> commandBuilder, T entity) 
        {
            SQLiteConnection conn = null;
            SQLiteCommand cmd = null;
            try
            {
                conn = new SQLiteConnection(ConnectionString);
                cmd = new SQLiteCommand(query, conn);

                commandBuilder(cmd, entity);
                conn.Open();
                cmd.ExecuteNonQuery();
                return (int)conn.LastInsertRowId;
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
        protected abstract void GetByIdCommandBuilder(SQLiteCommand cmd, int id);
        public T GetById(int id)
        {
            SQLiteConnection conn = null;
            SQLiteCommand cmd = null;
            SQLiteDataReader dr = null;
            try
            {
                conn = new SQLiteConnection(ConnectionString);
                cmd = new SQLiteCommand(GetByIdQuery, conn);

                GetByIdCommandBuilder(cmd, id);

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

                GetByIdCommandBuilder(cmd, id);

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

            return default(T);
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
