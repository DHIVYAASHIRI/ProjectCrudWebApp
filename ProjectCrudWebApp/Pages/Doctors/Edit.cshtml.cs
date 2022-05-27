using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using ProjectCrudWebApp.DataAccess;
using System.ComponentModel.DataAnnotations;
using ProjectCrudWebApp.Models;
using ProjectCrudWebApp.Pages.Doctors.Models;

namespace ProjectCrudWebApp.Pages.Doctors
{
    public class EditModel : PageModel
    {
        [BindProperty(SupportsGet = true)]
        public int Id { get; set; }

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
       
        public string AvailableTime { get; set; }

        public string SuccessMessage { get; set; }
        public string ErrorMessage { get; set; }

        public  EditModel()
        {
            DoctorName = "";
            Gender = "";
            MobileNumber = "";
            Specializations = GetSpecialization();
            AvailableDays = "";
            AvailableTime ="";
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
        public void OnGet(int id)
        {
            Id = id;

            if (Id <= 0)
            {
                ErrorMessage = "Invalid Id";
                return;
            }

            var doctorData = new DoctorDataAccess();
            var doc = doctorData.GetDoctortById(id);
            if(doc !=null)
            {
                DoctorName = doc.DoctorName;
                Gender= doc.Gender;
                MobileNumber= doc.MobileNumber;
                Specialization= doc.Specialization;
                AvailableDays= doc.AvailableDays;
                AvailableTime= doc.AvailableTime;
            }
            else
            {
                ErrorMessage = "No Record found with that Id";
            }
        }


        public void OnPost()
        {
            //validation
            if (!ModelState.IsValid)
            {
                ErrorMessage = "Invalid data.Please Check and try again";
                return;
            }
            //data operation (Calling DataAccess)
           
           Specializations = GetSpecialization();
            var doctorData = new DoctorDataAccess();
            var docToUpdate = new DoctorDataModel { Id = Id, DoctorName = DoctorName, Gender = Gender,MobileNumber = MobileNumber, Specialization = Specialization, AvailableDays=AvailableDays,AvailableTime=AvailableTime};
            var updatedDoctor = doctorData.Update(docToUpdate);

            //check result
            if (updatedDoctor != null)
            {
                SuccessMessage = $"Doctor{updatedDoctor.Id} updated successfully!";
                Response.Headers.Add("REFRESH", "3;URL=/Doctors/List");
            }
            else
            {
                ErrorMessage = $"Error! Updating Doctor.";
            }
        }
    }
}
