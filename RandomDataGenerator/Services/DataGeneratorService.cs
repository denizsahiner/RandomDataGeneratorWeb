using DataGeneratorLibrary.Generators;
namespace RandomDataGenerator.Services
{
    public class DataGeneratorService
    {
        public object GenerateRandomData(string DataType)
        {
            IGenerator generator = DataType.ToLower() switch
            {
                "string" => new StringGenerator(),
                "number" => new NumberGenerator(),
                "boolean" => new BooleanGenerator(),
                "date" => new DateGenerator(),
                _ => throw new ArgumentException("Invalid data type")
            };
            return generator.GenerateRandomValue();
        }
    }
}
