using ProjectCrudWebApp.Helpers;
using ProjectCrudWebApp.Models;
using System.Data.SqlClient;

namespace ProjectCrudWebApp.DataAccess
{
    public class DashboardDataAccess
    {
        public string ErrorMessage { get; private set; }
        public DashboardDataModel GetAll()
        {
            try
            {

                ErrorMessage = String.Empty;
                ErrorMessage = "";
                var d = new DashboardDataModel();
                using (SqlConnection conn = DataBase.GetConnection())
                {
                    conn.Open();
                    var sqlStmt = "select count(*) as DoctorCount from Doctor";
                    using (SqlCommand cmd = new SqlCommand(sqlStmt, conn))
                    {
                        d.DoctorCount = Convert.ToInt32(cmd.ExecuteScalar());
                    }

                    sqlStmt = "select count(*) as PatientCount from Patient";
                    using (SqlCommand cmd = new SqlCommand(sqlStmt, conn))
                    {
                        d.PatientCount = Convert.ToInt32(cmd.ExecuteScalar());
                    }
                    sqlStmt = "select count(*) as AppointmentCount from Appointment";
                    using (SqlCommand cmd = new SqlCommand(sqlStmt, conn))
                    {
                        d.AppointmentCount = Convert.ToInt32(cmd.ExecuteScalar());
                    }
                     

                }

                return d;


            }
            catch (Exception ex)
            {
                ErrorMessage = ex.Message;
                return null;
            }

        }

    }
}


