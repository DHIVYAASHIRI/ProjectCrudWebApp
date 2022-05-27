using ProjectCrudWebApp.Helpers;
using ProjectCrudWebApp.Models;
using ProjectCrudWebApp.Pages.Doctors.Models;
using System.Data.SqlClient;

namespace ProjectCrudWebApp.DataAccess
{
    public class DoctorDataAccess
    {
        public string ErrorMessage { get; private set; }
        public DoctorDataAccess()
        {
            ErrorMessage = "";
        }

        //Get all Doctors
        public List<DoctorDataModel> GetAll()
        {
            try
            {
                ErrorMessage = string.Empty;
                ErrorMessage = "";

                List<DoctorDataModel> doctors = new List<DoctorDataModel>();

                using (SqlConnection conn = DataBase.GetConnection())
                {
                    conn.Open();
                    var sqlStmt = "Select Id,DoctorName,Gender,MobileNumber,Specialization,AvailableDays,AvailableTime from dbo.Doctor";
                    using (SqlCommand cmd = new SqlCommand(sqlStmt, conn))
                    {
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read() == true)
                            {
                                DoctorDataModel doctor = new DoctorDataModel();
                                doctor.Id = reader.GetInt32(0);
                                doctor.DoctorName = reader.GetString(1);
                                doctor.Gender = reader.GetString(2);
                                doctor.MobileNumber = reader.GetString(3);
                                doctor.Specialization = reader.GetString(4);
                                doctor.AvailableDays = reader.GetString(5);
                                doctor.AvailableTime = reader.GetString(6);

                                doctors.Add(doctor);
                            }
                        }
                    }
                }

                return doctors;
            }
            catch (Exception ex)
            {
                ErrorMessage = ex.Message;
                return null;
            }
        }


        //Get Doctor By Id
        public DoctorDataModel GetDoctortById(int id)
        {
            try
            {
                DoctorDataModel doctor = null;
                using (SqlConnection conn = DataBase.GetConnection())
                {
                    conn.Open();
                    string sqlStmt = $"Select Id, DoctorName, Gender, MobileNumber, Specialization, AvailableDays, AvailableTime from Doctor where Id = {id}";
                    using (SqlCommand cmd = new SqlCommand(sqlStmt, conn))
                    {
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.Read() == true)
                            {
                                doctor = new DoctorDataModel();
                                doctor.Id = reader.GetInt32(0);
                                doctor.DoctorName = reader.GetString(1);
                                doctor.Gender = reader.GetString(2);
                                doctor.MobileNumber = reader.GetString(3);
                                doctor.Specialization = reader.GetString(4);
                                doctor.AvailableDays = reader.GetString(5);
                                doctor.AvailableTime = reader.GetString(6);
                            }
                        }
                    }
                }

                return doctor;
            }
            catch (Exception ex)
            {
                ErrorMessage = ex.Message;
                return null;
            }
        }
        public List<DoctorDataModel> GetDoctorsByName(string doctorname)
        {
            try
            {
                List<DoctorDataModel> doctors = new List<DoctorDataModel>();
                using (SqlConnection conn = DataBase.GetConnection())
                {
                    conn.Open();
                    string sqlStmt = $"Select Id, DoctorName, Gender,MobileNumber,Specialization,AvailableDays,AvailableTime from Doctor where DoctorName like '%{doctorname}%'";
                    using (SqlCommand cmd = new SqlCommand(sqlStmt, conn))
                    {
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read() == true)
                            {
                                DoctorDataModel doctor = new DoctorDataModel();
                                doctor.Id = reader.GetInt32(0);
                                doctor.DoctorName = reader.GetString(1);
                                doctor.Gender = reader.GetString(2);
                                doctor.MobileNumber = reader.GetString(3);
                                doctor.Specialization = reader.GetString(4);
                                doctor.AvailableDays = reader.GetString(5);
                                doctor.AvailableTime = reader.GetString(6);

                                doctors.Add(doctor);

                            }
                        }
                    }
                }

                return doctors;
            }
            catch (Exception ex)
            {
                ErrorMessage = ex.Message;
                return null;
            }
        }


        //Insert new Doctor
        public DoctorDataModel Insert(DoctorDataModel newDoctor)
        {
            try
            {
                ErrorMessage = string.Empty;
                ErrorMessage = String.Empty;
                using (SqlConnection conn = DataBase.GetConnection())
                {
                    conn.Open();
                    string sqlStmt = $"INSERT INTO dbo.Doctor (DoctorName,Gender, MobileNumber, Specialization, AvailableDays,AvailableTime) VALUES ('{newDoctor.DoctorName}', '{newDoctor.Gender}', '{newDoctor.MobileNumber}', '{newDoctor.Specialization}', '{newDoctor.AvailableDays}', '{newDoctor.AvailableTime}'); SELECT SCOPE_IDENTITY();";

                    using (SqlCommand cmd = new SqlCommand(sqlStmt, conn))
                    {
                        int idInserted = Convert.ToInt32(cmd.ExecuteScalar());
                        if (idInserted > 0)
                        {
                            newDoctor.Id = idInserted;
                            return newDoctor;
                        }
                    }
                }
                return null;
            }
            catch (Exception ex)
            {
                this.ErrorMessage = ex.Message;
                return null;
            }
        }

        //Update Doctor
        public DoctorDataModel Update(DoctorDataModel updDoctor)
        {
            try
            {
                ErrorMessage = "";
                using (SqlConnection conn = DataBase.GetConnection())
                {
                    conn.Open();
                    string sqlStmt = $"UPDATE dbo.Doctor SET DoctorName = '{updDoctor.DoctorName}', " +
                        $"Gender = '{updDoctor.Gender}' ," +
                        $"MobileNumber = '{updDoctor.MobileNumber}' ," +
                        $"Specialization = '{updDoctor.Specialization}', " +
                        $"AvailableDays = '{updDoctor.AvailableDays}', " +
                        $"AvailableTime = '{updDoctor.AvailableTime}' " +
                        $"where Id = {updDoctor.Id}";

                    using (SqlCommand cmd = new SqlCommand(sqlStmt, conn))
                    {
                        int numOfRows = cmd.ExecuteNonQuery();
                        if (numOfRows > 0)
                        {
                            return updDoctor;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                ErrorMessage = ex.Message;
            }
            return null;
        }

        //Delete Doctor
        public int Delete(int id)
        {
            try
            {
                ErrorMessage = String.Empty;
                int numOfRows = 0;

                using (SqlConnection conn = DataBase.GetConnection())
                {
                    conn.Open();
                    string sqlStmt = $"DELETE FROM Doctor Where Id = {id}";

                    using (SqlCommand cmd = new SqlCommand(sqlStmt, conn))
                    {
                        numOfRows = cmd.ExecuteNonQuery();
                    }
                }
                return numOfRows;
            }
            catch (Exception ex)
            {
                ErrorMessage = ex.Message;
                return 0;
            }
        }
    }
}


