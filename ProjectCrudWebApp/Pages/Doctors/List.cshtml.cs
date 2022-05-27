using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ProjectCrudWebApp.DataAccess;
using ProjectCrudWebApp.Models;
using ProjectCrudWebApp.Pages.Doctors.Models;

namespace ProjectCrudWebApp.Pages.Doctors
{
    [Authorize(Roles = "Admin")]
    public class ListModel : PageModel
    {
        [BindProperty]
        public string SearchText { get; set; }
        public List<DoctorDataModel> Doctors { get; set; }
        public string SuccessMessage { get; set; }
        public string ErrorMessage { get; set; }


        public  ListModel()
        {
            SuccessMessage = "";
            ErrorMessage = "";
            Doctors = new List<DoctorDataModel>();
        }
        public void OnGet()
        {
            var doctorData = new DoctorDataAccess();
            Doctors = doctorData.GetAll();
        }
        public void OnPostSearch()
        {
            //Validation
            if (!ModelState.IsValid)
            {
                ErrorMessage = $"Invalid Data";
                return;
            }

            if (string.IsNullOrEmpty(SearchText) || SearchText.Length < 3)
            {
                ErrorMessage = "Please input a search string of atleast 3 characters";
                return;
            }


            //Call DataAccess
            DoctorDataAccess doctorData = new DoctorDataAccess();
            Doctors = doctorData.GetDoctorsByName(SearchText);

            //Verify Data and Return
            if (Doctors != null && Doctors.Count > 0)
            {
                SuccessMessage = $"{Doctors.Count} Doctors found";
                ErrorMessage = "";
            }
            else
            {
                ErrorMessage = $"No Doctors found";
                SuccessMessage = "";
            }
        }

        public void OnPostClear()
        {
            SearchText = "";
            ModelState.Clear();

            var doctorData = new DoctorDataAccess();
            Doctors = doctorData.GetAll();
        }
    }
}

