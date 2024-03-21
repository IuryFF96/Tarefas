using Dapper;
using System;
using System.Data.SQLite;
public abstract class BaseDAO
{
        public string DataSourceFile => Environment.CurrentDirectory + "\\TarefasDB.sqlite";
        public SQLiteConnection Connection => new SQLiteConnection("DataSource=" + DataSourceFile);
        public BaseDAO()
        {
            
        }
}