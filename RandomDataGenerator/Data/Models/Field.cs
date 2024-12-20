using System.ComponentModel.DataAnnotations.Schema;

namespace RandomDataGenerator.Data.Models
{
    [Table("DATA_TABLE")]
    public class Field
    {
        public int id { get; set; }
        public string firstName { get; set; } = string.Empty; 
        public string middleName { get; set; } = string.Empty; 
        public string lastName { get; set; } = string.Empty; 
        public string gender { get; set; } = string.Empty;
        public string ssn { get; set; } = string.Empty;
        public int salary { get; set; }

    }
}
