//Class for generating data type "string"
namespace DataGeneratorLibrary.Generators
{
    public class StringGenerator : IGenerator
    {
        private readonly Random _random = new();

        public string DataType => "String";

        public object GenerateRandomValue()
        {
            var chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz";
            return new string(Enumerable.Repeat(chars, 8).Select(s => s[_random.Next(s.Length)]).ToArray());
        }
    }
}
