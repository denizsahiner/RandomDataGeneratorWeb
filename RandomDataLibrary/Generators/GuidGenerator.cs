//Class for generating data type "GUID"
namespace DataGeneratorLibrary.Generators
{
    public class GuidGenerator : IGenerator
    {       
        public string DataType => "GUID";

        public object GenerateRandomValue()
        {
            return Guid.NewGuid();
        }
    }
}
