using Patients.Models;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web.Mvc;

namespace Patients.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        { 
            return View();
        }

        public JsonResult Query(string name)
        {
            DataTable patients = Database.Instance.GetPatients(name);

            var patientList = patients.AsEnumerable().Select(r => new Patient(r.Field<string>("lastName") + ", " +  
                                                                      r.Field<string>("firstName"))
            { Id = r.Field<string>("id") }).ToList();

            return Json(patientList, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult GetPatient(string patientId)
        {
            DataTable patient = Database.Instance.GetPatient(patientId);

            return Json( patient.AsEnumerable().Select(r => new Patient()
                        {
                            FirstName = r.Field<string>("firstName"),
                            LastName = r.Field<string>("lastName"),
                            DOB = r.Field<string>("dateOfbirth"),
                            PhoneNumber = r.Field<string>("phoneNumber")
                        }).FirstOrDefault());
        }

        public class Patient 
        {
            public string Id;
            public string Name { get; set;}
            public string FirstName { get; set; }
            public string LastName { get; set; }
            public string DOB { get; set; }
            public string PhoneNumber { get; set; }
            public Patient() {  }
            public Patient(string name)
            {
                Name = name;
            }
        }
    }
}