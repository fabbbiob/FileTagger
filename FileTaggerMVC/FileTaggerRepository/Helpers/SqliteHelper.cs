using FileTaggerModel;
using System;
using System.Configuration;
using System.Data.SQLite;

namespace FileTaggerRepository.Helpers
{
    internal class SqliteHelper
    {
        private static string ConnectionString => ConfigurationManager.AppSettings["SqliteConnectionString"];

        internal static void Insert(string query,
                                    Action<SQLiteCommand, IEntity> commandBinder,
                                    IEntity entity)
        {
            ExecuteQuery(query, commandBinder, entity, cmd => { entity.Id = (int)(long)cmd.ExecuteScalar(); });
        }

        internal static void Delete(string query,
                                  Action<SQLiteCommand, IEntity> commandBinder,
                                  IEntity entity)
        {
            ExecuteQuery(query, commandBinder, entity, cmd => cmd.ExecuteNonQuery());
        }

        internal static void GetAll(string query, Action<SQLiteDataReader> action)
        {
            ExecuteQuery(query, null, null, cmd => {
                SQLiteDataReader dr = null;
                try
                {
                    dr = cmd.ExecuteReader();
                    action(dr);
                }
                finally
                {
                    if (dr != null)
                    {
                        dr.Close();
                        dr.Dispose();
                    }
                }
            });
        }

        internal static void GetById(string query,
                                     Action<SQLiteCommand, IEntity> commandBinder,
                                     IEntity entity, 
                                     Action<SQLiteDataReader> action)
        {
            ExecuteQuery(query, commandBinder, entity, cmd =>
            {
                SQLiteDataReader dr = null;
                try
                {
                    dr = cmd.ExecuteReader();
                    action(dr);
                }
                finally
                { 
                    if (dr != null)
                    { 
                        dr.Close();
                        dr.Dispose();
                    }
                }
            });
        }

        internal static void Update(string query, Action<SQLiteCommand, IEntity> commandBinder, IEntity entity)
        {
            ExecuteQuery(query, commandBinder, entity, cmd => cmd.ExecuteNonQuery());
        }

        private static void ExecuteQuery(string query,
                                         Action<SQLiteCommand, IEntity> commandBinder,
                                         IEntity entity,
                                         Action<SQLiteCommand> execute)
        {
            SQLiteConnection conn = null;
            SQLiteCommand cmd = null;
            try
            {
                conn = new SQLiteConnection(ConnectionString);
                cmd = new SQLiteCommand(query, conn);
                commandBinder?.Invoke(cmd, entity);
                conn.Open();
                execute(cmd);
            }
            finally
            {
                cmd?.Dispose();
                if (conn != null)
                {
                    conn.Close();
                    conn.Dispose();
                }
                SQLiteConnection.ClearAllPools();
            }
        }
                
    }
}
