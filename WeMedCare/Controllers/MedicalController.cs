using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WeMedCare.Manger;
using WeMedCare.Models;

namespace WeMedCare.Controllers
{
    public class MedicalController : Controller
    {
        private SpecialistManager specialistManager=new SpecialistManager();
       
         //GET: /Medical/
        public ActionResult IndexHomepage()
        {
            return View();
        }
         [HttpGet]
        public ActionResult specialist()
         {
             ViewBag.specialistlist = specialistManager.GetAllSpecialist();
            return View();
        }

        public JsonResult GetAllDoctorInfoById(int id)
        {
            List<DoctorDetailsModel> doctorDetails = specialistManager.GetAllDoctorInfoById(id);
            return Json(doctorDetails);
        }
        public ActionResult Tutorial()
        {
            return View();
        }
        public ActionResult Reciption()
        {
            return View();
        }
        [HttpGet]
        public ActionResult Appointment()
        {
            ViewBag.message = null;
            ViewBag.specialistlist = specialistManager.GetAllSpecialist();
            return View();
        }
        public JsonResult GetAllDoctorNameById(int id)
        {
            List<DoctorDetailsModel> doctorDetails = specialistManager.GetAllDoctorInfoById(id);
            return Json(doctorDetails);
        }
        public ActionResult Appointment(PatientAppointmentModel patientAppointment)
        {
            ViewBag.specialistlist = specialistManager.GetAllSpecialist();
            ViewBag.message = specialistManager.Appointment(patientAppointment);
            
            return View();
        }
        public ActionResult Blood()
        {
            return View();

        }
        [HttpGet]
        public ActionResult BloodDonorRegistration()
        {
            ViewBag.message = null;
            ViewBag.bloodgroup = specialistManager.GetAllBloodGroup();
            return View();

        }
        public ActionResult BloodDonorRegistration(BloodDonorModel bloodDonor)
        {
           
            ViewBag.bloodgroup = specialistManager.GetAllBloodGroup();
            ViewBag.message = specialistManager.SaveDonor(bloodDonor);
            return View();

        }
        [HttpGet]
        public ActionResult BloodDonorList()
        {
            ViewBag.bloodlist = specialistManager.GetAllBloodGroup();
            return View();

        }
        public JsonResult GetAllDonorInfoById(int id)
        {
            List<BloodDonorModel> bloodDonorDetails = specialistManager.GetAllDonorInfoById(id);
            return Json(bloodDonorDetails);
        }
       
	}
}