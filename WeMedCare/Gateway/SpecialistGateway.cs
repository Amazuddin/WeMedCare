using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using WeMedCare.Models;

namespace WeMedCare.Gateway
{
    public class SpecialistGateway
    {
        public List<SpecialistModel> GetAllSpecialist()
        {
            string connectionstring = WebConfigurationManager.ConnectionStrings["StringCon"].ConnectionString;
            SqlConnection connection = new SqlConnection(connectionstring);
            string query = "SELECT * FROM Specialist";
            SqlCommand command = new SqlCommand(query, connection);
            connection.Open();
            List<SpecialistModel> specialists = new List<SpecialistModel>();
            SqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                SpecialistModel specialist = new SpecialistModel();
                specialist.SpecialistId = (int)reader["id"];
                specialist.Specialist = reader["specialist"].ToString();

                specialists.Add(specialist);
            }
            connection.Close();
            return specialists;
        }
        public List<DoctorDetailsModel> GetAllDoctorInfoById(int id)
        {
            string connectionstring = WebConfigurationManager.ConnectionStrings["StringCon"].ConnectionString;
            SqlConnection connection = new SqlConnection(connectionstring);
            string query = "SELECT * FROM Doctor WHERE Specialist_id=" + id;
            SqlCommand command = new SqlCommand(query, connection);
            connection.Open();
            List<DoctorDetailsModel> doctorDetailsModels = new List<DoctorDetailsModel>();
            SqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                DoctorDetailsModel doctorDetails = new DoctorDetailsModel();
                doctorDetails.id = (int)reader["Id"];
                doctorDetails.name = reader["Name"].ToString();
                doctorDetails.degree = reader["Degree"].ToString();
                doctorDetails.fees = (int)reader["Fees"];
                doctorDetails.schedule = reader["Schedule"].ToString();
                doctorDetailsModels.Add(doctorDetails);
            }
            connection.Close();
            return doctorDetailsModels;
        }

        public int Appointment(PatientAppointmentModel patientAppointment)
        {
            string data="";
            string connectionstring = WebConfigurationManager.ConnectionStrings["StringCon"].ConnectionString;
            SqlConnection connection = new SqlConnection(connectionstring);

            string q = "SELECT specialist FROM Specialist WHERE Id=" + patientAppointment.specialistId;
            SqlCommand c = new SqlCommand(q,connection);
            connection.Open();
            SqlDataReader name = c.ExecuteReader();
            
            while (name.Read())
                data = name["specialist"].ToString();
            name.Close();
            string query = "INSERT INTO Appointment(patient_name,phone_number,email,date,department,doctor_id,sex)" +
                           " VALUES('" + patientAppointment.patientName + "','" +
                           patientAppointment.number+ "','" + patientAppointment.email+ "','" + 
                           patientAppointment.date+ "','" + data + "','" 
                           + patientAppointment.id + "','" + patientAppointment.sex+ "')";
            
            SqlCommand command = new SqlCommand(query, connection);
            
           
            int rowAffect = command.ExecuteNonQuery();
            connection.Close();

            return rowAffect;
        }


        public List<BloodModel> GetAllBloodGroup()
        {
            string connectionstring = WebConfigurationManager.ConnectionStrings["StringCon"].ConnectionString;
            SqlConnection connection = new SqlConnection(connectionstring);
            string query = "SELECT * FROM Blood";
            SqlCommand command = new SqlCommand(query, connection);
            connection.Open();
            List<BloodModel> bloods = new List<BloodModel>();
            SqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                BloodModel blood = new BloodModel();
                blood.id = (int)reader["id"];
                blood.bloodGroup = reader["blood_group"].ToString();

                bloods.Add(blood);
            }
            connection.Close();
            return bloods;
        }

        public int SaveDonor(BloodDonorModel bloodDonor)
        {
            
            string connectionstring = WebConfigurationManager.ConnectionStrings["StringCon"].ConnectionString;
            SqlConnection connection = new SqlConnection(connectionstring);
            string query = "INSERT INTO BloodDonor(donor_name,contact_number,blood_groupid,gender,address)" +
                           " VALUES('" + bloodDonor.donorName + "','" +
                           bloodDonor.number + "','" + bloodDonor.bloodid + "','" +
                           bloodDonor.sex+ "','"
                           + bloodDonor.address + "')";

            SqlCommand command = new SqlCommand(query, connection);
            connection.Open();
            int result = command.ExecuteNonQuery();
            connection.Close();
            return result;

        }

        public List<BloodDonorModel> GetAllDonorInfoById(int id)
        {
            string connectionstring = WebConfigurationManager.ConnectionStrings["StringCon"].ConnectionString;
            SqlConnection connection = new SqlConnection(connectionstring);
            string query = "SELECT * FROM BloodDonor WHERE blood_groupid=" + id;
            SqlCommand command = new SqlCommand(query, connection);
            connection.Open();
            List<BloodDonorModel> bloodDonorModels = new List<BloodDonorModel>();
            SqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                BloodDonorModel donordetails = new BloodDonorModel();
                donordetails.id = (int)reader["id"];
                donordetails.donorName = reader["donor_name"].ToString();
                donordetails.number = (int)reader["contact_number"];
                donordetails.address = reader["address"].ToString();

                bloodDonorModels.Add(donordetails);
            }
            connection.Close();
            return bloodDonorModels;
        }

    }

}