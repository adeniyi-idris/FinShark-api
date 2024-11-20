using System;
using System.Collections.Generics;
using System.Linq;
using System.Threading.Tasks;

namespace API.Models
{
    public class stock
    {
        public int Id { get; set; }

        public string Symbol { get; set; } = string.Empty;

        public string CompanyName { get; set; } = string.Empty;
        [Column(TypeName = "decimal(18,2)")]
        public decimal Purchase { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal LastDiv { get; set; }

        public string Industry { get; set; } = string.Empty;

        public long MarketCap { get; set; }
        //Navigation Property
        public List<comment> comments {get; set;} = new List<comment>
    }
}