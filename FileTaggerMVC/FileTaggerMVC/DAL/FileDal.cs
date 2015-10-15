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

        
        internal static IEnumerable<FileViewModel> GetAll() 
        {
            throw new NotImplementedException();
        }

        
        internal static void Create(FileViewModel file) 
        {
            throw new NotImplementedException();
        }

        
        private static FileViewModel Parse(SQLiteDataReader dr) 
        {
            throw new NotImplementedException();
        }

        
        internal static void Create(TagViewModel tag)
        {
            throw new NotImplementedException();
        }

       
        internal static FileViewModel Get(int id)
        {
            throw new NotImplementedException();
        }

        
        internal static FileViewModel Get(string fileName) 
        {
            //throw new NotImplementedException();
            return null;
        }

        
        internal static void Edit(TagViewModel tag)
        {
            throw new NotImplementedException();
        }

        
        internal static void Delete(int id)
        {
            throw new NotImplementedException();
        }
    }
}