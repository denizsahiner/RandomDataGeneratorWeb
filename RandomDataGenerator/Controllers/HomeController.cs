using System.Text;
using DataGeneratorLibrary.Generators;
using Microsoft.AspNetCore.Mvc;
using RandomDataGenerator.Models;
using Newtonsoft.Json;
using OfficeOpenXml;

namespace RandomDataGenerator.Controllers
{
    public class HomeController : Controller
    {
        private readonly IConfiguration _configuration;

        //Constructor to initialize configuration
        public HomeController(IConfiguration configuration)
        {
            _configuration = configuration;
            // EPPlus license configuration.
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
        }
        
        public IActionResult Index()
        {
            return View();
        }

        // API endpoint to generate random data
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

                        row[field.Name] = generatedValue;
                    }

                    generatedDataList.Add(row);
                }
                // Store generated data in session
                HttpContext.Session.SetString("GeneratedData", JsonConvert.SerializeObject(generatedDataList));

                return Json(generatedDataList);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
                return StatusCode(500, new { message = "An error occurred while generating data", error = ex.Message });
            }
        }

        // API endpoint to download generated data in various formats
        [HttpPost("/Home/DownloadData")]
        public IActionResult DownloadData([FromBody] DownloadRequest request)
        {
            // Retrieve generated data from session
            var dataJson = HttpContext.Session.GetString("GeneratedData");
            if (string.IsNullOrEmpty(dataJson))
            {
                return BadRequest(new { message = "No Data available to download" });
            }

            var data = JsonConvert.DeserializeObject<List<Dictionary<string, object>>>(dataJson);

            if (data == null || !data.Any())
            {
                return BadRequest(new { message = "No Data available to download" });
            }


            if (request.Format == "CSV")
            {
                string fileContent = ConvertToCSV(data);
                return File(Encoding.UTF8.GetBytes(fileContent), "text/csv", "generated_data.csv");
            }
            else if (request.Format == "excel")
            {
                byte[] fileContent = ConvertToExcel(data);
                return File(fileContent, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "generated_data.xlsx");
            }
            else if (request.Format == "SQL")
            {
                string fileContent = ConvertToSQL(data);
                return File(Encoding.UTF8.GetBytes(fileContent), "application/sql", "generated_data.sql");
            }
            else if (request.Format == "JSON")
            {
                string fileContent = ConvertToJson(data);
                return File(Encoding.UTF8.GetBytes(fileContent), "application/json", "generated_data.json");
            }
            else
            {
                return BadRequest(new { message = "Unsupported file format" });
            }
        }

        private string ConvertToCSV(List<Dictionary<string, object>> data)
        {
            var csv = new StringBuilder();

            var headers = string.Join(",", data[0].Keys);

            csv.AppendLine(headers);

            foreach (var row in data)
            {
                var rowData = string.Join(",", row.Values);
                csv.AppendLine(rowData);
            }
            return csv.ToString();
        }
        private string ConvertToSQL(List<Dictionary<string, object>> data)
        {
            var sql = new StringBuilder();
            foreach (var row in data)
            {
                var values = string.Join(",", row.Values.Select(v => $"'{v}'"));
                sql.AppendLine($"INSERT INTO YourTableName ({string.Join(",", row.Keys)}) VALUES ({values});");
            }

            return sql.ToString();
        }
        private byte[] ConvertToExcel(List<Dictionary<string, object>> data)
        {
            using (var package = new ExcelPackage())
            {
                var worksheet = package.Workbook.Worksheets.Add("Sheet1");

                var columnHeaders = data[0].Keys.ToList();
                for (int col = 0; col < columnHeaders.Count; col++)
                {
                    worksheet.Cells[1, col + 1].Value = columnHeaders[col];
                }

                
                for (int row = 0; row < data.Count; row++)
                {
                    var rowData = data[row].Values.ToList();
                    for (int col = 0; col < rowData.Count; col++)
                    {
                        worksheet.Cells[row + 2, col + 1].Value = rowData[col];
                    }
                }

                using (var stream = new MemoryStream())
                {
                    package.SaveAs(stream);
                    return stream.ToArray(); 
                }
            }
        }
        private string ConvertToJson(List<Dictionary<string, object>> data)
        {
           
            return JsonConvert.SerializeObject(data, Formatting.Indented);
        }
    }

    // Model for download request
    public class DownloadRequest
    {
        public List<Field>? Fields { get; set; }
        public int Count { get; set; }
        public string? Format { get; set; }
    }
}