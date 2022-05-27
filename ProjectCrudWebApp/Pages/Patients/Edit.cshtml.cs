using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ProjectCrudWebApp.DataAccess;
using ProjectCrudWebApp.Models;
using System.ComponentModel.DataAnnotations;

namespace ProjectCrudWebApp.Pages.Patients
{
    public class EditModel : PageModel
    {
        [BindProperty]
        [Display(Name = "Name")]
        [Required]
        public string Name { get; set; }

        [BindProperty]
        [Display(Name = "DOB")]
        [DataType(DataType.Date)]
        [Required]
        public DateTime DOB { get; set; }

        [BindProperty]
        [Display(Name = "Gender")]
        [Required]
        public string Gender { get; set; }
        public string[] Genders = new[] { "Male", "Female", "Other" };

        [BindProperty]
        [Display(Name = "MobileNumber")]
        [Required]
        public string MobileNumber { get; set; }

        [BindProperty]
        [Display(Name = "Address")]
        [Required]
        public string Address { get; set; }

        [BindProperty]
        [Display(Name = "email")]
        [Required]
        public string email { get; set; }

        [BindProperty]
        [Display(Name = "Password")]
        [Required]
        public string Password { get; set; }

        public string SuccessMessage { get; set; }
        public string ErrorMessage { get; set; }
        public int Id { get; private set; }

        public EditModel()
        {
            SuccessMessage = "";
            Name = "";
            DOB = DateTime.Now.AddYears(-20);
            Gender = "";
            MobileNumber = "";
            Address = "";
            email = "";
            Password = "";
        }

        public void OnGet(int id)
        {
            Id = id;

            if (Id <= 0)
            {
                ErrorMessage = "Invalid Id";
                return;
            }

            var patientData = new PatientDataAccess();
            var pat = patientData.GetPatientById(id);

            if (pat != null)
            {
                Name = pat.Name;
                DOB = pat.DOB;
                Gender = pat.Gender;
                MobileNumber = pat.MobileNumber;
                Address = pat.Address;
                email = pat.email;
                Password = pat.Password;
            }
            else
            {
                ErrorMessage = "No Record found with that Id";
            }
        }
        public void OnPost()
        {
            //1. Validation
            if (!ModelState.IsValid)
            {
                ErrorMessage = "Invalid Data. Please correct and try again.";
                return;
            }

            //2. Data Access (Calling DB)
            var patientData = new PatientDataAccess();
            var patToUpdate = new PatientDataModel { Id = Id, Name = Name,DOB=DOB, Gender=Gender,MobileNumber=MobileNumber,Address=Address,email=email,Password=Password};
            var updatedPatient = patientData.Update(patToUpdate);

            //3. Check Data and Return Result
            if (updatedPatient != null)
            {
                SuccessMessage = $"Patient {updatedPatient.Id} updated successfully.";

                Response.Headers.Add("REFRESH", "3;URL=/Patients/List");

            }
            else
            {
                ErrorMessage = $"Error! Updating Patient.";
            }
        }

    }
}
