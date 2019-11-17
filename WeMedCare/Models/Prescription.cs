using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WeMedCare.Models
{
    public class Prescription
    {
        public int Id { get; set; }
        public int DocId { get; set; }
        public string PatientAddress { get; set; }
        public string DoctorName { get; set; }
        public string Date { get; set; }
        public string PatientName { get; set; }
       
        public string PatientWeight { get; set; }
        
        public int PatientAge { get; set; }
        
        public string PrescriptMedicine { get; set; }
       
        public string PrescriptTest { get; set; }
        public int WardId { get; set; }
        
    }
}