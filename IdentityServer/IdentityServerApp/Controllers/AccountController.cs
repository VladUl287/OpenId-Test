using IdentityServer4;
using IdentityServer4.Services;
using IdentityServerApp.Attributes;
using IdentityServerApp.Dtos;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using IdentityServerApp.Database.Entities;

namespace IdentityServerApp.Controllers
{
    [SecurityHeaders]
    [AllowAnonymous]
    public class AccountController : Controller
    {
        private readonly IIdentityServerInteractionService interaction;
        private readonly UserManager<User> userManage;

        public AccountController(IIdentityServerInteractionService interaction, UserManager<User> userManage)
        {
            this.interaction = interaction;
            this.userManage = userManage;
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterDtoModel model)
        {
            if (ModelState.IsValid)
            {
                var user = new User
                {
                    Email = model.Email,
                };

                var result = await userManage.CreateAsync(user, model.Password);
                if (!result.Succeeded)
                {
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError(string.Empty, error.Description);
                    }
                }
            }
            return View(model);
        }

        [HttpGet]
        public IActionResult Login(string returnUrl)
        {
            return View(new LoginDtoModel
            {
                ReturnUrl = returnUrl
            });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login([FromForm] LoginDtoModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await userManage.FindByEmailAsync(model.Email);

                if (user is not null)
                {
                    AuthenticationProperties? props = null;
                    if (model.RememberLogin)
                    {
                        props = new AuthenticationProperties
                        {
                            IsPersistent = true,
                            ExpiresUtc = DateTimeOffset.UtcNow.AddDays(30)
                        };
                    };

                    var isuser = new IdentityServerUser(user.Id)
                    {
                        DisplayName = user.UserName
                    };

                    await HttpContext.SignInAsync(isuser, props);

                    return Redirect(model.ReturnUrl);
                }

                ModelState.AddModelError(string.Empty, "Invalid username or password");
            }

            return View(model);
        }

        [HttpGet]
        public IActionResult Logout(string logoutId)
        {
            return View(new LogoutDtoModel()
            {
                LogoutId = logoutId
            });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Logout(LogoutDtoModel model)
        {
            var logout = await interaction.GetLogoutContextAsync(model.LogoutId);

            if (User.Identity?.IsAuthenticated is true)
            {
                await HttpContext.SignOutAsync();
}

            return Redirect(logout.PostLogoutRedirectUri ?? "http://localhost:8080");
        }
    }
}
