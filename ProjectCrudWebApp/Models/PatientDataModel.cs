namespace ProjectCrudWebApp.Models
{
    public class PatientDataModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime DOB { get; set; }
        public string Gender { get; set; }
        public string MobileNumber { get; set; }
        public string Address { get; set; }
        public string email { get; set; }
        public string Password { get; set; }

        //Constructor
        public PatientDataModel()
        {
            Id = 0;
            Name = "";
            DOB = DateTime.Now;
            Gender = "";
            MobileNumber = "";
            Address = "";
            email = "";
            Password = "";
        }
        public bool IsValid()
        {
            if (MobileNumber == null || MobileNumber.Trim().Length > 10)
            {
                return false;
            }
          

            if(Password.Trim().Length != 6)
            {
                return false;
            }

            return true;
        }
    }
}
