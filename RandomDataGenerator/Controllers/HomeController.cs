using DataGeneratorLibrary.Generators;
using Microsoft.AspNetCore.Mvc;
using RandomDataGenerator.Models;


namespace RandomDataGenerator.Controllers
{
    public class HomeController : Controller
    {
        private readonly IConfiguration _configuration;

        public HomeController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost("/Home/GenerateData")]
        public IActionResult GenerateData([FromBody] List<Field> fields, int count = 10)
        {
            var generatedDataList = new List<Dictionary<string, object>>();

            try
            {
                for (int i = 0; i < count; i++)
                {
                    var row = new Dictionary<string, object>();

                    foreach (var field in fields)
                    {
                        // Null kontrolü
                        if (string.IsNullOrEmpty(field.Name))
                        {
                            return BadRequest(new { message = "Field name cannot be null or empty." });
                        }

                        if (string.IsNullOrEmpty(field.Type))
                        {
                            return BadRequest(new { message = "Field type cannot be null or empty." });
                        }

                        IGenerator generator;
                        var connectionString = _configuration.GetConnectionString("DefaultConnection") ?? string.Empty;

                        if (AllowedColumns.Columns.Contains(field.Type))
                        {
                            generator = GeneratorFactory.CreateGenerator(field.Type, connectionString);
                        }
                        else
                        {
                            generator = GeneratorFactory.CreateGenerator(field.Type);
                        }

                        var generatedValue = generator.GenerateRandomValue();

                        // field.Name'in null olmadığından eminiz, o yüzden kullanıyoruz
                        row[field.Name] = generatedValue;
                    }

                    generatedDataList.Add(row);
                }

                return Json(generatedDataList);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
                return StatusCode(500, new { message = "An error occurred while generating data", error = ex.Message });
            }
        }
    }
}
