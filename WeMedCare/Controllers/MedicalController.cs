using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Web.Services.Description;
using System.Web.WebPages;
using Newtonsoft.Json;
using Rotativa.MVC;
using WeMedCare.Context;
using WeMedCare.Models;

namespace WeMedCare.Controllers
{
    public class MedicalController : Controller
    {
        PrivacyController privacy = new PrivacyController();
        //
        // GET: /Medical/
        public ActionResult Index()
        {
            ViewBag.Index = "active";
            List<Services> services;
            using (var ctx = new MedicalContext())
            {
                services = ctx.AllServices.ToList();
            }
            return View(services);
        }
        public ActionResult DoctorInfo()
        {
            ViewBag.Doctor = "active";
            List<SpecialistModel> doctorCategories;
            
           
            using (var ctx = new MedicalContext())
            {

                doctorCategories = ctx.Specialist.ToList();
            }
           
            ViewBag.Category = doctorCategories;
            
            return View();
        }

        public JsonResult GetAllDoctorInfoById(int id)
        {

            List<DoctorDetailsModel> doctor = new List<DoctorDetailsModel>();
           
            using (var ctx = new MedicalContext())
            {
                var a = ctx.Doctors.Where(s => s.SpecialistId == id).Select(c => new { c.Name, c.Degree,c.Fees,c.Schedule }); ;
                foreach (var k in a)
                {
                    DoctorDetailsModel doc = new DoctorDetailsModel();
                    doc.Name = privacy.Decrypt(k.Name);
                    doc.Degree = privacy.Decrypt(k.Degree);
                    doc.Fees = k.Fees;
                    doc.Schedule = privacy.Decrypt(k.Schedule);
                    doctor.Add(doc);
                }
            } 
            return Json(doctor);
        }
        public ActionResult BloodDonorList()
        {
            ViewBag.Donor = "active";
            List<BloodModel> bloodCategories;


            using (var ctx = new MedicalContext())
            {

                bloodCategories = ctx.Blood.ToList();
            }

            ViewBag.Category = bloodCategories;

            return View();
        }
        public JsonResult GetAllDonorInfoById(int id)
        {

            List<BloodDonorModel> donor = new List<BloodDonorModel>();

            using (var ctx = new MedicalContext())
            {
                var a = ctx.BloodDonor.Where(s => s.Bloodid == +id);
                //var a = ctx.Doctors.SqlQuery("Select Name, Degree, Fees, Schedule from DoctorDetailsModel where SpecialistId=" + id);

                foreach (var k in a)
                {
                    BloodDonorModel doc = new BloodDonorModel();
                    doc.DonorName = k.DonorName;
                    doc.Number = k.Number;
                    doc.Address = k.Address;
                    
                    donor.Add(doc);
                }
            }
            return Json(donor);
        }

        public ActionResult BloodDonorRegistration()
        {
            ViewBag.Donor = "active";
            if (Session["PatientId"] == null)
                return RedirectToAction("LogIn", "Security");
            List<BloodModel> bloodCategories;


            using (var ctx = new MedicalContext())
            {

                bloodCategories = ctx.Blood.ToList();
            }

            ViewBag.Category = bloodCategories;
            return View();
        }
        [HttpPost]
        public ActionResult BloodDonorRegistration(BloodDonorModel bloodDonor)
        {
            ViewBag.Donor = "active";

            List<BloodModel> bloodCategories;
            using (var ctx = new MedicalContext())
            {

                ctx.BloodDonor.Add(bloodDonor);
                ctx.SaveChanges();
                bloodCategories = ctx.Blood.ToList();
            }
            ViewBag.Category = bloodCategories;
            return View();
        }

        public ActionResult Appointment()
        {
            if (Session["PatientId"] == null)
            return RedirectToAction("LogIn", "Security");
            ViewBag.Appoint = "active";
            List<SpecialistModel> specialists;


            using (var ctx = new MedicalContext())
            {

                specialists = ctx.Specialist.ToList();
            }

            ViewBag.Category = specialists;
            return View();
        }

        public JsonResult GetAllDoctorNameById(int id)
        {

            List<DoctorDetailsModel> doctors = new List<DoctorDetailsModel>();

            using (var ctx = new MedicalContext())
            {
                var a = ctx.Doctors.Where(s => s.SpecialistId == +id);
                

                foreach (var k in a)
                {
                    DoctorDetailsModel doc = new DoctorDetailsModel();
                    doc.Id = k.Id;
                    doc.Name = privacy.Decrypt(k.Name);
                    

                    doctors.Add(doc);
                }
            }
            return Json(doctors);
        }
        [HttpPost]
        public ActionResult Appointment(PatientAppointmentModel patientAppointment)
        {

            ViewBag.patient = "active";
            patientAppointment.PatientId = Convert.ToInt32(Session["PatientId"]);
            List<SpecialistModel> specialists;
            using (var db = new MedicalContext())
            {
                
                    db.Appointment.Add(patientAppointment);
                    db.SaveChanges();
                   
         
                specialists = db.Specialist.ToList();
            }
            ViewBag.Category = specialists;
            ViewBag.Error = '1';
            return View();
        }

       
        public static string EncodePassword(string password)     
        {
            Byte[] originalBytes;
            Byte[] encodedBytes;
            MD5 a;  
            a = new MD5CryptoServiceProvider();
            originalBytes = ASCIIEncoding.Default.GetBytes(password);
            encodedBytes = a.ComputeHash(originalBytes);
            return BitConverter.ToString(encodedBytes);
        }
        public ActionResult PatientLogout()
        {
            Session["PatientId"] = null;
            Session["PatientName"] = null;
            return RedirectToAction("Index", "Medical");
        }
        public ActionResult DoctorLogout()
        {
            Session["DoctorId"] = null;
            Session["DoctorName"] = null;
            return RedirectToAction("Index", "Medical");
        }
        public ActionResult ReceptionistLogout()
        {
            Session["ReceptionistId"] = null;
            Session["ReceptionistName"] = null;
            return RedirectToAction("Index", "Medical");
        }

        public ActionResult Blood()
        {
            ViewBag.Blood = "active";
            return View();
        }

        public ActionResult Tutorial()
        {
            ViewBag.Tutorial = "active";
            return View();
        }
        public ActionResult Reciption()
        {
            ViewBag.Reciption = "active";
            return View();
        }
        [HttpPost]
        public ActionResult Reciption(string dt)
        {
            string ll = dt + "/01/19";
            string hl = dt + "/31/19";
            int[] ar = new int[32];
            for (int i = 1; i <= 31; i++)
                ar[i] = 0;



            using (var ctx = new MedicalContext())
            {


                string sql = "SELECT Count(a.Id) as val,a.Date FROM PatientAppointmentModels as a " +
                                                        "WHERE a.Date>='" + ll + "' AND a.Date<='" + hl + "' GROUP BY a.Date";
                SqlConnection cn = new SqlConnection(ctx.Database.Connection.ConnectionString);
                SqlCommand command = new SqlCommand(sql, cn);

                cn.Open();


                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        int a = Convert.ToInt32(reader["val"]);
                        string b = reader["Date"].ToString();
                        string kl = b[3].ToString() + b[4].ToString();
                        int dd = Convert.ToInt32(kl);
                        ar[dd - 1] = a;
                    }
                }
                cn.Close();
            }

            string dataStr = JsonConvert.SerializeObject(ar, Formatting.None);
            ViewBag.Data = dataStr;
            ViewBag.mon = dt;
            return View();

        }



        public ActionResult Test()
        {
            ViewBag.Test = "active";
            return View();
        }

        public ActionResult TestForm(string data)
        {
            if (Session["PatientId"] == null)
            return RedirectToAction("LogIn", "Security");

            ViewBag.TestForm = "active";
            ViewBag.Data = data;
            return View();
        }
        [HttpPost]
        public ActionResult TestForm(TestForm testForm)
        {
            ViewBag.TestForm = "active";
            testForm.PatientId = Convert.ToInt32(Session["PatientId"]);
            using (var db= new MedicalContext())
            {
                db.TestForms.Add(testForm);
                db.SaveChanges();
            }
            ViewBag.Yes = '1';
            return View();
        }
       
        public ActionResult PatientProfile()
        {
            ViewBag.PatientProfile = "active";
            Register patient=new Register();
            int patientid = Convert.ToInt32(Session["PatientId"]);
            using (var db=new MedicalContext())
            {
                var u = db.Registers.Where(k => k.Id== patientid).Select(c => new { c.Name, c.Age, c.Address, c.PhoneNo });
                foreach (var j in u)
                {
                    patient.Name = privacy.Decrypt(j.Name);
                    patient.Address = privacy.Decrypt(j.Address);
                    patient.Age = j.Age;
                    patient.PhoneNo = privacy.Decrypt(j.PhoneNo);
                }
                ViewBag.Patient = patient;
                return View();
            }
            
        }

        public ActionResult PatientProfileUpdate()
        {
            ViewBag.PatientProfileUpdate = "active";
            Register patient = new Register();
            int patientid = Convert.ToInt32(Session["PatientId"]);
            using (var db = new MedicalContext())
            {
                var u = db.Registers.Where(k => k.Id == patientid).Select(c => new { c.Name, c.Age, c.Address, c.PhoneNo });
                foreach (var j in u)
                {
                    patient.Name = privacy.Decrypt(j.Name);
                    patient.Address = privacy.Decrypt(j.Address);
                    patient.Age = j.Age;
                    patient.PhoneNo = privacy.Decrypt(j.PhoneNo);
                }
                ViewBag.Patient = patient;
                return View();
            }
        }
        public ActionResult Update(Register patient)
        {

            int id = Convert.ToInt32(Session["PatientId"]);
            string useremail = privacy.Encrypt(patient.Email);
            using (var db = new MedicalContext())
            {
                Register pa = db.Registers.Single(e => e.Id == id);
                if (pa.Email == useremail)
                {
                    pa.Name = privacy.Encrypt(patient.Name);
                    pa.Age = patient.Age;
                    pa.Address = privacy.Encrypt(patient.Address);
                    pa.PhoneNo = privacy.Encrypt(patient.PhoneNo);
                    db.SaveChanges();
                    return RedirectToAction("PatientProfile", "Medical");
                }

                return RedirectToAction("PatientProfile", "Medical");

            }
        }
        //reception profile
        public ActionResult ReceptionistProfile()
        {
            ViewBag.ReceptionistProfile = "active";
           Receptionist receptionist = new Receptionist();
           int receptionistid = Convert.ToInt32(Session["ReceptionistId"]);
            using (var db = new MedicalContext())
            {
                var q = db.Receptionist.Where(k => k.Id == receptionistid).Select(c => new { c.Name,c.Address, c.PhoneNo });
                foreach (var j in q)
                {
                    receptionist.Name = j.Name;
                    receptionist.Address = j.Address;
                    receptionist.PhoneNo = j.PhoneNo;
                }
                ViewBag.Receptionist = receptionist;
                return View();
            }

        }
        public ActionResult ReceptionistProfileUpdate()
        {
            ViewBag.ReceptionistProfileUpdate = "active";
            Receptionist receptionist = new Receptionist();
            int receptionistid = Convert.ToInt32(Session["ReceptionistId"]);
            using (var db = new MedicalContext())
            {
                var q = db.Receptionist.Where(k => k.Id == receptionistid).Select(c => new { c.Name, c.Address, c.PhoneNo });
                foreach (var j in q)
                {
                    receptionist.Name = j.Name;
                    receptionist.Address = j.Address;
                    receptionist.PhoneNo = j.PhoneNo;
                }
                ViewBag.Receptionist = receptionist;
                return View();
            }
        }
        public ActionResult UpdateReceptionist(Receptionist receptionist)
        {

            int id = Convert.ToInt32(Session["ReceptionistId"]);
            string useremail = privacy.Encrypt(receptionist.Email);
            using (var db = new MedicalContext())
            {
                Receptionist pa = db.Receptionist.Single(e => e.Id == id);
                if (pa.Email == useremail)
                {
                    pa.Name = receptionist.Name;
                    pa.Address = receptionist.Address;
                    pa.PhoneNo = receptionist.PhoneNo;
                    db.SaveChanges();
                    return RedirectToAction("ReceptionistProfile", "Medical");
                }

                return RedirectToAction("ReceptionistProfile", "Medical");

            }
        }

        public ActionResult DoctorAppointment()
        {

            List<SpecialistModel> specialists;


            using (var db = new MedicalContext())
            {

                specialists = db.Specialist.ToList();
            }

            ViewBag.Category = specialists;
            return View();
        }
        public JsonResult GetAllPatientByDoctorId(int id,string date)
        {

            List<PatientAppointmentModel> patients = new List<PatientAppointmentModel>();

            using (var db = new MedicalContext())
            {
                var a = db.Appointment.Where(s => s.DoctorId== id && s.Date==date);


                foreach (var k in a)
                {
                    PatientAppointmentModel patient = new PatientAppointmentModel();
                    patient.PatientName = k.PatientName;
                    patient.Number = k.Number;
                    patient.Email = k.Email;
                    patient.Sex = k.Sex;
                    patients.Add(patient);
                }
            }
            return Json(patients);
        }

        public ActionResult MakePdf(Prescription prescription)
        {
            DoctorView doctor = new DoctorView();
           
            using (var db = new MedicalContext())
            {
                var q = 
                (from a in db.Doctors
                 join p in db.Specialist on a.SpecialistId equals p.Id
                 where a.Id == prescription.DocId
                 select new
                 {
                     dId = a.Id,
                     dName = a.Name,
                     dQualification = a.Degree,
                     dspecialist = p.Specialist
                 });
                foreach (var j in q)
                {

                    doctor.DocName = privacy.Decrypt(j.dName);
                    doctor.DocSpecialist = j.dspecialist;
                    doctor.DocDegree = privacy.Decrypt(j.dQualification);
                }
                ViewBag.Doctor = doctor;
                return View(prescription);
            }
        }

        public ActionResult Prescription()
        {
            ViewBag.Prescription = "active";
            return View();
        }
        [HttpPost]
        public ActionResult Prescription(Prescription prescription)
        {
            prescription.DocId = Convert.ToInt32(Session["DoctorId"]);
            ViewBag.Prescription = "active";
            string searchdate = DateTime.Now.ToString("MM/dd/yyyy");
            string pdfname;
            pdfname = DateTime.Now.ToString("dd-MM-yyyy") +"_"+Guid.NewGuid()+".pdf";
            var printpdf = new ActionAsPdf("MakePdf", prescription) { FileName = pdfname };

            string path = Server.MapPath("~/Prescriptions");
            string a = Path.Combine(path, pdfname);
            var byteArray = printpdf.BuildPdf(ControllerContext);
            var fileStream = new FileStream(a, FileMode.Create, FileAccess.Write);
            fileStream.Write(byteArray, 0, byteArray.Length);
            fileStream.Close();
            using (var db = new MedicalContext())
            {
                PatientAppointmentModel app = db.Appointment.Single(c => c.Email == prescription.PatientEmail && c.DoctorId==prescription.DocId 
                    && c.Date==searchdate);
                app.Prescription = "Prescriptions/" + pdfname;
                
                db.SaveChanges();
            }
            return printpdf;
        }




        //Doctor profile
        //public ActionResult DoctorProfile()
        //{
        //    ViewBag.DoctorProfile = "active";
        //    DoctorDetailsModel doctor = new DoctorDetailsModel();
        //    int doctortid = Convert.ToInt32(Session["DoctorId"]);
        //    using (var db = new MedicalContext())
        //    {
        //        var q = db.Doctors.Where(k => k.Id == doctortid).Select(c => new { c.Name, c.Degree, c.Fees });
        //        foreach (var j in q)
        //        {
        //            doctor.Name = j.Name;
        //            doctor.Degree = j.Degree;
        //            doctor.Fees = j.Fees;
        //        }
        //        ViewBag.Doctor = doctor;
        //        return View();
        //    }

        //}
        //public ActionResult DoctorProfileUpdate()
        //{
        //    ViewBag.DoctorProfileUpdate = "active";
        //    DoctorDetailsModel doctor = new DoctorDetailsModel();
        //    int doctortid = Convert.ToInt32(Session["DoctorId"]);
        //    using (var db = new MedicalContext())
        //    {
        //        var q = db.Doctors.Where(k => k.Id == doctortid).Select(c => new { c.Name, c.Degree, c.Fees });
        //        foreach (var j in q)
        //        {
        //            doctor.Name = j.Name;
        //            doctor.Degree = j.Degree;
        //            doctor.Fees = j.Fees;
        //        }
        //        ViewBag.Doctor = doctor;
        //        return View();
        //    }
        //}
        //public ActionResult UpdateDoctor(DoctorDetailsModel doctor)
        //{

        //    int id = Convert.ToInt32(Session["DoctorId"]);
        //    string useremail = doctor.Email;
        //    using (var db = new MedicalContext())
        //    {
        //        DoctorDetailsModel pa = db.Doctors.Single(e => e.Id == id);
        //        if (pa.Email == useremail)
        //        {
        //            pa.Name = doctor.Name;
        //            pa.Degree= doctor.Degree;
        //            pa.Fees = doctor.Fees;
        //            db.SaveChanges();
        //            return RedirectToAction("ReceptionistProfile", "Medical");
        //        }

        //        return RedirectToAction("ReceptionistProfile", "Medical");

        //    }
        //}

        public ActionResult Information()
        {
            ViewBag.Information = "active";
            int id = Convert.ToInt32(Session["PatientId"]);
            List<AppointmentView> appointmentList = new List<AppointmentView>();
           
            using (var db = new MedicalContext())
            {
                var q = (from ab in db.Doctors
                         join p in db.Specialist on ab.SpecialistId equals p.Id
                    
                         join a in db.Appointment on ab.Id equals a.DoctorId
                            where a.DoctorId == ab.Id && a.PatientId == id
                            select new
                            {
                                appointmentDate = a.Date,
                                appointmentName = ab.Name,
                                appointmentSpecialist=p.Specialist,
                                appointmentPrescription=a.Prescription
                                
                            });
                foreach (var d in q)
                {
                    AppointmentView ap = new AppointmentView();
                    ap.DoctorName = privacy.Decrypt(d.appointmentName);
                    ap.Date = d.appointmentDate;
                    ap.Specialist = d.appointmentSpecialist;
                    ap.Prescription = d.appointmentPrescription;

                    appointmentList.Add(ap);
                }
                
               
            }
            return View(appointmentList);
        }

       
	}
}