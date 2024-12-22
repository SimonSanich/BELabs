using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Lab2.Models
{
    public class Record
    {
        [Key]
        public Guid Id { get; set; }

        [ForeignKey("User")]
        public required Guid UserId { get; set; }
        [ForeignKey("Category")]
        public required int CategoryId { get; set; }
        public DateTime CreatedAt { get; set; }
        public decimal TotalAmount { get; set; }

        public User User { get; set; }
        public Category Category { get; set; }
        public int? CurrencyId { get; set; }
        [ForeignKey("CurrencyId")]
        public virtual Currency Currency { get; set; }

    }
}
