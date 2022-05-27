using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;
using System.Security.Claims;

namespace TrainingCRUDWebApp.Pages
{
    public class LoginModel : PageModel
    {
        [BindProperty]
        [Display(Name = "User Name")]
        public string UserName { get; set; }

        [BindProperty]
        [Display(Name = "Password")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        public string ErrorMessage { get; set; }
        public void OnGet()
        {
        }

        public async void OnPost()
        {
            if (!ModelState.IsValid)
            {
                ErrorMessage = "Invalid Login or Password";
                return;
            }

            if (UserName == "DigitalHealthUser" && Password == "DHuser")
            {
                var userClaims = new List<Claim>()
                {
                    new Claim("UserId", "1"),
                    new Claim(ClaimTypes.Name, "DigitalHealthUser"),
                    new Claim(ClaimTypes.Role, "User" )
                 };

                var userIdentity = new ClaimsIdentity(userClaims, "User Identity");

                var userPrincipal = new ClaimsPrincipal(new[] { userIdentity });
                await HttpContext.SignInAsync(userPrincipal);

                Response.Redirect("/Index");
                return;
            }
            else if (UserName == "Admin" && Password == "DHadmin")
            {
                var userClaims = new List<Claim>()
                {
                    new Claim("UserId", "2"),
                    new Claim(ClaimTypes.Name, "Administrator"),
                    new Claim(ClaimTypes.Role, "Admin" )
                 };

                var userIdentity = new ClaimsIdentity(userClaims, "User Identity");

                var userPrincipal = new ClaimsPrincipal(new[] { userIdentity });
                await HttpContext.SignInAsync(userPrincipal);

                Response.Redirect("/Index");
                return;
            }

            //Invalid User
            ErrorMessage = "Invalid Login or Password";
            return;
        }
    }
}
