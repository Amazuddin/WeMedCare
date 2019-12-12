using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.Mvc;
using WeMedCare.Models;
using WeMedCare.Context;

namespace WeMedCare.Controllers
{
    public class DoctorAdminController : Controller
    {
        private MedicalContext db = new MedicalContext();
        PrivacyController privacy=new PrivacyController();

        // GET: /DoctorAdmin/
        public ActionResult Index()
        {
                 
            List<DoctorDetailsModel> doctors = db.Doctors.ToList();
            foreach (DoctorDetailsModel doc in doctors)
            {

                doc.Name = privacy.Decrypt(doc.Name);
                doc.Degree = privacy.Decrypt(doc.Degree);
                doc.Schedule = privacy.Decrypt(doc.Schedule);
                doc.Email = privacy.Decrypt(doc.Email);
            }
            return View(doctors);
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

        // GET: /DoctorAdmin/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DoctorDetailsModel doctordetailsmodel = db.Doctors.Find(id);
            if (doctordetailsmodel == null)
            {
                return HttpNotFound();
            }
            return View(doctordetailsmodel);
        }

        // GET: /DoctorAdmin/Create
        public ActionResult Create()
        {
            //ViewBag.Info = error;
            
            ViewBag.Specialist = db.Specialist.ToList();
            return View();
        }

        // POST: /DoctorAdmin/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Name,Degree,Fees,Schedule,SpecialistId,Email,Password")] DoctorDetailsModel doctordetailsmodel, int id)
        {
            doctordetailsmodel.Email = privacy.Encrypt(doctordetailsmodel.Email);
            var e = db.Doctors.Where(c => c.Email == doctordetailsmodel.Email).ToList().Count;
            if (e==0)
            {
                if (ModelState.IsValid)
                {
                    doctordetailsmodel.SpecialistId = id;
                    doctordetailsmodel.Name = privacy.Encrypt(doctordetailsmodel.Name);
                    doctordetailsmodel.Degree = privacy.Encrypt(doctordetailsmodel.Degree);
                    doctordetailsmodel.Schedule = privacy.Encrypt(doctordetailsmodel.Schedule);

                    doctordetailsmodel.Password = EncodePassword(doctordetailsmodel.Password);
                    db.Doctors.Add(doctordetailsmodel);
                    db.SaveChanges();

                }
                ViewBag.Message = "Insert Values Successfully";
            }
            else
            {
                ViewBag.Message = "Email Already exists";
            }
            ViewBag.Specialist = db.Specialist.ToList();
            return View(doctordetailsmodel);
            //return RedirectToAction("Create");
        }

        // GET: /DoctorAdmin/Edit/5
        public ActionResult Edit(int? id)
        {
            ViewBag.Specialist = db.Specialist.ToList();
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DoctorDetailsModel doctordetailsmodel = db.Doctors.Find(id);
            if (doctordetailsmodel == null)
            {
                return HttpNotFound();
            }
            doctordetailsmodel.Name = privacy.Decrypt(doctordetailsmodel.Name);
            doctordetailsmodel.Degree = privacy.Decrypt(doctordetailsmodel.Degree);
            doctordetailsmodel.Email = privacy.Decrypt(doctordetailsmodel.Email);
            doctordetailsmodel.Schedule = privacy.Decrypt(doctordetailsmodel.Schedule);
            return View(doctordetailsmodel);
        }

        // POST: /DoctorAdmin/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name,Degree,Fees,Schedule,SpecialistId,Email,Password")] DoctorDetailsModel doctordetailsmodel, int SpecialistId)
        {
            if (ModelState.IsValid)
            {
                doctordetailsmodel.SpecialistId = SpecialistId;
                doctordetailsmodel.Name = privacy.Encrypt(doctordetailsmodel.Name);
                doctordetailsmodel.Email = privacy.Encrypt(doctordetailsmodel.Email);
                doctordetailsmodel.Degree = privacy.Encrypt(doctordetailsmodel.Degree);
                doctordetailsmodel.Password = EncodePassword(doctordetailsmodel.Password);
                doctordetailsmodel.Schedule = privacy.Encrypt(doctordetailsmodel.Schedule);
                db.Entry(doctordetailsmodel).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Message = "Update Values Successfully";
            return View(doctordetailsmodel);
        }

        // GET: /DoctorAdmin/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DoctorDetailsModel doctordetailsmodel = db.Doctors.Find(id);
            if (doctordetailsmodel == null)
            {
                return HttpNotFound();
            }
            doctordetailsmodel.Name = privacy.Decrypt(doctordetailsmodel.Name);
            doctordetailsmodel.Degree = privacy.Decrypt(doctordetailsmodel.Degree);
            doctordetailsmodel.Email = privacy.Decrypt(doctordetailsmodel.Email);
            doctordetailsmodel.Schedule = privacy.Decrypt(doctordetailsmodel.Schedule);
            return View(doctordetailsmodel);
        }

        // POST: /DoctorAdmin/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            DoctorDetailsModel doctordetailsmodel = db.Doctors.Find(id);
            db.Doctors.Remove(doctordetailsmodel);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
