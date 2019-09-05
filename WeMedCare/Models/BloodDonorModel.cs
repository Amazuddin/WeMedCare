using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.AccessControl;
using System.Web;

namespace WeMedCare.Models
{
    public class BloodDonorModel
    {
        public int id { get; set; }
        public string donorName { get; set; }
        public int number { get; set; }
        public int bloodid { get; set; }
        public string sex { get; set; }
        public string address { get; set; }
    }
}