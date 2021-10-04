using System;
using System.Collections.Generic;

#nullable disable

namespace FarmerScheme.Models
{
    public partial class FarmerBidder
    {
        public FarmerBidder()
        {
            Banks = new HashSet<Bank>();
            Bidders = new HashSet<Bidder>();
            CropRequests = new HashSet<CropRequest>();
            CropSoldHistories = new HashSet<CropSoldHistory>();
            InsurancePolicies = new HashSet<InsurancePolicy>();
        }

        public int UniqueId { get; set; }
        public string FullName { get; set; }
        public string ContactNo { get; set; }
        public string EmailId { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Pincode { get; set; }
        public double? LandArea { get; set; }
        public string LandAddress { get; set; }
        public string LandPincode { get; set; }
        public string Aadhar { get; set; }
        public string Pan { get; set; }
        public string Certificate { get; set; }
        public string Password { get; set; }
        public string RegType { get; set; }

        public virtual ICollection<Bank> Banks { get; set; }
        public virtual ICollection<Bidder> Bidders { get; set; }
        public virtual ICollection<CropRequest> CropRequests { get; set; }
        public virtual ICollection<CropSoldHistory> CropSoldHistories { get; set; }
        public virtual ICollection<InsurancePolicy> InsurancePolicies { get; set; }
    }
}
