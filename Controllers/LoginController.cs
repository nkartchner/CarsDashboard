using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Cars_and_Owners.Models;
using System.Diagnostics;
using Newtonsoft.Json;
using System.Linq;
using System.Collections.Generic;

namespace Cars_and_Owners.Controllers
{
	public class LoginController : Controller
	{

		private MyContext dbContext;
		public LoginController(MyContext context)
		{
			dbContext = context;
		}

		[HttpGet]
		public IActionResult Login()
		{
			if (HttpContext.Session.GetInt32("user_id") != null)
			{
				return RedirectToAction("Dashboard", "Home");
			}
			return View();
		}

		[HttpPost]
		public IActionResult Login(LoginViewModel model)
		{
			System.Console.WriteLine($"&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&& {ModelState.IsValid} &&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&");
			System.Console.WriteLine($"*************************************************************** {JsonConvert.SerializeObject(model)} ***************************************************************");

			if (ModelState.IsValid)
			{
				if (model.User != null)
				{
					List<User> users = dbContext.Users.Where(u => u.Email == model.User.Email).ToList();
					if (users.Count > 0)
					{
						ModelState.AddModelError("User.Email", "A user with that email already Exists");
						return View();
					}
					else
					{
						PasswordHasher<User> Hasher = new PasswordHasher<User>();
						model.User.Password = Hasher.HashPassword(model.User, model.User.Password);
						dbContext.Users.Add(model.User);
						dbContext.SaveChanges();
						return RedirectToAction("Dashboard", "Home");
					}
				}
				else if (model.Credentials != null)
				{

					User user = dbContext.Users.FirstOrDefault(u => u.Email == model.Credentials.Email);

					if (user != null)
					{
						PasswordHasher<Credentials> Hasher = new PasswordHasher<Credentials>();
						PasswordVerificationResult result = Hasher.VerifyHashedPassword(model.Credentials, user.Password, model.Credentials.Password);
						if (result == 0)
						{
							System.Console.WriteLine("Invalid PW");
							ModelState.AddModelError("Credentials.Password", "Invalid Password");
							return View();
						}
						else
						{
							HttpContext.Session.SetInt32("user_id", user.Id);
							return RedirectToAction("Dashboard", "Home");
						}
					}
					else
					{
						ModelState.AddModelError("Credentials.Email", "A user with that email does not exist");
						System.Console.WriteLine("Email does not exist!");
						return View();
					}

				}


			}
			return View();
		}

	}
}