using FileTaggerMVC.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.Web.Configuration;

namespace FileTaggerMVC.DAL
{
    internal class FileDal
    {
        private readonly static string ConnectionString = WebConfigurationManager.AppSettings["SqliteConnectionString"];

        // TODO
        internal static IEnumerable<File> GetAll() 
        {
            throw new NotImplementedException();
        }

        // TODO
        internal static void Create(File file) 
        {
            throw new NotImplementedException();
        }

        // TODO
        private static File Parse(SQLiteDataReader dr) 
        {
            throw new NotImplementedException();
        }

        // TODO
        internal static void Create(Tag tag)
        {
            throw new NotImplementedException();
        }

        // TODO
        internal static File Get(int id)
        {
            throw new NotImplementedException();
        }

        // TODO
        internal static File Get(string fileName) 
        {
            //throw new NotImplementedException();
            return null;
        }

        // TODO
        internal static void Edit(Tag tag)
        {
            throw new NotImplementedException();
        }

        // TODO
        internal static void Delete(int id)
        {
            throw new NotImplementedException();
        }
    }
}