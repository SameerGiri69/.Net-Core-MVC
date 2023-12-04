using Domain.Models;
using Infrastructure.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ValueGeneration.Internal;
using RunningGroups.Data;
using Presentation_MVC.ViewModels;
using RunningGroups.ViewModels;

namespace Presentation_MVC.Controllers
{
    public class AccountController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        public AccountController(ApplicationDbContext context, UserManager<AppUser> userManager, SignInManager<AppUser> signInManager)
        {
            _context = context;
            _userManager = userManager;
            _signInManager = signInManager;
        }

        // GET: LoginViewModels
        public async Task<IActionResult> Index()
        {
            var users = await _context.Users.Select(x => new UserViewModel()
            {
                Id = x.Id,
                EmailAddress = x.NormalizedEmail,
                UserName = x.UserName
            }).ToListAsync();



            return View(users);

        }
        [HttpGet]
        public IActionResult Login()
        {
            var response = new LoginViewModel();
            return View(response);
        }
        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel loginviewModel)
        {
            {
                if (!ModelState.IsValid) return View(loginviewModel);

                var user = await _userManager.FindByEmailAsync(loginviewModel.Email);

                if (user != null)
                {
                    //User is found, check password

                    //Password correct, sign in
                    var result = await _signInManager.PasswordSignInAsync(user, loginviewModel.Password, false, false);
                    if (result.Succeeded)
                    {
                        return RedirectToAction("Index", "Race");
                    }
                    else

                        //Password is incorrect
                        TempData["Error"] = "Wrong credentials. Please try again";
                    return View(loginviewModel);
                }
                //User not found
                TempData["Error"] = "Wrong credentials. Please try again";
                return View(loginviewModel);
            }

        }
        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel registerViewModel)
        {
            if (!ModelState.IsValid) return View(registerViewModel);

            var user = await _userManager.FindByEmailAsync(registerViewModel.EmailAddress);
            if (user != null)
            {
                TempData["Error"] = "This email is already registered";
                return View(registerViewModel);
            }
            var newUser = new AppUser()
            {
                Email = registerViewModel.EmailAddress,
                UserName = registerViewModel.EmailAddress,
                NormalizedEmail = registerViewModel.EmailAddress.ToUpper(),
                NormalizedUserName = registerViewModel.EmailAddress.ToUpper()
            };
            var newUserResponse = await _userManager.CreateAsync(newUser, registerViewModel.Password);
            if (newUserResponse.Succeeded)
            {
                await _userManager.AddToRoleAsync(newUser, UserRoles.User);
            }
            return RedirectToAction("Index", "Race");
        }

        [HttpGet]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }


        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Email,Password")] LoginViewModel loginViewModel)
        {
            if (ModelState.IsValid)
            {
                _context.Add(loginViewModel);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(loginViewModel);
        }
    }
}
