using System;
using System.Collections.Generic;

#nullable disable

namespace FarmerScheme.Models
{
    public partial class Claim
    {
        public int ClaimId { get; set; }
        public int? InsuranceNo { get; set; }
        public string Reason { get; set; }
        public string DateOfLoss { get; set; }
        public bool? Approval { get; set; }

        public virtual InsurancePolicy InsuranceNoNavigation { get; set; }
    }
}
