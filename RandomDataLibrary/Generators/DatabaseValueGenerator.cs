using System;
using System.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using Microsoft.Data.Sqlite;

namespace DataGeneratorLibrary.Generators
{
    public static class AllowedColumns
    {
        public static readonly List<string> Columns = new()
    {
        "firstName",
        "middleName",
        "lastName",
        "gender",
        "ssn",
        "salary"
    };
    }

    public class DatabaseValueGenerator : IGenerator
    {
        private readonly string _connectionString;
        private readonly string _columnName;

       
        public DatabaseValueGenerator(string connectionString, string columnName)
        {
            if (!AllowedColumns.Columns.Contains(columnName))
            {
                throw new ArgumentException("Invalid column name selected.");
            }

            _connectionString = connectionString;
            _columnName = columnName;
        }

        public string DataType => _columnName;
                
        public object GenerateRandomValue()
        {
            using (var connection = new SqliteConnection(_connectionString))
            {
                try
                {
                    connection.Open();

                    var query = $@"
                SELECT {_columnName}
                FROM DATA_TABLE
                ORDER BY RANDOM()
                LIMIT 1";

                    using (var command = new SqliteCommand(query, connection))
                    {
                        var result = command.ExecuteScalar(); 
                        return result ?? "Default Value";
                    }
                }
                catch (Exception ex)
                {                    
                    throw new Exception("Veritabanı bağlantısı sırasında hata oluştu.", ex);
                }
            }
        }
    }
}


