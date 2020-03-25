using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using Schoolix.Db;
using Schoolix.Db.Types;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Schoolix.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace Schoolix.Areas.Identity.Pages.Account
{
    [AllowAnonymous]
    public class RegisterModel : PageModel
    {
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ILogger<RegisterModel> _logger;
        private readonly IServiceScopeFactory _serviceScopeFactory;
        private readonly IEmailSender _emailSender;

        public RegisterModel(
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            ILogger<RegisterModel> logger,
            IServiceScopeFactory serviceScopeFactory,
            IEmailSender emailSender)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _logger = logger;
            this._serviceScopeFactory = serviceScopeFactory;
            _emailSender = emailSender;
        }

        [BindProperty]
        public InputModel Input { get; set; }

        public string ReturnUrl { get; set; }

        public IList<AuthenticationScheme> ExternalLogins { get; set; }

        public class InputModel
        {
            [Required]
            [EmailAddress]
            [Display(Name = "E-Mail")]
            public string Email { get; set; }

            [Required]
            [StringLength(100, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 6)]
            [DataType(DataType.Password)]
            [Display(Name = "Passwort")]
            public string Password { get; set; }

            [DataType(DataType.Password)]
            [Display(Name = "Passwort bestätigen")]
            [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
            public string ConfirmPassword { get; set; }

            [Required]
            [Display(Name ="Typ")]
            public UserType UserType { get; set; }
            public SelectList Schools { get; set; }

            [Display(Name = "Schule")]
            public Guid? SchoolId { get; set; }

            [Display(Name = "Name der Schule")]
            public string SchoolName { get; set; }
        }

        public async Task OnGetAsync(string returnUrl = null)
        {
            ReturnUrl = returnUrl;
            Input = new InputModel();
            using (var ser = this._serviceScopeFactory.CreateScope())
            {
                var dbContext = ser.ServiceProvider.GetService<ApplicationDbContext>();
                Input.Schools = new SelectList(dbContext.Schools.Select(school => new{Id = school.Id, Value = school.Name}).ToList(), "Id", "Value");
            }
            ExternalLogins = (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList();
        }

        public async Task<IActionResult> OnPostAsync(string returnUrl = null)
        {
            returnUrl = returnUrl ?? Url.Content("~/");
            ExternalLogins = (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList();
            if (ModelState.IsValid)
            {
                var user = new ApplicationUser { UserName = Input.Email, Email = Input.Email, UserType = Input.UserType};
                if (Input.UserType == UserType.School && Input.SchoolId == null)
                {
                    EntityEntry<DbSchool> school = null;
                    using (var ser = this._serviceScopeFactory.CreateScope())
                    {
                        var dbContext = ser.ServiceProvider.GetService<ApplicationDbContext>();
                        school = dbContext.Schools.Add(new DbSchool()
                        {
                            Name = Input.SchoolName
                        });
                        await dbContext.SaveChangesAsync();
                    }
                    user.SchoolId = school.Entity.Id;
                }
                else
                {
                    user.SchoolId = Input.SchoolId.Value;
                }

                var result = await _userManager.CreateAsync(user, Input.Password);
                if (result.Succeeded)
                {
                    _logger.LogInformation("User created a new account with password.");

                    if (Input.UserType == UserType.School)
                    {
                        using (var ser = this._serviceScopeFactory.CreateScope())
                        {
                            var dbContext = ser.ServiceProvider.GetService<ApplicationDbContext>();
                            var dbSchool = dbContext.Schools.Find(user.SchoolId);
                            dbSchool.OwnerId = user.Id;
                            await dbContext.SaveChangesAsync();
                        }
                    }

                    var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                    code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code));
                    var callbackUrl = Url.Page(
                        "/Account/ConfirmEmail",
                        pageHandler: null,
                        values: new { area = "Identity", userId = user.Id, code = code },
                        protocol: Request.Scheme);

                    await _emailSender.SendEmailAsync(Input.Email, "Confirm your email",
                        $"Please confirm your account by <a href='{HtmlEncoder.Default.Encode(callbackUrl)}'>clicking here</a>.");

                    if (_userManager.Options.SignIn.RequireConfirmedAccount)
                    {
                        return RedirectToPage("RegisterConfirmation", new { email = Input.Email });
                    }
                    else
                    {
                        await _signInManager.SignInAsync(user, isPersistent: false);
                        return LocalRedirect(returnUrl);
                    }
                }
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }

            // If we got this far, something failed, redisplay form
            return Page();
        }
    }
}
