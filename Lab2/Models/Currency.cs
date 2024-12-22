using System.ComponentModel.DataAnnotations;

namespace Lab2.Models
{
    public class Currency
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
    }
}
