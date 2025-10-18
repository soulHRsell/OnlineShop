using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shoping_Card_DB_Connection.Models
{
    public class PurchaseModel
    {
        public int Id { get; set; } 
        public DateTime PurchaseDate { get; set; }  
        public int UserId { get; set; } 
        public int ProductId { get; set; }
        public bool IsCompleted { get; set; }
        public bool IsSent { get; set; }    
        public int Amount { get; set; } 
    }
}
