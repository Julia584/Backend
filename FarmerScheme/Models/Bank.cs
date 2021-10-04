using System;
using System.Collections.Generic;

#nullable disable

namespace FarmerScheme.Models
{
    public partial class Bank
    {
        public string AccNo { get; set; }
        public string Ifsc { get; set; }
        public int? UniqueId { get; set; }

        public virtual FarmerBidder Unique { get; set; }
    }
}
