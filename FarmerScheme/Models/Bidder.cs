using System;
using System.Collections.Generic;

#nullable disable

namespace FarmerScheme.Models
{
    public partial class Bidder
    {
        public int BiddingId { get; set; }
        public int? CropId { get; set; }
        public int? UniqueId { get; set; }
        public decimal BidAmount { get; set; }
        public bool? SellStatus { get; set; }

        public virtual CropRequest Crop { get; set; }
        public virtual FarmerBidder Unique { get; set; }
    }
}
