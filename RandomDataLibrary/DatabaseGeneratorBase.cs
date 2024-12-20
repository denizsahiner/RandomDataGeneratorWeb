using System;
using System.Collections.Generic;
using System.Linq;
using DataGeneratorLibrary.Generators;

namespace DataGeneratorLibrary
{
    public abstract class DatabaseGeneratorBase : IGenerator
    {
        protected IEnumerable<object>? DataSource; 

        public string DataType { get; protected set; } = string.Empty;

        public abstract object GenerateRandomValue();

        public void SetDataSource(IEnumerable<object> data)
        {
            DataSource = data ?? throw new ArgumentNullException(nameof(data));
        }

        protected object GetRandomValue()
        {
            if (DataSource == null || !DataSource.Any())
                throw new InvalidOperationException("Data source is not set or empty.");

            var random = new Random();
            return DataSource.ElementAt(random.Next(DataSource.Count()));
        }
    }
}
