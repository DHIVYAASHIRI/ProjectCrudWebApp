namespace ProjectCrudWebApp.Models
{
    public class AppointmentDataModel
    {
        public int Id { get; set; }
        public DateTime AppointmentDate { get; set; }
        public string AppointmentStatus { get; set; }
        public int PatientId { get;  set; }
        public int DoctorId { get;  set; }
        public string Name { get; internal set; }
        public string DoctorName { get; internal set; }

        //constructor

        public AppointmentDataModel()
        {
            Id = -1;
            AppointmentDate = DateTime.Now;
            AppointmentStatus = "";
            PatientId = -1;
            DoctorId = -1;
        }
        public bool IsValid()
        {
            if(AppointmentDate >= DateTime.Now)
            {
                return true;
            }
            return false;
        }


    }
}
