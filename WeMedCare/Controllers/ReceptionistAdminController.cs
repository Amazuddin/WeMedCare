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
    public class ReceptionistAdminController : Controller
    {
        private MedicalContext db = new MedicalContext();
        PrivacyController privacy = new PrivacyController();
        // GET: /ReceptionistAdmin/
        public ActionResult Index()
        {
            List<Receptionist> receptionists = db.Receptionist.ToList();
            foreach (Receptionist rep in receptionists)
            {

                rep.Name = privacy.Decrypt(rep.Name);
                rep.Address = privacy.Decrypt(rep.Address);
                rep.PhoneNo = privacy.Decrypt(rep.PhoneNo);
                rep.Email = privacy.Decrypt(rep.Email);
            }
            return View(receptionists);
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
        // GET: /ReceptionistAdmin/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Receptionist receptionist = db.Receptionist.Find(id);
            if (receptionist == null)
            {
                return HttpNotFound();
            }
            return View(receptionist);
        }

        // GET: /ReceptionistAdmin/Create
        public ActionResult Create()
        {
            ViewBag.Create = "active";
            return View();
        }

        // POST: /ReceptionistAdmin/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include="Id,Name,PhoneNo,Address,Email,Password")] Receptionist receptionist)
        {
            receptionist.Email = privacy.Encrypt(receptionist.Email);
            var e = db.Doctors.Where(c => c.Email == receptionist.Email).ToList().Count;
            if (e==0)
            {
                if (ModelState.IsValid)
                {
                    receptionist.Name = privacy.Encrypt(receptionist.Name);
                    receptionist.Address = privacy.Encrypt(receptionist.Address);
                    receptionist.PhoneNo = privacy.Encrypt(receptionist.PhoneNo);
                    receptionist.Password = EncodePassword(receptionist.Password);
                    db.Receptionist.Add(receptionist);
                    db.SaveChanges();
                    //return RedirectToAction("Index");
                }
                ViewBag.Message = "Insert Values Successfully";
            }
           
            else
            {
                ViewBag.Message = "Email Already exists";
            }
            return View(receptionist);
        }

        // GET: /ReceptionistAdmin/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Receptionist receptionist = db.Receptionist.Find(id);
            if (receptionist == null)
            {
                return HttpNotFound();
            }
            receptionist.Name = privacy.Decrypt(receptionist.Name);
            receptionist.Address = privacy.Decrypt(receptionist.Address);
            receptionist.Email = privacy.Decrypt(receptionist.Email);
            receptionist.PhoneNo = privacy.Decrypt(receptionist.PhoneNo);
            return View(receptionist);
        }

        // POST: /ReceptionistAdmin/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include="Id,Name,PhoneNo,Address,Email,Password")] Receptionist receptionist)
        {
            if (ModelState.IsValid)
            {
                receptionist.Name = privacy.Encrypt(receptionist.Name);
                receptionist.Email = privacy.Encrypt(receptionist.Email);
                receptionist.Password = EncodePassword(receptionist.Password);
                receptionist.Address = privacy.Encrypt(receptionist.Address);
                receptionist.PhoneNo = privacy.Encrypt(receptionist.PhoneNo);
                db.Entry(receptionist).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Message = "Update Values Successfully";
            return View(receptionist);
        }

        // GET: /ReceptionistAdmin/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Receptionist receptionist = db.Receptionist.Find(id);
            if (receptionist == null)
            {
                return HttpNotFound();
            }
            receptionist.Name = privacy.Decrypt(receptionist.Name);
            receptionist.Address = privacy.Decrypt(receptionist.Address);
            receptionist.Email = privacy.Decrypt(receptionist.Email);
            receptionist.PhoneNo = privacy.Decrypt(receptionist.PhoneNo);
            return View(receptionist);
        }

        // POST: /ReceptionistAdmin/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Receptionist receptionist = db.Receptionist.Find(id);
            db.Receptionist.Remove(receptionist);
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
