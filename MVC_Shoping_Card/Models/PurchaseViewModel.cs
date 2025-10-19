namespace MVC_Shoping_Card.Models
{
    public class PurchaseViewModel
    {
          
        public int Id { get; set; }
        public DateTime PurchaseDate { get; set; }
        public int UserId { get; set; }
        public ProductViewModel Product { get; set; }  
        public bool IsCompleted { get; set; }
        public bool IsSent { get; set; }
        public int Amount { get; set; }
    }
}
