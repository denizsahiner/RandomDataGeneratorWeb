using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataGeneratorLibrary.Generators
{
    public interface IGenerator
    {
        object GenerateRandomValue();
        string DataType { get; }

       
    }
}
