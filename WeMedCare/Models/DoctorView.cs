using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WeMedCare.Models
{
    public class DoctorView
    {
        public int Id { get; set; }
        public string DocName { get; set; }
        public string DocDegree { get; set; }
        public string DocSpecialist { get; set; }
    }
}