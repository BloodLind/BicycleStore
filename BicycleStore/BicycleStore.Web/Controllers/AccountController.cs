using BicycleStore.Identity.Models;
using BicycleStore.Identity.Repositories;
using BicycleStore.Web.Models.ViewModels.Account;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BicycleStore.Web.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserRepository userRepository;

        public AccountController(UserRepository userRepository)
        {
            this.userRepository = userRepository;
        }

        public SignInManager<User> SignInManager { get; }

        #region Account Managment
        public IActionResult ChangePassword()
        {
            return View(new ChangePasswordViewModel());
        }

        [HttpPost]
        public async Task<IActionResult> ChangePassword(ChangePasswordViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                if ((await userRepository.ChangePasswordAsync(await userRepository.GetUserAsync(User), viewModel.Password, viewModel.NewPassword)))
                {
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    ModelState.AddModelError("Password", "Incorrect password");
                }
            }
            return View(viewModel);
        }
        #endregion

        #region Authorization
        public IActionResult Login(string returnURL = null)
        {
            return View(new LoginViewModel() { ReturnURL = returnURL });
        }

        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel loginViewModel)
        {
            if (ModelState.IsValid)
            {
                var result = await userRepository.SignInManager.PasswordSignInAsync(loginViewModel.Email, loginViewModel.Password, loginViewModel.rememberMe, false);
                
                if (result.Succeeded)
                {
                    if (!string.IsNullOrEmpty(loginViewModel.ReturnURL) && Url.IsLocalUrl(loginViewModel.ReturnURL))
                        return Redirect(loginViewModel.ReturnURL);
                    else
                        return RedirectToAction("Index", "Home");
                    
                }
                else
                    ModelState.AddModelError("", "Wrong password or login!");
            }
            return View(loginViewModel);
        }

        public async Task<IActionResult> LogOut()
        {
            await userRepository.SignInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }
        #endregion

        #region Registration
        public IActionResult Register(bool authorize = true)
        {
            return View(new RegistrationViewModel() { User = new User(), Authorize = authorize });
        }


        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegistrationViewModel registerViewModel)
        {

            if (registerViewModel.Password != registerViewModel.ConfirmPassword)
            {
                ModelState.AddModelError("", "Different Passwords!");
                return View(registerViewModel);
            }

            if (ModelState.IsValid)
            {
                registerViewModel.User.UserName = registerViewModel.User.Email;

                var result = await userRepository.AddUserAsync(registerViewModel.User, registerViewModel.Password);


                if (result.Succeeded)
                {
                    await userRepository.AddToRoleAsync(await userRepository.GetUserAsync(registerViewModel.User.Email), "User");
                    return RedirectToAction("Login", "Account");
                }
                else
                {
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError("", error.Description);
                    }
                }


            }
            return View(registerViewModel);
        }
        #endregion
    }
}
