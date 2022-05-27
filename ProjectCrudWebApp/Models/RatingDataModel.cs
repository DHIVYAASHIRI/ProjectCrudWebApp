namespace ProjectCrudWebApp.Models
{
    public class RatingDataModel
    {
        public int DoctorId { get; set; }
        public int Ratings { get; set; }


        //constructor
        public RatingDataModel()
        {
            DoctorId = 0;
            Ratings = 0;
        }
    }
}
