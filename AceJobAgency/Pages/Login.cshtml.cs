using AceJobAgency.ViewModels;
using AceJobAgency.Model;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace AceJobAgency.Pages
{
    public class LoginModel : PageModel
    {
        private readonly GoogleCaptchaService _GoogleCaptchaService;
        [BindProperty]
        public Login LModel { get; set; }

        private readonly SignInManager<ApplicationUser> signInManager;
        private readonly IHttpContextAccessor contxt;
        public LoginModel(SignInManager<ApplicationUser> signInManager, GoogleCaptchaService googleCaptchaService, IHttpContextAccessor httpContextAccessor)
        {
            this.signInManager = signInManager;
            _GoogleCaptchaService = googleCaptchaService;
            contxt = httpContextAccessor;
        }
        public void OnGet()
        {
        }

        public async Task<IActionResult> OnPostAsync()
        {
            var _GoogleCaptcha = _GoogleCaptchaService.ResVer(LModel.Token);
            if (!_GoogleCaptcha.Result.success && _GoogleCaptcha.Result.score >= 0.5)
            {
                ModelState.AddModelError("", "You are not human");

            }
            if (ModelState.IsValid)
            {
                //await signInManager.SignInAsync(user, false);
                var identityResult = await signInManager.PasswordSignInAsync(LModel.Email, LModel.Password,
                LModel.RememberMe, lockoutOnFailure:true);
                if (identityResult.IsLockedOut)
                {
                    ModelState.AddModelError("LockoutError", "The account is locked out");
                    return Page();
                }
                if (identityResult.Succeeded)
                {
                    string guid = Guid.NewGuid().ToString();
                    HttpContext.Session.SetString("AuthSession", guid);
                    Response.Cookies.Append("AuthSession", guid);
                    contxt.HttpContext.Session.SetString("UserSession", LModel.Email);
                    return RedirectToPage("Index");
                }
                ModelState.AddModelError("", "Username or Password incorrect");

            }
            return Page();
        }
    }
}
