using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ProjectCrudWebApp.DataAccess;

namespace ProjectCrudWebApp.Pages.Doctors
{
    public class DeleteModel : PageModel
    {
        [BindProperty(SupportsGet = true)]
        public int Id { get; set; }

        public bool ShowButton { get; set; }

        public string DoctorName { get; set; }

        public string SuccessMessage { get; set; }
        public string ErrorMessage { get; set; }

        public DeleteModel()
        {
            DoctorName = "";
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

            var doctorData = new DoctorDataAccess();
            var doc = doctorData.GetDoctortById(id);

            if (doc != null)
            {
                DoctorName = doc.DoctorName;
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

            var doctorData = new DoctorDataAccess();
            var numOfRows = doctorData.Delete(Id);
            if (numOfRows > 0)
            {
                SuccessMessage = $"Doctor {Id} deleted successfully!";
                ShowButton = false;
            }
            else
            {
                ErrorMessage = $"Error! Unable to delete Doctor {Id}";
            }
        }
    }
}
