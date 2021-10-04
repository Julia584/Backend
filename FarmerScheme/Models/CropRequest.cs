using System;
using System.Collections.Generic;

#nullable disable

namespace FarmerScheme.Models
{
    public partial class CropRequest
    {
        public CropRequest()
        {
            Bidders = new HashSet<Bidder>();
            CropSoldHistories = new HashSet<CropSoldHistory>();
        }

        public int CropId { get; set; }
        public string CropName { get; set; }
        public string CropType { get; set; }
        public string FertilizerType { get; set; }
        public double Quantity { get; set; }
        public decimal Msp { get; set; }
        public string SoilPhcertificate { get; set; }
        public int? UniqueId { get; set; }
        public string Status { get; set; }
        public bool? Approval { get; set; }

        public virtual FarmerBidder Unique { get; set; }
        public virtual ICollection<Bidder> Bidders { get; set; }
        public virtual ICollection<CropSoldHistory> CropSoldHistories { get; set; }
    }
}
