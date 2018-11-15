using System.Collections.Generic;
using System.Web.Mvc;

namespace Patients.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public JsonResult Query(string id)
        {
            List<Patient> patients = new List<Patient>
            {
                new Patient("Roy"),
                new Patient("Lydia")
            };

            return Json(patients, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult GetPatient(string patient)
        {
            List<Patient> patients = new List<Patient>
            {
                new Patient(patient),
            };

            return Json(patients, JsonRequestBehavior.AllowGet);
        }

        public class Patient 
        {
            public string Name { get; set;}
            public Patient(string name)
            {
                Name = name;
            }
        }
    }
}