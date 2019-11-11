using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WeMedCare.Models
{
    public class DoctorAppointmentView
    {
        public string DoctorName { get; set; }
        public string Date { get; set; }
        public string Specialist { get; set; }
        public string Prescription { get; set; }
    }
}