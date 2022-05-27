namespace ProjectCrudWebApp.Pages.Doctors.Models
{
    public class DoctorDataModel
    {
        public int Id { get; set; }
        public string DoctorName { get; set; }
        public string Gender { get; set; }
        public string email { get; set; }
        public string MobileNumber { get; set; }
        public string Specialization { get; set; }
        public string AvailableDays { get; set; }
        public String AvailableTime { get; set; }

        //Constructor
        public  DoctorDataModel()
        {
            Id= 0;
            DoctorName = "";
            Gender = "";
            email = "";
            MobileNumber = "";
            Specialization = "";
            AvailableDays = "";
            AvailableTime = "";
        }
        public bool IsValid()
        {
            if (DoctorName == null || DoctorName.Trim().Length > 50 || DoctorName.Trim() == "")
            {
                return false;
            }
            return true;
        }
    }
}
