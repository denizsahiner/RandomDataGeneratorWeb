using DataGeneratorLibrary.Generators;

public static class GeneratorFactory
{
    public static IGenerator CreateGenerator(string dataType, string connectionString )
    {
        return dataType switch
        {          
            "firstName" => new DatabaseValueGenerator(connectionString, "firstName"),
            "middleName" => new DatabaseValueGenerator(connectionString, "middleName"),
            "lastName" => new DatabaseValueGenerator(connectionString, "lastName"),
            "gender" => new DatabaseValueGenerator(connectionString, "gender"),
            "ssn"=>new DatabaseValueGenerator(connectionString,"ssn"),
            "salary"=> new DatabaseValueGenerator(connectionString,"salary"),
            _ => throw new ArgumentException("Invalid data type selected")
        };
    }
    public static IGenerator CreateGenerator(string dataType)
    {
        return dataType switch
        {
            "String" => new StringGenerator(),
            "Number" => new NumberGenerator(),
            "Boolean" => new BooleanGenerator(),
            "Date" => new DateGenerator(),   
            _ => throw new ArgumentException("Invalid data type selected")
        };
    }
}
