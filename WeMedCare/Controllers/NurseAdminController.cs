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
    public class NurseAdminController : Controller
    {
        private MedicalContext db = new MedicalContext();
        PrivacyController privacy=new PrivacyController();

        // GET: /NurseAdmin/
        public ActionResult Index()
        {
            List<Nurse> nurses = db.Nurses.ToList();
            foreach (Nurse nurse in nurses)
            {
                nurse.Name = privacy.Decrypt(nurse.Name);
                nurse.Designation = privacy.Decrypt(nurse.Designation);
                nurse.Email = privacy.Decrypt(nurse.Email);
                nurse.Number = privacy.Decrypt(nurse.Number);
            }
            return View(nurses);
        }

        // GET: /NurseAdmin/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Nurse nurse = db.Nurses.Find(id);
            if (nurse == null)
            {
                return HttpNotFound();
            }
            return View(nurse);
        }

        // GET: /NurseAdmin/Create
        public ActionResult Create()
        {
            ViewBag.Create = "active";
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
        // POST: /NurseAdmin/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include="Id,Name,Designation,Email,Password,Number")] Nurse nurse)
        {
            nurse.Email = privacy.Encrypt(nurse.Email);
            var e = db.Doctors.Where(c => c.Email == nurse.Email).ToList().Count;
            if (e==0)
            {
                if (ModelState.IsValid)
                {
                    nurse.Name = privacy.Encrypt(nurse.Name);
                    nurse.Password = EncodePassword(nurse.Password);
                    nurse.Designation = privacy.Encrypt(nurse.Designation);
                    nurse.Number = privacy.Encrypt(nurse.Number);
                    db.Nurses.Add(nurse);
                    db.SaveChanges();
                    
                }
                ViewBag.Message = "Insert Values Successfully";
            }
            else
            {
                ViewBag.Message = "Email Already exists";
            }

            return View(nurse);
        }

        // GET: /NurseAdmin/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Nurse nurse = db.Nurses.Find(id);
            if (nurse == null)
            {
                return HttpNotFound();
            }
            nurse.Name = privacy.Decrypt(nurse.Name);
            nurse.Designation = privacy.Decrypt(nurse.Designation);
            nurse.Email = privacy.Decrypt(nurse.Email);
            nurse.Number = privacy.Decrypt(nurse.Number);
            
            return View(nurse);
        }

        // POST: /NurseAdmin/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include="Id,Name,Designation,Email,Password,Number")] Nurse nurse)
        {
            if (ModelState.IsValid)
            {
                nurse.Name = privacy.Encrypt(nurse.Name);
                nurse.Email = privacy.Encrypt(nurse.Email);

                nurse.Password = EncodePassword(nurse.Password);
                nurse.Designation = privacy.Encrypt(nurse.Designation);
                nurse.Number = privacy.Encrypt(nurse.Number);
                db.Entry(nurse).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Message = "Update Values Successfully";
            return View(nurse);
        }

        // GET: /NurseAdmin/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Nurse nurse = db.Nurses.Find(id);
            if (nurse == null)
            {
                return HttpNotFound();
            }
            nurse.Name = privacy.Decrypt(nurse.Name);
            nurse.Designation = privacy.Decrypt(nurse.Designation);
            nurse.Email = privacy.Decrypt(nurse.Email);
            nurse.Number = privacy.Decrypt(nurse.Number);
            return View(nurse);
        }

        // POST: /NurseAdmin/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Nurse nurse = db.Nurses.Find(id);
            db.Nurses.Remove(nurse);
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
