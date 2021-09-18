using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.DataAccess.Models;
using Core.DataAccess.Services;
using Microsoft.AspNetCore.Http;
using Core_WebApp.CustomSessions;
namespace Core_WebApp.Controllers
{
	public class EmployeeController : Controller
	{
		private readonly IService<Employee, int> empServ;

		public EmployeeController(IService<Employee, int> serv)
		{
			empServ = serv;
		}

		public async Task<IActionResult> Index()
		{

			var dept = HttpContext.Session.GetObject<Department>("Dept");
			// REad data from the session
			var deptNo = HttpContext.Session.GetInt32("DeptNo");
			if (deptNo == 0)
			{
				var emps = await empServ.GetAsync();
				return View(emps);
			}

			var data =  empServ.GetAsync().Result.Where(e=>e.DeptNo == deptNo);
			return View(data);


		}
	}
}
