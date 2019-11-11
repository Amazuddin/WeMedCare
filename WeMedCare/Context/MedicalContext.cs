using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Services.Description;
using WeMedCare.Models;

namespace WeMedCare.Context
{
    public class MedicalContext:DbContext
    {
        public DbSet<Services> AllServices { get; set; }
        public DbSet<DoctorDetailsModel> Doctors { get; set; }
        public DbSet<BloodModel> Blood { get; set; }
        public DbSet<BloodDonorModel> BloodDonor { get; set; }
        public DbSet<PatientAppointmentModel> Appointment { get; set; }
        public DbSet<SpecialistModel> Specialist { get; set; }
        public DbSet<Register> Registers { get; set; }
        public DbSet<TestForm> TestForms { get; set; }
        public DbSet<Receptionist> Receptionist { get; set; }
        public DbSet<Admin> Admins { get; set; }
        public DbSet<Nurse> Nurses { get; set; }
        public DbSet<Ward> Wards { get; set; } 

    }
}