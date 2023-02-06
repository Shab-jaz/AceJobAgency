using AceJobAgency.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using AceJobAgency.Model;
using Microsoft.AspNetCore.DataProtection;
using PwnedPasswords.Client;

namespace AceJobAgency.Pages
{
    public class RegisterModel : PageModel
    {
        private UserManager<ApplicationUser> userManager { get; }
        private SignInManager<ApplicationUser> signInManager { get; }

        [BindProperty]
        public Register RModel { get; set; }

        public string Filepath { get; set; }

        private IWebHostEnvironment _environment;


        public RegisterModel(UserManager<ApplicationUser> userManager,SignInManager<ApplicationUser> signInManager, IWebHostEnvironment environment)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
            this._environment = environment;

        }
        public void OnGet()
        {
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (ModelState.IsValid)
            {
                var dataProtectionProvider = DataProtectionProvider.Create("EncryptData");
                var protector = dataProtectionProvider.CreateProtector("MySecretKey");


                    if (RModel.Resume != null)
                    {
                        if (RModel.Resume.Length > 2 * 1024 * 1024)
                        {
                            ModelState.AddModelError("Upload",
                           "File size cannot exceed 2MB.");
                            return Page();
                        }

                        var uploadsFolder = "uploads";
                        var imageFile = Guid.NewGuid() + Path.GetExtension(RModel.Resume.FileName);
                        var imagePath = Path.Combine(_environment.ContentRootPath, "wwwroot", uploadsFolder, imageFile);
                        using var fileStream = new FileStream(imagePath, FileMode.Create);
                        await RModel.Resume.CopyToAsync(fileStream);
                        Filepath = string.Format("/{0}/{1}", uploadsFolder, imageFile);
                    }


                    var user = new ApplicationUser()
                    {
                        UserName = RModel.Email,
                        Email = RModel.Email,
                        FirstName = RModel.FirstName,
                        LastName = RModel.LastName,
                        Gender = RModel.Gender,
                        NRIC = protector.Protect(RModel.NRIC),
                        DateOfBirth = RModel.DateOfBirth,
                        WhoamI = RModel.WhoamI,
                        ResumePath = Filepath,
                    };
                    var result = await userManager.CreateAsync(user, RModel.Password);
                    if (result.Succeeded)
                    {

                        return RedirectToPage("Login");
                    }
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError("", error.Description);
                    }
            }
            return Page();
        }

    }
}
