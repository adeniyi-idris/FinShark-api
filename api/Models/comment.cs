using System;
using System.Collections.Generics;
using System.Linq;
using System.Threading.Tasks;

namespace API.Models
{
    public class comments
    {
        public int Id { get; set; } = string.Empty;

        public string Title { get; set; } = string.Empty;

        public string Contents { get; set; } = string.Empty;

        public DateTime CreateOn { get; set; } = DateTime.Now;

        public int? StockId { get; set; }
        //Navigation Property
        public stock stock { get; set; }
    }
}