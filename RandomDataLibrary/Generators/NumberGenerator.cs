using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataGeneratorLibrary.Generators
{
    public class NumberGenerator : IGenerator
    {
        private Random _random = new Random();

        public string DataType => "Integer";

        public object GenerateRandomValue()
        {
            return _random.Next(1, 100); // 1 ile 100 arasında rastgele sayı
        }
    }
}
