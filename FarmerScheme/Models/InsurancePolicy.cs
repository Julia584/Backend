using System;
using System.Collections.Generic;

#nullable disable

namespace FarmerScheme.Models
{
    public partial class InsurancePolicy
    {
        public InsurancePolicy()
        {
            Claims = new HashSet<Claim>();
        }

        public int InsuranceNo { get; set; }
        public int? UniqueId { get; set; }
        public string CropType { get; set; }
        public string CropName { get; set; }
        public string InsuranceCompany { get; set; }
        public decimal SumInsuredPh { get; set; }
        public double SharePremium { get; set; }
        public decimal PremiumAmount { get; set; }
        public string ZoneType { get; set; }
        public double Area { get; set; }
        public decimal SumInsured { get; set; }

        public virtual FarmerBidder Unique { get; set; }
        public virtual ICollection<Claim> Claims { get; set; }
    }
}
