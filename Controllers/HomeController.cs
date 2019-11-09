using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Cars_and_Owners.Models;
using Newtonsoft.Json;
using System.Collections;
using System.Diagnostics;
using System.Linq;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace Cars_and_Owners.Controllers
{
	public class HomeController : Controller
	{
		private MyContext dbContext;
		public HomeController(MyContext context)
		{
			dbContext = context;
		}

		[HttpGet]
		public IActionResult Dashboard()
		{
			if (HttpContext.Session.GetInt32("user_id") != null)
			{
				int userId = (int)HttpContext.Session.GetInt32("user_id");
				User user = dbContext.Users.FirstOrDefault(u => u.Id == userId);
				List<Car> cars = dbContext.Cars.Include(c => c.Likes).ToList();
				DashboardViewModel model = new DashboardViewModel()
				{
					User = user,
					Cars = cars
				};
				return View(model);
			}
			return RedirectToAction("Login", "Login");
		}

		[HttpGet]
		public IActionResult NewCar() => View();

		[HttpGet("/car/{id}")]
		public IActionResult Car(int id)
		{
			Car car = dbContext.Cars.Include(c => c.Likes).ThenInclude(like => like.User).FirstOrDefault(c => c.Id == id);
			CarViewModel model = new CarViewModel()
			{
				Car = car,
				Like = new Like()
			};

			model.Like.UserId = (int)HttpContext.Session.GetInt32("user_id");
			model.Like.CarId = car.Id;
			return View(model);
		}

		[HttpPost]
		public IActionResult DeleteCar(int id)
		{
			Car Car = dbContext.Cars.FirstOrDefault(c => c.Id == id);
			dbContext.Remove(Car);
			dbContext.SaveChanges();
			return RedirectToAction("Dashboard");
		}

		[HttpPost]
		public IActionResult AddLike(CarViewModel model)
		{
			dbContext.Likes.Add(model.Like);
			dbContext.SaveChanges();
			return RedirectToAction("Car", new { id = model.Like.CarId });
		}


		[HttpPost]
		public IActionResult NewCar(Car newCar)
		{
			System.Console.WriteLine(JsonConvert.SerializeObject(newCar));
			System.Console.WriteLine(ModelState.IsValid);
			if (ModelState.IsValid)
			{
				dbContext.Cars.Add(newCar);
				dbContext.SaveChanges();
				return RedirectToAction("Dashboard");
			}
			else
			{
				return View();
			}
		}

		[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
		public IActionResult Error()
		{
			return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
		}
	}
}
