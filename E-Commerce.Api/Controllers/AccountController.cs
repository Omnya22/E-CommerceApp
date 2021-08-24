using E_Commerce.Api.DataAccess;
using E_Commerce.Api.Models;
using E_Commerce.Api.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace E_Commerce.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly AppDbContext _context;

        private readonly UserManager<ApplicationUser> _userManager;

        private RoleManager<ApplicationRole> _roleManager;

        private readonly SignInManager<ApplicationUser> _signInManager;

        private IMongoCollection<ApplicationUser> _collection;


        public AccountController(AppDbContext context,RoleManager<ApplicationRole> roleManager, UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager)
        {
            _context = context;
            _roleManager = roleManager;
            _userManager = userManager;
            _signInManager = signInManager;
            _collection = _context.GetCollection<ApplicationUser>("Users");
        }

        [HttpPost]
        [AllowAnonymous]
        [Route("Register")]
        public async Task<IActionResult> Register(RegisterViewModel model)
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

                ApplicationUser appUser = new ApplicationUser
                {
                    UserName = model.UserName,
                    Email = model.Email
                };

                IdentityResult result = await _userManager.CreateAsync(appUser, model.Password);
                if (result.Succeeded)
                     return Ok("User Created Successfully");
                else
                {
                    foreach (IdentityError error in result.Errors)
                        ModelState.AddModelError("", error.Description);
                }
            }
            return BadRequest();
        }

        [HttpPost]
        [AllowAnonymous]
        [Route("Login")]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            await CreateRoles();
            await CreateAdmin();
            if (model == null)
                return NotFound();

            var user = await _userManager.FindByEmailAsync(model.Email);
            if (user == null)
                return NotFound();

            var result = await _signInManager.PasswordSignInAsync(user, model.Password, false, false);
            if (result.Succeeded)
            {
                if (await _roleManager.RoleExistsAsync("User"))
                {
                    if (!await _userManager.IsInRoleAsync(user, "User") && !await _userManager.IsInRoleAsync(user, "Admin"))
                    {
                        await _userManager.AddToRoleAsync(user, "User");
                    }
                }
                return Ok(result);
            }

            return BadRequest(result.IsNotAllowed);
        }

        [HttpPost]
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
            var exist = await _collection.FindAsync(x => x.UserName == username);
            if (exist != null)
                return Ok();
            return NotFound();
        }

        [HttpGet]
        [Authorize("Admin")]
        [Route("EmailExists")]
        public async Task<IActionResult> EmailExists(string email)
        {
            var exist = await _collection.FindAsync(x => x.Email == email);
            if (exist != null)
                return Ok();
            return NotFound();
        }

        [HttpPost]
        [Authorize("Admin")]
        [Route("CreateRole")]
        public async Task<IActionResult> CreateRole(string name)
        {
            if (ModelState.IsValid)
            {
                IdentityResult result = await _roleManager.CreateAsync(new ApplicationRole() { Name = name });
                if (result.Succeeded)
                    return Ok("User Created Successfully");
                else
                {
                    foreach (IdentityError error in result.Errors)
                        ModelState.AddModelError("", error.Description);
                }
            }
            return BadRequest();
        }

        private bool EmailExist(string email)
        {
            return _collection.Find(e => e.Email==email).FirstOrDefault() != null;
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
            return _collection.Find(e => e.UserName == name).FirstOrDefault() != null;
        }
       
        private async Task CreateAdmin()
        {
            var admin = await _userManager.FindByNameAsync("Admin");
            if (admin == null)
            {
                var user = new ApplicationUser
                {
                    Email = "admin@admin.com",
                    UserName = "Admin"
                };

                var x = await _userManager.CreateAsync(user, "123456");
                if (x.Succeeded)
                {
                    if (await _roleManager.RoleExistsAsync("Admin"))
                        await _userManager.AddToRoleAsync(user, "Admin");
                }
            }
        }

        private async Task CreateRoles()
        {
            if (await _roleManager.RoleExistsAsync("Admin"))
            {
                var role = new ApplicationRole
                {
                    Name = "Admin"
                };
                await _roleManager.CreateAsync(role);
            }
            if (await _roleManager.RoleExistsAsync("User"))
            { 
                var role = new ApplicationRole
                {
                    Name = "User"
                };
                await _roleManager.CreateAsync(role);
            }
        }

    }
}