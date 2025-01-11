using System;
using System.Collections.Generic;

namespace DataGeneratorLibrary.Generators
{
    public class DateGenerator : IGenerator
    {
        private static readonly Random RandomGenerator = new Random();
        private readonly DateOnly _earliestDate = new DateOnly(1950, 1, 1);
        private readonly DateOnly _latestDate = new DateOnly(2024, 12, 31);

        public string DataType => "Date";

        public object GenerateRandomValue()
        {
            int range = (_latestDate.ToDateTime(new TimeOnly()) - _earliestDate.ToDateTime(new TimeOnly())).Days;
            return _earliestDate.AddDays(RandomGenerator.Next(range));
        }
    }
}
