using ProjectCrudWebApp.Helpers;
using ProjectCrudWebApp.Models;
using System.Data.SqlClient;

namespace ProjectCrudWebApp.DataAccess
{
    public class AppointmentDataAccess
    {
        public string ErrorMessage { get; set; }
        public List<AppointmentDataModel> GetAll()
        {
            try
            {
                ErrorMessage = string.Empty;
                ErrorMessage = "";

                List<AppointmentDataModel> appointments = new List<AppointmentDataModel>();

                using (SqlConnection conn = DataBase.GetConnection())
                {
                    conn.Open();
                    var sqlStmt = "SELECT A.Id AS AppointmentId,P.Id AS PatientId,P.Name AS PatientName,D.Id AS DoctorId,D.DoctorName AS DoctorName,A.AppointmentDate AS AppointmentDate,A.AppointmentStatus AS AppointmentStatus  " +
                                   "FROM[dbo].[Appointment] AS A " +
                                   "INNER JOIN [dbo].Patient AS P ON A.PatientId = P.Id " +
                                   "INNER JOIN [dbo].Doctor AS D ON A.DoctorId = D.Id " +
                                    "ORDER BY D.Id ";
                    using (SqlCommand cmd = new SqlCommand(sqlStmt, conn))
                    {
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read() == true)
                            {
                                AppointmentDataModel appointment = new AppointmentDataModel();
                                appointment.Id = reader.GetInt32(0);
                                appointment.PatientId = reader.GetInt32(1);
                                appointment.Name = reader.GetString(2);
                                appointment.DoctorId = reader.GetInt32(3);
                                appointment.DoctorName = reader.GetString(4);
                                appointment.AppointmentDate = reader.GetDateTime(5);
                                appointment.AppointmentStatus = reader.GetString(6);


                                appointments.Add(appointment);
                            }
                        }
                    }
                }

                return appointments;
            }
            catch (Exception ex)
            {
                ErrorMessage = ex.Message;
                return null;
            }
        }

        //Get Doctor By Id
        public AppointmentDataModel GetAppointmentById(int id)
        {
            try
            {
                AppointmentDataModel appointment = null;
                using (SqlConnection conn = DataBase.GetConnection())
                {
                    conn.Open();
                    string sqlStmt = $"Select Id,AppointmentDate,AppointmentStatus, PatientId, DoctorId from Appointment where Id = {id}";
                    using (SqlCommand cmd = new SqlCommand(sqlStmt, conn))
                    {
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.Read() == true)
                            {
                                appointment = new AppointmentDataModel();
                                appointment.Id = reader.GetInt32(0);
                                appointment.AppointmentDate = reader.GetDateTime(1);
                                appointment.AppointmentStatus = reader.GetString(2);
                                appointment.PatientId = reader.GetInt32(3);
                                appointment.DoctorId = reader.GetInt32(4);

                            }
                        }
                    }
                }

                return appointment;
            }
            catch (Exception ex)
            {
                ErrorMessage = ex.Message;
                return null;
            }
        }
        //Insert new Appointment
        public AppointmentDataModel Insert(AppointmentDataModel newAppointment)
        {
            try
            {
                ErrorMessage = string.Empty;
                ErrorMessage = String.Empty;
                using (SqlConnection conn = DataBase.GetConnection())
                {
                    conn.Open();
                    string sqlStmt = $"INSERT INTO dbo.Appointment ( AppointmentDate, AppointmentStatus,PatientId,DoctorId) VALUES ('{newAppointment.AppointmentDate.ToString("yyyy-MM-dd")}', '{newAppointment.AppointmentStatus}',{newAppointment.PatientId},{newAppointment.DoctorId}); SELECT SCOPE_IDENTITY();";

                    using (SqlCommand cmd = new SqlCommand(sqlStmt, conn))
                    {
                        int idInserted = Convert.ToInt32(cmd.ExecuteScalar());
                        if (idInserted > 0)
                        {
                            newAppointment.Id = idInserted;
                            return newAppointment;
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
        //Update Appointment
        public AppointmentDataModel Update(AppointmentDataModel updAppointment)
        {
            try
            {
                ErrorMessage = "";
                using (SqlConnection conn = DataBase.GetConnection())
                {
                    conn.Open();
                    string sqlStmt = $"UPDATE dbo.Appointment SET AppointmentDate = '{ updAppointment.AppointmentDate.ToString("yyyy-MM-dd ")}'," +
                        $"AppointmentStatus = '{updAppointment.AppointmentStatus}', " +
                        $"PatientId = {updAppointment.PatientId},"+
                         $"DoctorId = {updAppointment.DoctorId} " + 
                        $"where Id = {updAppointment.Id}";

                    using (SqlCommand cmd = new SqlCommand(sqlStmt, conn))
                    {
                        int numOfRows = cmd.ExecuteNonQuery();
                        if (numOfRows > 0)
                        {
                            return updAppointment;
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

        //Delete Appointment
        public int Delete(int id)
        {
            try
            {
                 ErrorMessage = string.Empty;
                int numOfRows = 0;

                using (SqlConnection conn = DataBase.GetConnection())
                {
                    conn.Open();
                    string sqlStmt = $"DELETE FROM Appointment Where Id = {id}";

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

