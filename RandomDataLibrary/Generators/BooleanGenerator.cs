using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
        public void SetDataSource(IEnumerable<object> data)
        {
            throw new NotImplementedException("BooleanGenerator does not support data sources.");
        }
    }
}
