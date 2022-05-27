using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using ProjectCrudWebApp.DataAccess;
using ProjectCrudWebApp.Models;
using System.ComponentModel.DataAnnotations;

namespace ProjectCrudWebApp.Pages.Appointments
{
    public class AddModel : PageModel
    { 
        [BindProperty]
        [Display(Name = "AppointmentDate")]
        [DataType(DataType.DateTime)]
        [Required]
        public DateTime AppointmentDate { get; set; }

        [BindProperty]
        [Display(Name = "AppointmentStatus")]
       
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


        public AddModel()
        {
           
            AppointmentDate = DateTime.Now;
            AppointmentStatus = "Pending";
            DoctorList = GetDoctors();
            PatientList = GetPatients();
            SuccessMessage = "";
            ErrorMessage = "";
        }
    
    public void OnGet()
    {
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

        public void OnPost()
    {
        if (!ModelState.IsValid)
        {
            ErrorMessage = "Invalid Data";
            return;
        }
       
            DoctorList = GetDoctors();
            PatientList = GetPatients();
            var appointmentData = new AppointmentDataAccess();
        var newAppointment = new AppointmentDataModel {AppointmentDate=AppointmentDate, AppointmentStatus=AppointmentStatus, PatientId = SelectedPatientId, DoctorId = SelectedDoctorId };
        var insertedAppointment = appointmentData.Insert(newAppointment);

        if (insertedAppointment != null && insertedAppointment.Id > 0)
        {
            SuccessMessage = $"Successfully Booked Your Slot.Keep Checking Your Appointment Status.And Your UniqueId is: {SelectedPatientId}.ThankYou!";
            ModelState.Clear();
        }
        else
        {
            ErrorMessage = "Error in Booking Your Appointment.Please Try Again";

        }

    }
}
}

