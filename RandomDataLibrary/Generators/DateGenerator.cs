using System;
using System.Collections.Generic;

namespace DataGeneratorLibrary.Generators
{
    public class DateGenerator : IGenerator
    {
        private static readonly Random RandomGenerator = new Random();
        private readonly DateTime _latestDate = new DateTime(2024, 12, 31);

        public string DataType => "Date";

        public object GenerateRandomValue()
        {           
            int range = (int)(_latestDate - DateTime.MinValue).TotalDays;
           
            return DateTime.MinValue.AddDays(RandomGenerator.Next(range));
        }       
    }
}
