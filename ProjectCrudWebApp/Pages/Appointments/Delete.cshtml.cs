using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ProjectCrudWebApp.DataAccess;

namespace ProjectCrudWebApp.Pages.Appointments
{
    public class DeleteModel : PageModel
    {
        [BindProperty(SupportsGet = true)]
        public int Id { get; set; }

        public bool ShowButton { get; set; }

        public int PatientId { get; set; }

        public string SuccessMessage { get; set; }
        public string ErrorMessage { get; set; }

        public DeleteModel()
        {
            PatientId = 0;
            SuccessMessage = "";
            ErrorMessage = "";
            ShowButton = true;
        }

        public void OnGet(int id)
        {
            Id = Id;

            if (Id <= 0)
            {
                ErrorMessage = "Invalid Id";
                return;
            }

            var appointmentData = new AppointmentDataAccess();
            var app = appointmentData.GetAppointmentById(id);

            if (app != null)
            {
               PatientId = app.PatientId;
            }
            else
            {
                ErrorMessage = "No Record found with that Id";
            }
        }

        public void OnPost()
        {
            if (!ModelState.IsValid)
            {
                ErrorMessage = "Invalid Data";
                return;
            }

            var appointmentData = new AppointmentDataAccess();
            var numOfRows = appointmentData.Delete(Id);
            if (numOfRows > 0)
            {
                SuccessMessage = $"Appointment of Unique Id-{Id} cancelled successfully!";
                ShowButton = false;
            }
            else
            {
                ErrorMessage = $"Error! Unable to Cancel Appointment{Id}";
            }
        }
    }
}

