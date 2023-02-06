using AceJobAgency.Model;
using AceJobAgency.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace AceJobAgency.Pages
{
    public class ForgetPassword1Model : PageModel
    {
        private UserManager<ApplicationUser> userManager { get; }
        private SignInManager<ApplicationUser> signInManager { get; }

        [BindProperty]
        public ForgotPassword FModel { get; set; }

        [BindProperty]
        public Register RModel { get; set; }

        public ForgetPassword1Model(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager)
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
                var user = await userManager.FindByEmailAsync(RModel.Email);
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

