using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WeMedCare.Models
{
    public class DoctorDetailsModel
    {
        public int id { get; set; }
        public string name { get; set; }
        public string degree { get; set; }
        public int fees { get; set; }
        public string schedule { get; set; }
        public int specialistId { get; set; }
    }
}