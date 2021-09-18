using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.DataAccess.Models;
using Core.DataAccess.Services;
using Core_WebApp.Models;
using Microsoft.AspNetCore.Http;
using Core_WebApp.CustomSessions;
using Microsoft.AspNetCore.Authorization;

namespace Core_WebApp.Controllers
{
	/// <summary>
	/// Request Facilitator, the object that is responsible to respond to the
	/// incomming request
	/// Please do not write Business Logic, Data Access Logic in COntroller
	/// Class used for, Request Processing using Action Filters for Security, Exception and any other custom logical filter
	/// If All is well, then Execute Action Mathods based on Request Type(?) 
	/// Request Type: GET, POST, PUT, DELETE, The Controller will Invoke the Action Method based on Request Type
	/// By Default Each Action Method is HttpGet.
	/// Apply HttpPost on the action methods thaose will be used to accepting data from
	/// HTTP Request Body
	/// The Controller is a base class for MVC COntroller
	/// The ViewBag and ViewData, Properties to pass values from COntroller's Action Method top View and Back
	/// The TempData , property used to pass data across controlers
	/// </summary>
	/// 
	 
	public class DepartmentController : Controller
	{
		// Dependency Injection of Department Service
		// that is registered in ConfigureServices() method of Startup class
		private readonly IService<Department,int> deptServ;
		public DepartmentController(IService<Department, int> serv)
		{
			deptServ = serv;
		}


		/// <summary>
		/// IActionResult is contract that represent the Response after the execution
		/// ViewResult, JsonResult, EmptyResult, FileResult, OkResult, OkObjectResult, etc.
		/// To Add a view (aka View Scaffolding) right-click  inside the action method
		/// and select Add View
		/// </summary>
		/// <returns></returns>
		/// 
		//[Authorize(Roles ="Admin,Manager,Clerk")]
		[Authorize(Policy ="AllRolePolicy")]
		public async Task<IActionResult> Index()
		{
			var depts = await deptServ.GetAsync();
			return View(depts);
		}

		/// <summary>
		/// Action Method  that will respons a view with Empty Department Object
		/// </summary>
		/// <returns></returns>
		/// 
		//[Authorize(Roles = "Admin,Manager")]
		[Authorize(Policy = "AdminManagerPolicy")]
		public IActionResult Create()
		{
			var dept = new Department();
			return View(dept);
		}

		/// <summary>
		/// The Request will be accepted as Http Post from the Browser
		/// </summary>
		/// <param name="department">Data mapped from Http Posted Body</param>
		/// <returns></returns>
		[HttpPost]
		public async Task<IActionResult> Create(Department department)
		{
			//try
			//{
				// check if the Model is Valid
				if (ModelState.IsValid)
				{
					if (department.DeptNo < 0) throw new Exception("DeptNo cannot be -ve");
					var res = await deptServ.CreateAsync(department);
					// Redirect to the Action
					return RedirectToAction("Index");
				}
				// if Model is not valid then Stay on Same page with Error Messages
				return View(department);
			//}
			//catch (Exception ex)
			//{
			//	// Ctach the exception and redirect to the Error page fro Views/Shared folder
			//	return View("Error", new ErrorViewModel()
			//	{
			//		// read route expression to extract Current Executing Controller 
			//		// and its  Action Method  
			//		//The 'controller' expresion is read from Route Expression of Startup class 
			//		ControllerName = RouteData.Values["controller"].ToString(),
			//		ActionName = RouteData.Values["action"].ToString(),
			//		ErrorMessage = ex.Message
			//	}); 
			//}
		}

		/// <summary>
		/// Serach Depatyment based on Id
		/// The id will be added as in HTTP Request URL
		/// http://server:port/Department/Edit/id
		/// </summary>
		/// <param name="id"></param>
		/// <returns></returns>
		/// 
		//[Authorize(Roles = "Admin")]
		[Authorize(Policy = "AdminPolicy")]
		public async Task<IActionResult> Edit(int id)
		{
			var dept = await deptServ.GetAsync(id);
			return View(dept);
		}

		/// <summary>
		/// The Request will be accepted as Http Post from the Browser
		/// </summary>
		/// <param name="department">Data mapped from Http Posted Body</param>
		/// <returns></returns>
		[HttpPost]
		public async Task<IActionResult> Edit(int id, Department department)
		{
			//try
			//{
				// check if the Model is Valid
				if (ModelState.IsValid)
				{
					var res = await deptServ.UpdateAsync(id,department);
					// Redirect to the Action
					return RedirectToAction("Index");
				}
				// if Model is not valid then Stay on Same page with Error Messages
				return View(department);
			//}
			//catch (Exception ex)
			//{
			//	throw;
			//}
		}
		//[Authorize(Roles = "Admin")]
		[Authorize(Policy = "AdminPolicy")]
		public async Task<IActionResult> Delete(int id)
		{
			await  deptServ.DeleteAsync(id);
			return RedirectToAction("Index");
		}

		public async Task<IActionResult> ListDept()
		{
			var depts = await deptServ.GetAsync();
			// Using ViewBag to send a collection to View
			ViewBag.Departments = depts;
			return View();
		}

		[HttpPost]
		public async Task<IActionResult> ListDept(Department d)
		{
			var depts = await deptServ.GetAsync();
			// Using ViewBag to send a collection to View
			ViewBag.Departments = depts;
			ViewBag.DeptNo = d.DeptNo;
			var dd = await deptServ.GetAsync(d.DeptNo);
			ViewBag.DeptName = dd.DeptName;
			return View("ListDept");
		}


		public IActionResult ShowDetails(int id)
		{

			HttpContext.Session.SetInt32("DeptNo",id);
			// Storee the Department object in session
			var dept = deptServ.GetAsync(id).Result;
			HttpContext.Session.SetObject<Department>("Dept",dept);

			return RedirectToAction("Index", "Employee");
		}

	}
}
