//Class for generating data type "number"
namespace DataGeneratorLibrary.Generators
{
    public class NumberGenerator : IGenerator
    {
        private Random _random = new Random();

        public string DataType => "Integer";

        public object GenerateRandomValue()
        {
            return _random.Next(1, 100);
        }
    }
}
