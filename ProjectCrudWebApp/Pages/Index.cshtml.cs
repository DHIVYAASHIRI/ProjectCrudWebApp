using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ProjectCrudWebApp.DataAccess;

namespace ProjectCrudWebApp.Pages
{
 
        public class IndexModel : PageModel
        {
        public int DoctorCount { get; set; }
        public int PatientCount { get; set; }
        public int AppointmentCount { get; set; }

        [FromQuery(Name = "action")]
            public string Action { get; set; }


            public string WelcomeMessage { get; set; }
        public string ErrorMessage { get; private set; }

        private readonly ILogger<IndexModel> _logger;

            public IndexModel(ILogger<IndexModel> logger)
            {
                _logger = logger;
            DoctorCount = 0;
            PatientCount = 0;
            AppointmentCount = 0;
            ErrorMessage = "";
            }

            public void OnGet()
            {
                if (!String.IsNullOrEmpty(Action) && Action.ToLower() == "logout")
                {
                    Logout();
                    return;
                }

          
            var dashBoardData = new DashboardDataAccess();
            var dashboard = dashBoardData.GetAll();
            if (dashboard != null)
            {
                DoctorCount = dashboard.DoctorCount;
                PatientCount = dashboard.PatientCount;
                AppointmentCount = dashboard.AppointmentCount;
            }
            else
            {
                ErrorMessage = $"No Dashboard Data Available - {dashBoardData.ErrorMessage}";
            }
        }

            public void OnPost()
            {
                Logout();
            }

            private void Logout()
            {
                HttpContext.SignOutAsync();
                Response.Redirect("/Index");
            }
        }
    }
