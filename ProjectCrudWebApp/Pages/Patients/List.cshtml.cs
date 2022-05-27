using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ProjectCrudWebApp.DataAccess;
using ProjectCrudWebApp.Models;

namespace ProjectCrudWebApp.Pages.Patients
{
    [Authorize(Roles = "Admin")]
    public class ListModel : PageModel
    {
       
        public List<PatientDataModel> Patients { get; set; }
        public string SuccessMessage { get; set; }
        public string ErrorMessage { get; set; }

        public ListModel()
        {
            SuccessMessage = "";
            ErrorMessage = "";
            Patients = new List<PatientDataModel>();
        }
        public void OnGet()
        {
            var patientData = new PatientDataAccess();
            Patients = patientData.GetAll();
        }
    }
}
