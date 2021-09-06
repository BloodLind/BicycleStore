using BicycleStore.Identity.Models;
using BicycleStore.Identity.Repositories;
using BicycleStore.Web.Models.ViewModels.Api;
using BicycleStore.Web.Services;
using BicycleStore.Web.Services.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace BicycleStore.Web.Controllers.API
{
    [Route("/api/account/"), ApiController]
    public class AccountController : Controller
    {
        private readonly UserRepository userRepository;
        private readonly IJwtGenerator jwtGenerator;

        public AccountController(UserRepository userRepository, IJwtGenerator jwtGenerator)
        {
            this.userRepository = userRepository;
            this.jwtGenerator = jwtGenerator;
        }

        private async Task<ClaimsIdentity> GetIdentity(string login, string password)
        {
            var user = await userRepository.GetUserAsync(login);
            if (user == null)
                return null;

            var result = await userRepository.SignInManager.CheckPasswordSignInAsync(user, password, false);
            if (result.Succeeded)
            {
                return  (await userRepository.SignInManager.ClaimsFactory.CreateAsync(user)).Identities.First();
            }
            return null;
        }
        
        [HttpPost]
        public async Task<ActionResult> GetToken([FromBody] ClientRequest request)
        {
            if (request == null || request.Login == null || request == null)
            {
                return BadRequest();
            }
            else
            {
                var identity = await GetIdentity(request.Login, request.Password);
                if (identity == null)
                {
                   return BadRequest();
                }
                else
                {
                    var user = await userRepository.GetUserAsync(request.Login);
                    var response = new AccountResponse
                    {
                        Email = user.Email,
                        Firstname = user.Firstname,
                        Secondname = user.Secondname,
                        Role = user.UserRoles.FirstOrDefault(x => x.UserId == user.Id)?.Role.Name,
                        Token = jwtGenerator.CreateToken(identity, user)
                    };
                    return Json(response);
                }
            }
        }
    }
}
