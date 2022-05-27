using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using ProjectCrudWebApp.DataAccess;
using ProjectCrudWebApp.Models;
using System.ComponentModel.DataAnnotations;

namespace ProjectCrudWebApp.Pages.Appointments
{
    [Authorize(Roles = "Admin")]
    public class EditModel : PageModel
    {
        [BindProperty(SupportsGet = true)]
        public int Id { get; set; }

        [BindProperty]
        [Display(Name = "AppointmentDate")]
        [DataType(DataType.Date)]
        [Required]
        public DateTime AppointmentDate { get; set; }

        [BindProperty]
        [Display(Name = "AppointmentStatus")]
        [Required]
        public string AppointmentStatus { get; set; }

        public List<SelectListItem> DoctorList { get; set; }
        [BindProperty]
        [Display(Name = "DoctorId")]
        public int SelectedDoctorId { get; set; }


        public List<SelectListItem> PatientList { get; set; }
        [BindProperty]
        [Display(Name = "PatientId")]
        public int SelectedPatientId { get; set; }
       

        public string SuccessMessage { get; set; }
        public string ErrorMessage { get; set; }
        public int PatientId { get; set; }
        public int DoctorId { get; set; }

        public EditModel()
        {
            PatientId = 0;
            DoctorId = 0;
            AppointmentDate = DateTime.Now;
            AppointmentStatus = "";
            DoctorList = GetDoctors();
            PatientList = GetPatients();
            SuccessMessage = "";
            ErrorMessage = "";
        }
        

        private List<SelectListItem> GetDoctors()
        {
            //Get Data from Data Access
            var doctorDataAccess = new DoctorDataAccess();
            var doctorList = doctorDataAccess.GetAll();

            //Create SelectListItem
            var doctorSelectList = new List<SelectListItem>();
            foreach (var doctor in doctorList)
            {
                doctorSelectList.Add(new SelectListItem
                {
                    Text = $"{doctor.DoctorName}-{doctor.Specialization}",
                    Value = doctor.Id.ToString(),
                });
            }
            return doctorSelectList;
        }
        private List<SelectListItem> GetPatients()
        {
            //Get Data from Data Access
            var patientDataAccess = new PatientDataAccess();
            var patientList = patientDataAccess.GetAll();

            //Create SelectListItem
            var patientSelectList = new List<SelectListItem>();
            foreach (var patient in patientList)
            {
                patientSelectList.Add(new SelectListItem
                {
                    Text = $"{patient.Name}",
                    Value = patient.Id.ToString(),
                });
            }
            return patientSelectList;
        }


        public void OnGet(int id)
        {
            Id = id;

            if (Id <= 0)
            {
                ErrorMessage = "Invalid Id";
                return;
            }

            var appointmentData = new AppointmentDataAccess();
            var app = appointmentData.GetAppointmentById(id);
            if (app != null)
            {
                AppointmentDate = app.AppointmentDate;
                AppointmentStatus = app.AppointmentStatus;
                PatientId = app.PatientId;
                DoctorId = app.DoctorId;

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


            var appointmentData = new AppointmentDataAccess();
            var appToUpdate = new AppointmentDataModel { Id = Id, AppointmentDate = AppointmentDate, AppointmentStatus = AppointmentStatus, PatientId = SelectedPatientId, DoctorId = SelectedDoctorId };
            var updatedAppointment = appointmentData.Update(appToUpdate);

            //check result
            if (updatedAppointment != null)
            {
                SuccessMessage = $"Appointment{updatedAppointment.Id} updated successfully!";
                Response.Headers.Add("REFRESH", "3;URL=/Appointments/List");
            }
            else
            {
                ErrorMessage = $"Error! Updating Appointment.";
            }
        }
    }
}

