using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WeMedCare.Models
{
    public class PatientAppointmentModel
    {
        public int Id { get; set; }
        public int PatientId { get; set; }
        public string PatientName { get; set; }
        public int Number { get; set; }
        public string Email { get; set; }
        public string Date { get; set; }
        public string SpecialistId { get; set; }
        public int DoctorId { get; set; }
        public string Sex { get; set; }
        public string Prescription { get; set; }
        public int WardId { get; set; }
        public int Accepted { get; set; }
    }
}