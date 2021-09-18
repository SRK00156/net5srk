using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Core_WebApp.Controllers
{
	public class RoleController : Controller
	{
		private readonly RoleManager<IdentityRole> roleManager;

		public RoleController(RoleManager<IdentityRole> role)
		{
			roleManager = role;
		}


		public IActionResult Index()
		{
			// read all roles
			var roles = roleManager.Roles;
			return View(roles);
		}

		public IActionResult Create()
		{
			return View(new IdentityRole());
		}
		[HttpPost]
		public async Task<IActionResult> Create(IdentityRole role)
		{
			await roleManager.CreateAsync(role);
			return RedirectToAction("Index");
		}
	}
}
