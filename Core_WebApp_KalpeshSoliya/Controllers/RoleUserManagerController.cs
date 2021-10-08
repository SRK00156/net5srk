using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Core_WebApp_KalpeshSoliya.Controllers
{
	public class RoleUserManagerController : Controller
	{
		private readonly UserManager<IdentityUser> userManager;
		private readonly RoleManager<IdentityRole> roleManager;
		public RoleUserManagerController(UserManager<IdentityUser> user, RoleManager<IdentityRole> role)
		{
			userManager = user;
			roleManager = role;
		}

		/// <summary>
		/// REad all Users and Roles and display then in <select></select> control
		/// </summary>
		/// <returns></returns>
		[Authorize(Roles = "Admin,Lead,Administrator")]
		public async Task<IActionResult> Index()
		{
			var ur = new UserInRole();
			ViewBag.Users = await userManager.Users.ToListAsync();
			ViewBag.Roles = await roleManager.Roles.ToListAsync();
			return View(ur);
		}

		[HttpPost]
		public async Task<IActionResult> Create(UserInRole ur)
		{
			// search the user based on userid received from view and if the user exist and if found read the UserId
			var user = await userManager.FindByIdAsync(ur.UserName);
			// search RoleNamme based on RoleId received from View
			var role = await roleManager.FindByIdAsync(ur.Name);
			// Noew assing Role to the user
			await userManager.AddToRoleAsync(user, role.Name);
			ViewBag.Message = $"User Name {user.UserName} is Assigned to Role {role.Name}";
			ViewBag.Users = await userManager.Users.ToListAsync();
			ViewBag.Roles = await roleManager.Roles.ToListAsync();
			return View("Index");
		}
	}

	/// <summary>
	/// The class used to accept UserNAme and Name (RoleName) from View
	/// and this will be used to assign role to User
	/// </summary>
	public class UserInRole
	{
		public string UserName { get; set; }
		// for RoleName
		public string Name { get; set; }
	}
}
