using System;
using System.Collections.Generic;

#nullable disable

namespace FarmerScheme.Models
{
    public partial class CropSoldHistory
    {
        public DateTime? DateOfSale { get; set; }
        public int? CropId { get; set; }
        public int? UniqueId { get; set; }
        public string CropName { get; set; }
        public decimal? Msp { get; set; }
        public decimal? SoldPrice { get; set; }
        public double? Quantity { get; set; }
        public int SoldId { get; set; }

        public virtual CropRequest Crop { get; set; }
        public virtual FarmerBidder Unique { get; set; }
    }
}
