﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.DataAccess.Models;
using Core.DataAccess.Services;
using System.Net.Mime;

namespace Core_API.Controllers
{
	/// <summary>
	/// The ROuteAttrinbute, that will be used by the Endpoint Mapper to 
	/// Map the Current HTTP Request with the COntroller class
	/// </summary>
	[Route("api/[controller]")]
	// Used to Map the Incomming HTTP Request to Corresponding method and
	// IMP***, used to read the Data from HTTP Request Body to the CLR Object
	// tyhe POST and PUT Request will be mapped with its request body to CLR Object
	 [ApiController]
	 // COnfiguring the API for Messaging Format
	 [Produces(MediaTypeNames.Application.Json)]
	 [Consumes(MediaTypeNames.Application.Json)]
	public class DepartmentController : ControllerBase
	{
		private readonly IService<Department, int> deptServ;

		public DepartmentController(IService<Department, int> serv)
		{
			deptServ = serv;
		}
		// Defining OpenAPI Method Names using HTTP Method Template for Requests 
		[HttpGet("/department/getall")]
		public async Task<IActionResult> GetAsync()
		{
			var res = await deptServ.GetAsync();
			return Ok(res);
		}
		[HttpGet("/department/getone/{id}")]
		public async Task<IActionResult> GetAsync(int id)
		{
			var res = await deptServ.GetAsync(id);
			if (res == null) return NotFound($"Record based on {id} is not found");
			return Ok(res);
		}

		// http://localhost:[port]/api/Department?DeptNo=40&DeptName=IT&Location=Pune
		//[HttpPost("{DeptNo}/{DeptName}/{Location}")]
		//public async Task<IActionResult> PostAsync([FromBody]Department dept)
		//public async Task<IActionResult> PostAsync([FromQuery] Department dept)
		// public async Task<IActionResult> PostAsync([FromRoute] Department dept)
		[HttpPost("/department/create")]
		public async Task<IActionResult> PostAsync(Department dept)
		{
			try
			{
				if (ModelState.IsValid)
				{
					if (dept.DeptNo < 0) throw new Exception("Value supplied for DeptNo in invalid");
					var res = await deptServ.CreateAsync(dept);
					return Ok(res);
				}
				return BadRequest(ModelState);
			}
			catch (Exception ex)
			{
				return BadRequest(ex.Message);
			}
		}
		[HttpPut("/department/update/{id}")]
		public async Task<IActionResult> PutAsync(int id, Department dept)
		{
			// check for all possible errors 

			if (id != dept.DeptNo)
				return NotFound($"URL Parameter and CLR Object Value Does not match");

			// check for not found
			var rec = await deptServ.GetAsync(id);
			if(rec == null)
				return NotFound($"Record based on {id} is not found");

			if (ModelState.IsValid)
			{
				var res = await deptServ.UpdateAsync(id,dept);
				return Ok(res);
			}
			return BadRequest(ModelState);
		}

		[HttpDelete("/department/delete/{id}")]
		public async Task<IActionResult> DeleteAsync(int id)
		{
			var rec = await deptServ.GetAsync(id);
			if (rec == null)
				return NotFound($"Record based on {id} is not found");
			  await deptServ.DeleteAsync(id);
			return Ok($"Record based on {id} is deleted successfully");
		}
	}
}
