//Interface for creating data.
namespace DataGeneratorLibrary.Generators
{    public interface IGenerator
    {
        object GenerateRandomValue();
        string DataType { get; }
       
    }
}
