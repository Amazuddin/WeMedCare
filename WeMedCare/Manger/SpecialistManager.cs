using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WeMedCare.Gateway;
using WeMedCare.Models;

namespace WeMedCare.Manger
{
    public class SpecialistManager
    {
        private  SpecialistGateway specialistGateway = new SpecialistGateway();
        public List<SpecialistModel> GetAllSpecialist()
        {
            return specialistGateway.GetAllSpecialist();
        }
        public List<DoctorDetailsModel> GetAllDoctorInfoById(int id)
        {
            return specialistGateway.GetAllDoctorInfoById(id);
        }

        public string Appointment(PatientAppointmentModel patientAppointment)
        {
            int rowAffected = specialistGateway.Appointment(patientAppointment);
            if (rowAffected > 0)
            {
                return "Saved";
            }
            else
            {
                return "Not Saved";
            }
        }

        public List<BloodModel> GetAllBloodGroup()
        {
            return specialistGateway.GetAllBloodGroup();
        }

        public string SaveDonor(BloodDonorModel bloodDonor)
        {
            int roweffected = specialistGateway.SaveDonor(bloodDonor);
            if (roweffected>0)
            {
                return "Saved";
            }
            else
            {
                return "Not Saved";
            }
        }
        public List<BloodDonorModel> GetAllDonorInfoById(int id)
        {
            return specialistGateway.GetAllDonorInfoById(id);
        }
    }
}