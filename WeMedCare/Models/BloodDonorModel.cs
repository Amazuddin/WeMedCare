using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.AccessControl;
using System.Web;

namespace WeMedCare.Models
{
    public class BloodDonorModel
    {
        public int Id { get; set; }
        public string DonorName { get; set; }
        public string Number { get; set; }
        public int Bloodid { get; set; }
        public string Sex { get; set; }
        public string Address { get; set; }
    }
} 