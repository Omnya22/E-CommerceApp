using E_CommerceApp.Core.Interfaces;
using E_CommerceApp.Core.Models;
using E_CommerceApp.Core.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace E_CommerceApp.Api.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;

        private readonly UserManager<AppUser> _userManager;

        private readonly SignInManager<AppUser> _signInManager;


        public AccountController(IUnitOfWork unitOfWork, UserManager<AppUser> userManager, SignInManager<AppUser> signInManager)
        {
            _unitOfWork = unitOfWork;
            _userManager = userManager;
            _signInManager = signInManager;
        }

        [HttpGet]
        [Authorize("Admin")]
        [Route("GetAllUsers")]
        public async Task<ActionResult> Get()
        {
            return Ok(await _unitOfWork.Users.GetAllAsync());
        }

        [HttpPost]
        [AllowAnonymous]
        [Route("Register")]
        public async Task<ActionResult> Register(RegisterViewModel model)
        {
            if (model == null)
                return NotFound();

            if (ModelState.IsValid)
            {
                if (EmailExist(model.Email))
                    return BadRequest("This Email is already exists.");

                if (UserNameExists(model.UserName))
                    return BadRequest("This User Name is already exists.");

                if (!EmailIsValid(model.Email))
                    return BadRequest("This Email isn't Valid.");

                var user = new AppUser
                {
                    UserName = model.UserName,
                    Email = model.Email,
                };

                var result = await _userManager.CreateAsync(user, model.Password);
                var resultRole = await _userManager.AddToRoleAsync(user, "User");
                if (result.Succeeded && resultRole.Succeeded)
                    return Ok(result);
            }

            return BadRequest();
        }

        [HttpPost]
        [AllowAnonymous]
        [Route("Login")]
        public async Task<ActionResult> Login(LoginViewModel model)
        {
            if (model == null)
                return NotFound();

            var user = await _userManager.FindByEmailAsync(model.Email);
            if (user == null)
                return NotFound();

            var result = await _signInManager.PasswordSignInAsync(user, model.Password, false, false);
            if (result.Succeeded)
                return Ok(result);

            return BadRequest(result.IsNotAllowed);
        }

        [HttpGet]
        [Authorize("Admin")]
        [Route("Logout")]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return Ok();
        }

        [HttpGet]
        [Authorize("Admin")]
        [Route("UserExists")]
        public async Task<IActionResult> UserExists(string username)
        {
            var exist = await _unitOfWork.Users.FindAsync(x => x.UserName == username);
            if (exist != null)
                return Ok();
            return NotFound();
        }

        [HttpGet]
        [Authorize("Admin")]
        [Route("EmailExists")]
        public async Task<IActionResult> EmailExists(string email)
        {
            var exist = await _unitOfWork.Users.FindAsync(x => x.Email == email);
            if (exist != null)
                return Ok();
            return NotFound();
        }

        private bool EmailExist(string email)
        {
            return _unitOfWork.Users.Find(e => e.Email == email) != null;
        }

        private bool EmailIsValid(string email)
        {
            Regex em = new Regex(@"\w+\@\w+.com|\w+\@\w+.net");
            if (em.IsMatch(email))
                return true;
            return false;
        }

        private bool UserNameExists(string name)
        {
            return _unitOfWork.Users.Find(e => e.UserName == name) != null;
        }
    }
}
