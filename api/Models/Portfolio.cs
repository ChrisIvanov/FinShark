using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace api.Models
{
    [Table("Portfolios")]
    public class Portfolio
    {
        public required string AppUserId { get; set; }

        //navigation property set as convetion. it can be skipped

        public AppUser AppUser { get; set; }

        public required int StockId { get; set; }

        //navigation property set as convetion. it can be skipped

        public Stock Stock { get; set; }
    }
}