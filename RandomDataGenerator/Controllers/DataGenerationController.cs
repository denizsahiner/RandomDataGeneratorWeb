using DataGeneratorLibrary.Generators;
using Microsoft.AspNetCore.Mvc;

namespace RandomDataGenerator.Controllers
{
    public class FieldInput
    {
        public required string Name { get; set; }  
        public required string Type { get; set; }  
    }

    public class DataGenerationController(IConfiguration configuration) : Controller
    {
#pragma warning disable CS8601 // Possible null reference assignment.
        private readonly string _connectionString = configuration.GetConnectionString("DefaultConnection");
#pragma warning restore CS8601 // Possible null reference assignment.

        [HttpPost]
        public IActionResult GenerateData([FromBody] List<FieldInput> fields)
        {
            var generatedData = new List<object>();

            foreach (var field in fields)
            {
                IGenerator generator = field.Type.ToLower() switch
                {
                    "String" => new StringGenerator(),
                    "Number" => new NumberGenerator(),
                    "Date" => new DateGenerator(),
                    "Boolean" => new BooleanGenerator(),
                    // Veritabanından rastgele veri çekme
                    "firstName" => new DatabaseValueGenerator(_connectionString, "firstName"),
                    "lastName" => new DatabaseValueGenerator(_connectionString, "lastName"),
                    "ssn" => new DatabaseValueGenerator(_connectionString, "ssn"),
                    // Diğer veri türleri eklenebilir
                    _ => throw new ArgumentException("Invalid field type")
                };

                generatedData.Add(generator.GenerateRandomValue());
            }

            return Json(generatedData);  // JSON olarak döndür
        }
    }

}
