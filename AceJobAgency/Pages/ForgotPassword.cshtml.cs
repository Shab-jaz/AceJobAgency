using AceJobAgency.Model;
using AceJobAgency.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;

namespace AceJobAgency.Pages
{
    public class ForgotPasswordModel : PageModel
    {
        private UserManager<ApplicationUser> userManager { get; }
        private SignInManager<ApplicationUser> signInManager { get; }

        [BindProperty]
        public ForgotPassword FModel { get; set; }   

        public ForgotPasswordModel(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
        }

        public void OnGet()
        {
        }

        public async Task<IActionResult> OnPostForgotPasswordasync()
        {
            if (!ModelState.IsValid)
            {
                var user = await userManager.FindByEmailAsync(FModel.Email);
                if (user == null)
                    return RedirectToPage("ForgotPasswordConfirmation");

                var token = await userManager.GeneratePasswordResetTokenAsync(user);
                var link = Url.Action("ResetPassword", "Account", new { token, email = user.Email }, Request.Scheme);

                EmailHelper emailHelper = new EmailHelper();
                bool emailResponse = emailHelper.SendEmailPasswordReset(user.Email, link);

                if (emailResponse)
                    return RedirectToPage("ForgotPasswordConfirmation");
                else
                {
                    return RedirectToPage("Register");
                }
            }
            return Page();
        }

    }
}
