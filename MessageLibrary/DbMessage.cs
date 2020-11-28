using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MessageLibrary
{
    public static class DbMessage
    {
        public static string invDbNameERROR = "Database name cannot be empty";
        public static string invTableNameERROR = "Tablename cannot be empty";
        public static string invFileListERROR = "File list is empty!";


        public static string CreateTable(string tableName)
        {
            return $"{tableName} table has been created";
        }

        public static string CreateUser(string userName)
        {
            return $"User {userName} added successfully";
        }
    }
}
