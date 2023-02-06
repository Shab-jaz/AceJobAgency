using AceJobAgency.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace AceJobAgency.Pages
{
    [Authorize]
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        private AuthDbContext authDbContext;
        private UserManager<ApplicationUser> UserManager { get; }
        private readonly SignInManager<ApplicationUser> signInManager;

        [BindProperty]
        public ApplicationUser user { get; set; }

        public IndexModel(ILogger<IndexModel> logger, UserManager<ApplicationUser> userManager, AuthDbContext authDbContext, SignInManager<ApplicationUser> signInManager)
        {
            _logger = logger;
            UserManager = userManager;
            this.authDbContext = authDbContext;
            this.signInManager = signInManager; 
        }

        public IActionResult OnGet()
        {

            var dataProtectionProvider = DataProtectionProvider.Create("EncryptData");
            var protector = dataProtectionProvider.CreateProtector("MySecretKey");

            if (string.IsNullOrEmpty(HttpContext.Session.GetString("AuthSession")))
            {
                HttpContext.Session.Remove("AuthSession");
                signInManager.SignOutAsync();
                HttpContext.Response.Redirect("Login");
            }

            string userId = UserManager.GetUserId(User);
            ApplicationUser? currentUser = authDbContext.Users.FirstOrDefault(x => x.Id.Equals(userId));
            user = currentUser;
            user.NRIC = protector.Unprotect(currentUser.NRIC);
            return Page();
        }
    }
}