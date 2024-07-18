using System;
using System.ComponentModel.DataAnnotations;

namespace DAL.Entities
{
    public class Document
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Path { get; set; }
        public DateTime CreatedDateTime { get; set; }
    }
}
