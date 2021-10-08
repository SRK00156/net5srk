using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Core_WebApp_KalpeshSoliya.Controllers
{
    public class RoleController:Controller
    {
        private readonly RoleManager<IdentityRole> roleManager;

        public RoleController(RoleManager<IdentityRole> role)
        {
            roleManager = role;
        }
		[Authorize(Roles = "Admin,Administrator")]
		public IActionResult Index()
		{
			// read all roles
			var roles = roleManager.Roles;
			return View(roles);
		}
		[Authorize(Roles = "Admin,Administrator")]
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
