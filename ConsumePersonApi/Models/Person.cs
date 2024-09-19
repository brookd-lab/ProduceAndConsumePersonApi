using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace ConsumePersonApi.Models
{
    public class Person
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set;  }
        public int Age { get; set; }
    }
}
