namespace Lab2.Models
{
    public class Record
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int CategoryId { get; set; }
        public DateTime CreatedAt { get; set; }
        public decimal TotalAmount { get; set; }

    }
}
