using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace AceJobAgency.Pages.Shared
{
    public class SettingsModel : PageModel
    {
        public void OnGet()
        {
        }

        public IActionResult OnPostChangePassword_Page()
        {
            return RedirectToPage("/ChangePassword");
        }

        public IActionResult OnPostResetPassword_Page()
        {
            return RedirectToPage("/ChangePassword");
        }
    }
}
