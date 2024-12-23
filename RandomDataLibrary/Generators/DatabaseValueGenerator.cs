using System;
using System.Data.SqlClient;
using Microsoft.Extensions.Configuration;
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

        // Constructor'da bağlantı dizesi ve kolon adı alır
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

        // Veritabanından rastgele veri çekme
        public object GenerateRandomValue()
        {
            using var connection = new SqlConnection(_connectionString);
            connection.Open();

            var query = $@"
            SELECT TOP 1 {_columnName}
            FROM DATA_TABLE
            ORDER BY NEWID()"; // Rastgele bir değer seç

            using var command = new SqlCommand(query, connection);
            return command.ExecuteScalar(); // Kolondan bir değeri döndür
        }
    }
}


