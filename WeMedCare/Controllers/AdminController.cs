using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WeMedCare.Context;
using WeMedCare.Models;

namespace WeMedCare.Controllers
{
    public class AdminController : Controller
    {
        PrivacyController privacy=new PrivacyController();
        //
        // GET: /Admin/
        public ActionResult NurseAssignToWard()
        {
            ViewBag.NurseAssignToWard = "active";
            List<Ward> wards;
            List<Nurse> nurses = new List<Nurse>();
            using (var db = new MedicalContext())
            {
                var a = db.Nurses.Where(c => c.Wardassign== 0).Select(c => new { c.Name, c.Id }).ToList();
                foreach (var b in a)
                {
                    Nurse n = new Nurse();
                    n.Id = b.Id;
                    n.Name = privacy.Decrypt(b.Name);
                    nurses.Add(n);

                }
                ViewBag.Nurse = nurses;
                wards = db.Wards.ToList();
            }
            return View(wards);
        }
        public JsonResult GetAllNurseByWard(int id)
        {
            List<Nurse> nurses=new List<Nurse>();
            using (var db = new MedicalContext())
            {
                nurses = db.Nurses.Where(c => c.Wardassign == id).ToList();
            }
            foreach (Nurse a in nurses)
            {
                a.Name = privacy.Decrypt(a.Name);
                a.Designation = privacy.Decrypt(a.Designation);

            }
            return Json(nurses);
        }
        public JsonResult ReleaseNurseFromWard(int id)
        {
            using (var db = new MedicalContext())
            {
                Nurse n = db.Nurses.Find(id);
                n.Wardassign = 0;
                db.SaveChanges();
            }
            return Json(1);

        }
        public JsonResult AssignIntoWard(int nurseId, int wardId)
        {
            using (var db = new MedicalContext())
            {
                Nurse nurse = db.Nurses.Find(nurseId);
                nurse.Wardassign = wardId;
                db.SaveChanges();
            }
            return Json(1);
        }
        public ActionResult PatientRealeseFromWard()
        {
            ViewBag.PatientRealeseFromWard = "active";
            List<Ward> wards = new List<Ward>();
            using (var db = new MedicalContext())
            {
                wards = db.Wards.ToList();
            }
            ViewBag.Wards = wards;
            return View();
        }
        public JsonResult GetAllAdmitPatientByWard(int id)
        {
            List<Prescription> prescriptions = new List<Prescription>();
            using (var db = new MedicalContext())
            {
                var info = from a in db.Appointment
                           join p in db.Registers
                               on a.PatientId equals p.Id
                           join d in db.Doctors
                               on a.DoctorId equals d.Id
                           where a.WardId == id
                           select new
                           {
                               appointmentId = a.Id,
                               PatientName = p.Name,
                               PatientAge = p.Age,
                               patientAddress = p.Address,
                               DoctorName = d.Name,
                           };

                foreach (var k in info)
                {
                    Prescription pres = new Prescription();
                    pres.Id = k.appointmentId;
                    pres.PatientName = privacy.Decrypt(k.PatientName);
                    pres.PatientAge = k.PatientAge;
                    pres.PatientAddress = privacy.Decrypt(k.patientAddress);
                    pres.DoctorName = privacy.Decrypt(k.DoctorName);
                    prescriptions.Add(pres);
                }
            }
            return Json(prescriptions);
        }
        public JsonResult ReleasePatientFromWards(int id, int wardId)
        {
            using (var db = new MedicalContext())
            {
                PatientAppointmentModel appoint = db.Appointment.Find(id);
                appoint.WardId = 0;
                db.SaveChanges();
            }
            return Json(1);
        }

	}
}