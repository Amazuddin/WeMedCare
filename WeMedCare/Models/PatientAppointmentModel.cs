using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WeMedCare.Models
{
    public class PatientAppointmentModel
    {
        public int formId { get; set; }
        public string patientName { get; set; }
        public int number { get; set; }
        public string email { get; set; }
        public string date { get; set; }
        public string specialistId { get; set; }
        public int id { get; set; }
        public string sex { get; set; }
    }
}