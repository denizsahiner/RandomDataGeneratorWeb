using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataGeneratorLibrary.Generators
{
    public class StringGenerator : IGenerator
    {
        private Random _random = new Random();

        public string DataType => "String";

        public object GenerateRandomValue()
        {
            var chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz";
            return new string(Enumerable.Repeat(chars, 8).Select(s => s[_random.Next(s.Length)]).ToArray());
        }
        public void SetDataSource(IEnumerable<object> data)
        {
            throw new NotImplementedException("StringGenerator does not support data sources.");
        }
    }
}
