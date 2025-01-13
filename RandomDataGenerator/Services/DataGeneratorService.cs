using DataGeneratorLibrary.Generators;
using RandomDataGenerator.Models;


namespace RandomDataGenerator.Services
{
    public interface IDataGeneratorService
    {
        List<object> GenerateData(List<Field> fields);
    }

    // Service implementation for generating random data
    public class DataGeneratorService : IDataGeneratorService
    {
        private readonly IConfiguration _configuration;

        // Retrieving the connection string using the IConfiguration in the constructor.
        public DataGeneratorService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public List<object> GenerateData(List<Field> fields)
        {
            var generatedData = new List<object>();

            try
            {
                foreach (var field in fields)
                {
                    if (field == null || string.IsNullOrEmpty(field.Type))
                    {
                        generatedData.Add(new { error = "Field type cannot be null or empty", field });
                        continue;
                    }

                    IGenerator generator;

                    // Check if the field type requires a database connection
                    if (AllowedColumns.Columns.Contains(field.Type))
                    {
                        var connectionString = _configuration.GetConnectionString("DefaultConnection");
                        if (string.IsNullOrEmpty(connectionString))
                        {
                            throw new InvalidOperationException("The connection string 'DefaultConnection' is not configured.");
                        }

                        generator = GeneratorFactory.CreateGenerator(field.Type, connectionString);
                    }
                    else
                    {
                        generator = GeneratorFactory.CreateGenerator(field.Type);
                    }

                    var generatedValue = generator.GenerateRandomValue();

                    generatedData.Add(new
                    {
                        field.Name,
                        field.Type,
                        GeneratedValue = generatedValue
                    });
                }
            }
            catch (Exception ex)
            {
                generatedData.Add(new { error = "An error occurred while generating data", message = ex.Message });
            }

            return generatedData;
        }

    }
}
