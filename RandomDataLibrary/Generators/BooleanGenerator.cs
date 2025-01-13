//Class for generating data type "boolean"

namespace DataGeneratorLibrary.Generators
{
    public class BooleanGenerator : IGenerator
    {
        private Random _random = new Random();

        public string DataType => "Boolean";
        public object GenerateRandomValue()
        {
            return _random.Next(0, 2) == 1; 
        }
    }
}
