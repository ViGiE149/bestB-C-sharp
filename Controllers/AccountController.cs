using Microsoft.AspNetCore.Mvc;
using bestbrigh.Models;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using System.Text;
using bestbrigh.data;
//using System.Security.Claims;
using System.Security.Cryptography;

namespace bestbrigh.Controllers
{
    public class AccountController : Controller
    {

        private ApplicationDbContext _dbContext;

        public AccountController(ApplicationDbContext dbContext)
        {
            this._dbContext = dbContext;
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }
        public IActionResult Index()
        {
            return View();
        }
       

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel model)
        {




            if (ModelState.IsValid)
            {
                // Hash the entered password
                string hashedPassword;
                using (SHA256 sha256 = SHA256.Create())
                {
                    byte[] hashedBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(model.Password));

                    StringBuilder builder = new StringBuilder();
                    for (int i = 0; i < hashedBytes.Length; i++)
                    {
                        builder.Append(hashedBytes[i].ToString("x2")); // Convert byte to hexadecimal string
                    }

                    hashedPassword = builder.ToString();
                }

                // Retrieve user from database based on username
                var user = _dbContext.Users.FirstOrDefault(u => u.Username == model.Username);

                if (user != null && user.PasswordHash == hashedPassword)
                {
                    // Create claims for authentication
                    var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, user.Username),
                new Claim(ClaimTypes.Role, user.Role)
            };

                    // Conditionally include BranchID claim based on user's role
                  //  if (user.Role != "Super Admin")
                   // {
                   //     claims.Add(new Claim("BranchID", user.BranchID.ToString()));
                    //}

                  var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                   var authProperties = new AuthenticationProperties
                    {
                        // Other authentication properties if needed
                    };

                    // Sign in the user
                    await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity), authProperties);

                    // Redirect based on user's role
                    switch (user.Role)
                    {
                        case "Admin":
                            return RedirectToAction("Index", "Home");
                        case "Salesperson":
                            return RedirectToAction("ManagerDashboard", "BranchManagerDashboard");
                        case "Regular User":
                            return RedirectToAction("Index", "Home");
                        default:
                            ModelState.AddModelError(string.Empty, "Unknown role");
                            break;
                    }
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Invalid username or password");
                }
            }

            return View(model);
        }


        public IActionResult Logout()
        {
            // Logout logic
            return RedirectToAction("Login");
        }
    }
}
