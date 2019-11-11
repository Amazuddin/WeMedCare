using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WeMedCare.Models
{
    public class Nurse
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Designation { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Number { get; set; }
        public int Wardassign { get; set; }
       
    }
}