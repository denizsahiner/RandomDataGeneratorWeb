using DataGeneratorLibrary.Generators;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using RandomDataGenerator.Models;
using System.Collections.Generic;
using System.Linq;

namespace RandomDataGenerator.Controllers
{   
    public class HomeController : Controller
    {
        private readonly IConfiguration _configuration;

        // Constructor'da IConfiguration ile bağlantı dizesini alıyoruz
        public HomeController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        // Ana sayfa (Index) aksiyonu
        public IActionResult Index()
        {
            return View();
        }

        // POST aksiyonu: Kullanıcının seçtiği veri tiplerine göre veri üretme işlemi
        [HttpPost("/Home/GenerateData")]
        public IActionResult GenerateData([FromBody] List<Field> fields)
        {
            var generatedData = new List<object>();

            try
            {
                foreach (var field in fields)
                {
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

                    generatedData.Add(new
                    {
                        field.Name,
                        field.Type,
                        GeneratedValue = generatedValue
                    });
                }

                return Json(generatedData);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
                return StatusCode(500, new { message = "An error occurred while generating data", error = ex.Message });
            }
        }

    }
}
