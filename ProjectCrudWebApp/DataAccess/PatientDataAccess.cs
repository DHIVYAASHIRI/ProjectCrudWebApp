using ProjectCrudWebApp.Helpers;
using ProjectCrudWebApp.Models;
using System.Data.SqlClient;

namespace ProjectCrudWebApp.DataAccess
{
    public class PatientDataAccess
    {
        public string ErrorMessage { get; private set; }
        public PatientDataAccess()
        {
            ErrorMessage = "";
        }

        //Get all Patients
        public List<PatientDataModel> GetAll()
        {
            try
            {
                ErrorMessage = string.Empty;
                ErrorMessage = "";

                List<PatientDataModel> patients = new List<PatientDataModel>();

                using (SqlConnection conn = DataBase.GetConnection())
                {
                    conn.Open();
                    var sqlStmt = "Select Id,Name,DOB,Gender,MobileNumber,Address,email,Password from dbo.Patient";
                    using (SqlCommand cmd = new SqlCommand(sqlStmt, conn))
                    {
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read() == true)
                            {
                                PatientDataModel patient = new PatientDataModel();
                                patient.Id = reader.GetInt32(0);
                                patient.Name = reader.GetString(1);
                                patient.DOB = reader.GetDateTime(2);
                                patient.Gender = reader.GetString(3);
                                patient.MobileNumber = reader.GetString(4);
                                patient.Address = reader.GetString(5);
                                patient.email = reader.GetString(6);
                                patient.Password = reader.GetString(7);

                                patients.Add(patient);
                            }
                        }
                    }
                }

                return patients;
            }
            catch (Exception ex)
            {
                ErrorMessage = ex.Message;
                return null;
            }
        }

        //Insert new patient
        public PatientDataModel Insert(PatientDataModel newPatient)
        {
            try
            {
                ErrorMessage = string.Empty;
                ErrorMessage = String.Empty;
                using (SqlConnection conn = DataBase.GetConnection())
                {
                    conn.Open();
                    string sqlStmt = $"INSERT INTO dbo.Patient (Name,DOB,Gender, MobileNumber,Address,email,Password) VALUES ('{newPatient.Name}', '{newPatient.DOB.ToString("yyyy-MM-dd")}', '{newPatient.Gender}', '{newPatient.MobileNumber}', '{newPatient.Address}', '{newPatient.email}', '{newPatient.Password}'); SELECT SCOPE_IDENTITY();";

                    using (SqlCommand cmd = new SqlCommand(sqlStmt, conn))
                    {
                        int idInserted = Convert.ToInt32(cmd.ExecuteScalar());
                        if (idInserted > 0)
                        {
                            newPatient.Id = idInserted;
                            return newPatient;
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

        //Get Patients By Id
        public PatientDataModel GetPatientById(int id)
        {
            try
            {
                PatientDataModel patient = null;
                using (SqlConnection conn = DataBase.GetConnection())
                {
                    conn.Open();
                    string sqlStmt = $"Select Id,Name,DOB,Gender, MobileNumber,Address,email,Password from Patient where Id = {id}";
                    using (SqlCommand cmd = new SqlCommand(sqlStmt, conn))
                    {
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.Read() == true)
                            {
                                patient = new PatientDataModel();
                                patient.Id = reader.GetInt32(0);
                                patient.Name = reader.GetString(1);
                                patient.DOB = reader.GetDateTime(2);
                                patient.Gender=reader.GetString(3);
                                patient.MobileNumber=reader.GetString(4);
                                patient.Address=reader.GetString(5);
                                patient.email=reader.GetString(6);
                                patient.Password=reader.GetString(7);
                            }
                        }
                    }
                }

                return patient;
            }
            catch (Exception ex)
            {
                ErrorMessage = ex.Message;
                return null;
            }
        }

        //Update Department
        public PatientDataModel Update(PatientDataModel updPatient)
        {
            try
            {
                ErrorMessage = "";
                using (SqlConnection conn = DataBase.GetConnection())
                {
                    conn.Open();
                    string sqlStmt = $"UPDATE dbo.Patient SET Name = '{updPatient.Name}', " +
                        $"DOB = '{updPatient.DOB}', " +
                        $"Gender = '{updPatient.Gender}', " +
                        $"MobileNumber = '{updPatient.MobileNumber}', " +
                        $"Address = '{updPatient.Address}' ," +
                        $"email = '{updPatient.email}', " +
                        $"Password = '{updPatient.Password}' " +
                        $"where Id = {updPatient.Id}";

                    using (SqlCommand cmd = new SqlCommand(sqlStmt, conn))
                    {
                        int numOfRows = cmd.ExecuteNonQuery();
                        if (numOfRows > 0)
                        {
                            return updPatient;
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

    }
}
