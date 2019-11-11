using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WeMedCare.Models
{
    public class AppointmentView
    {
        public int AppointmentId { get; set; }
        public int PatientId { get; set; }
        public string PatientName { get; set; }
        public string PatientAddress { get; set; }
        public int Age { get; set; }
        public string PhoneNo { get; set; }
        
        
        
    }
}