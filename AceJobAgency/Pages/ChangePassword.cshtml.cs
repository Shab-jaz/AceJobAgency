using AceJobAgency.Model;
using AceJobAgency.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PwnedPasswords.Client;

namespace AceJobAgency.Pages
{
    public class ChangePasswordModel : PageModel
    {
        private UserManager<ApplicationUser> UserManager { get; set; }

        //private readonly IPwnedPasswordsClient pwnedPasswords;, IPwnedPasswordsClient pwnedPasswords,this.pwnedPasswords = pwnedPasswords;

        [BindProperty]
        public ChangePassword CModel { get; set; }

        public ChangePasswordModel(UserManager<ApplicationUser> userManager)
        {
            UserManager = userManager;
        }

        public void OnGet()
        {
        }

        public async Task<IActionResult> OnPostChange_PasswordAsync()
        {
            if (ModelState.IsValid)
            {
                //if (await pwnedPasswords.HasPasswordBeenPwned(CModel.NewPassword))
                //{
                //    return RedirectToPage("/Index");
                //}

                var userId = UserManager.GetUserId(User);
                var user = await UserManager.FindByIdAsync(userId);

                if (user != null)
                {
                    IdentityResult result = await UserManager.ChangePasswordAsync(user, CModel.CurrentPassword, CModel.NewPassword);
                    if (result.Succeeded)
                    {
                        return RedirectToPage("/Settings");
                    }

                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError("", error.Description);
                    }
                }
            }

            return Page();
        }

        public IActionResult OnPostNo_Change_Password()
        {
            return RedirectToPage("/Settings");
        }
    }
}
