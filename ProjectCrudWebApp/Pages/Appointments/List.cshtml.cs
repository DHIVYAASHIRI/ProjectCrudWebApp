using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ProjectCrudWebApp.DataAccess;
using ProjectCrudWebApp.Models;

namespace ProjectCrudWebApp.Pages.Appointments
{
    public class ListModel : PageModel
    { 
        
        public List<AppointmentDataModel> Appointments { get; set; }
        public string SuccessMessage { get; set; }
        public string ErrorMessage { get; set; }


        public ListModel()
        {

            SuccessMessage = "";
            ErrorMessage = "";
            Appointments = new List<AppointmentDataModel>();
        }
        public void OnGet()
        {
            var appointmentData = new AppointmentDataAccess();
            Appointments = appointmentData.GetAll();
        }
    }
}


 
