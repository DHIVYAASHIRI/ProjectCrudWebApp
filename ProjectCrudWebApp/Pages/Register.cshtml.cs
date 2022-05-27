using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ProjectCrudWebApp.DataAccess;
using ProjectCrudWebApp.Models;
using System.ComponentModel.DataAnnotations;

namespace ProjectCrudWebApp.Pages
{
    public class RegisterModel : PageModel
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

        public RegisterModel()
        {
            SuccessMessage = "";
            Name = "";
            DOB = DateTime.Now.AddYears(-20);
            Gender = "";
            MobileNumber = "";
            Address = "";
            email = "";
            Password = "123456";

        }
        public void OnGet()
        {
        }
        public void OnPost()
        {
            if (!ModelState.IsValid)
            {
                ErrorMessage = "Invalid Data";
                return;
            }

            var patientData = new PatientDataAccess();
            var newPatient = new PatientDataModel { Name = Name, DOB = DOB, Gender = Gender, MobileNumber = MobileNumber, Address = Address, email = email, Password = Password };
            var insertedPatient = patientData.Insert(newPatient);

            if (insertedPatient != null && insertedPatient.Id > 0)
            {
                SuccessMessage = $"Congrats! Now you are a Part of Digital Health. And your Unique DigitalHealth Id is:{insertedPatient.Id}.Now Go Back and Login.Your Username is:DigitalHealthUser and Password is:DHuser";
                ModelState.Clear();
            }
            else
            {
                ErrorMessage = "Error! Register Falied.Please Try Again";

            }

        }
    }
}


