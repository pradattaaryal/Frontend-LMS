namespace Frontend.Models
{
    public class Transaction
    {
        public int TransactionId { get; set; }
        public int StudentId { get; set; }
        public int UserId { get; set; }
        public int BookId { get; set; }
        public string TransactionType { get; set; } // "Borrow" or "Return"
        public DateTime Date { get; set; } = DateTime.Today; // Default date without time

    }
}
