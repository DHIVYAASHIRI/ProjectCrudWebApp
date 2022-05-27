using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using ProjectCrudWebApp.DataAccess;
using ProjectCrudWebApp.Models;
using ProjectCrudWebApp.Pages.Doctors.Models;
using System.ComponentModel.DataAnnotations;

namespace ProjectCrudWebApp.Pages.Doctors
{
    public class AddModel : PageModel
    { 
        [BindProperty]
        [Display(Name = "DoctorName")]
        [Required]
        public string DoctorName { get; set; }
        
        [BindProperty]
        [Display(Name = "Gender")]
        [Required]
        public string Gender { get; set; }

        public string[] Genders = new[] { "Male", "Female", "Other" };

        [BindProperty]
        [Display(Name = "Mobile Number")]
        [Required]
        public string MobileNumber { get; set; }

        [BindProperty]
        [Display(Name = "Specialization")]
        [Required]
        public List<SelectListItem> Specializations { get; set; }
        [BindProperty]
        public string Specialization { get; set; }

        [BindProperty]
        [Display(Name = "AvailableDays")]
        [Required]
        public string AvailableDays { get; set; }
      


        [BindProperty]
        [Display(Name = "AvailableTime")]
       
        public String AvailableTime { get; set; }

        public string SuccessMessage { get; set; }
        public string ErrorMessage { get; set; }

        public AddModel()
        {
            DoctorName = "";
            Gender = "";
            MobileNumber = "";
            Specializations = GetSpecialization();
            AvailableDays = "";
            AvailableTime = "";
            SuccessMessage = "";
            ErrorMessage = "";
        }
        private List<SelectListItem> GetSpecialization()
        {
            var selectitems = new List<SelectListItem>();
            selectitems.Add(new SelectListItem { Text = "EyeSpecialist", Value = "EyeSpecialist" });
            selectitems.Add(new SelectListItem { Text = "Dermetologist", Value = "Dermetologist" });
            selectitems.Add(new SelectListItem { Text = "Dentist", Value = "Dentist" });
            selectitems.Add(new SelectListItem { Text = "Psychiartist", Value = "Psychiartist" });
            selectitems.Add(new SelectListItem { Text = "Pediatrician", Value = "Pediatrician" });
            selectitems.Add(new SelectListItem { Text = "Surgeon", Value = "Surgeon" });
            selectitems.Add(new SelectListItem { Text = "Cardiologist", Value = "Cardiologist" });
            selectitems.Add(new SelectListItem { Text = "Gynaecologist", Value = "Gynaecologist" });
            selectitems.Add(new SelectListItem { Text = "Urologist", Value = "Urologist" });
            selectitems.Add(new SelectListItem { Text = "Nuerologist", Value = "Nuerologist" });

         return selectitems;

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
            Specializations = GetSpecialization();
            var doctorData = new DoctorDataAccess();
            var newDoctor = new DoctorDataModel { DoctorName = DoctorName, Gender = Gender,MobileNumber = MobileNumber, Specialization= Specialization, AvailableDays=AvailableDays, AvailableTime=AvailableTime};
            var insertedDoctor = doctorData.Insert(newDoctor);

            if (insertedDoctor != null && insertedDoctor.Id > 0)
            {
                SuccessMessage = $"Successfully Added New Doctor {insertedDoctor.Id}";
                ModelState.Clear();
            }
            else
            {
                ErrorMessage = "Error! Add Falied.Please Try Again";

            }

        }
    }
}
