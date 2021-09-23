using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core_DataAccess_KalpeshSoliya.Models;
using Core_DataAccess_KalpeshSoliya.Services;
using Core_WebApp_KalpeshSoliya.Models;

namespace Core_WebApp_KalpeshSoliya.Controllers
{
    /// <summary>
    /// Request Facilitator, the object that is responsible to respond to the
    /// incomming request
    /// Please do not write Business Logic, Data Access Logic in COntroller
    /// Class used for, Request Processing using Action Filters for Security, Exception and any other custom logical filter
    /// If All is well, then Execute Action Mathods based on Request Type(?) 
    /// Request Type: GET, POST, PUT, DELETE, The Controller will Invoke the Action Method based on Request Type
    /// By Default Each Action Method is HttpGet.
    /// Apply HttpPost on the action methods those will be used to accepting data from
    /// HTTP Request Body
    /// The Controller is a base class for MVC Controller
    /// The ViewBag and ViewData, Properties to pass values from COntroller's Action Method top View and Back
    /// The TempData , property used to pass data across controlers
    /// </summary>
    public class DepartmentController: Controller
    {
        CustomValidator valid = new CustomValidator();

        // Dependency Injection of Department Service
        // that is registered in ConfigureServices() method of Startup class   
        private readonly IService<Dept, int> DeptServ;
        public DepartmentController(IService<Dept, int> serv)
        {
            DeptServ = serv;
        }

        /// <summary>
		/// IActionResult is contract that represent the Response after the execution
		/// ViewResult, JsonResult, EmptyResult, FileResult, OkResult, OkObjectResult, etc.
		/// To Add a view (aka View Scaffolding) right-click  inside the action method
		/// and select Add View
		/// </summary>
		/// <returns></returns>
        public async Task<IActionResult> Index()
        {
            var depts = await DeptServ.GetAsync();
            return View(depts);
        }

        /// <summary>
        /// Action Method  that will respons a view with Empty Department Object
        /// </summary>
        /// <returns></returns>
        public IActionResult Create()
        {
            var dept = new Dept();
            return View(dept);
        }

        /// <summary>
        /// The Request will be accepted as Http Post from the Browser 
        /// </summary>
        /// <param name="department"> Data mapped from Http Posted Body </param>
        /// <return></return>
        [HttpPost]
        public async Task<IActionResult> Create(Dept _department)
        {
            try
            {
                //check if model is valid
                if(ModelState.IsValid)
                {
                    if (_department.DeptId < 0) throw new Exception("DeptId cannot be -ve");
                    if (!valid.IsValid(_department.Capacity)) throw new Exception("Capacity cannot be -ve");
                    var res = await DeptServ.CreateAsync(_department);
                    //Redirect to the Action
                    return RedirectToAction("Index");
                }
                //if Model is not valid then stay on same page with error message
                return View(_department);
            }
            catch(Exception ex)
            {
                //Catch the Exception and redirect to the Error Page for View/Share folder
                return View("Error", new ErrorViewModel()
                {
                    //Read route expression to extract current execution controller and its Action method
                    //The 'controller' expresion is read from Route Expression of Startup Class
                    ControllerName = RouteData.Values["controller"].ToString(),
                    ActionName = RouteData.Values["action"].ToString(),
                    ErrorMessage = ex.Message
                });
            }
        }

        ///<summary>
        ///Search Department based on Id
        /// The id will be added as in HRRP Request URL
        /// http://server:port/Deaprtment/Edit/id
        ///</summary>
        ///<param name="id"></param>
        ///<return></return>
        public async Task<IActionResult> Edit(int id)
        {
            var dept = await DeptServ.GetAsync(id);
            return View(dept);
        }

        /// <summary>
        /// The Request will be accespted as HTTP post from the Browser
        /// </summary>
        /// <Param name="Department"> Data mapped from Http Posted Body</Param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> Edit(int id, Dept dept)
        {
            try
            {
                //check if the model is vaild
                if(ModelState.IsValid)
                {
                    if (!valid.IsValid(dept.Capacity)) throw new Exception("Capacity cannot be -ve");
                    var res = await DeptServ.UpdateAsync(id, dept);
                    return RedirectToAction("Index");
                }
                return View(dept);
            }
            catch(Exception ex)
            {
                throw;
            }
        }

        public async Task<IActionResult> Delete(int id)
        {
            await DeptServ.DeleteAsync(id);
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> ListDept()
        {
            var depts = await DeptServ.GetAsync();
            ViewBag.Dept = depts;
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> ListDept(Dept d)
        {
            var depts = await DeptServ.GetAsync();
            ViewBag.Dept = depts;
            ViewBag.DeptId = d.DeptId;
            var dd = await DeptServ.GetAsync(d.DeptId);
            ViewBag.DeptName = dd.DeptName;
            ViewBag.Capacity = dd.Capacity;
            return View("ListDept");
        }

        public async Task<IActionResult> ListEmp()
        {
            var _dept = await DeptServ.GetAsync();
            ViewBag.Employee = _dept.SelectMany(d => d.Emps).ToList();
            ViewBag.DeptId = 0;
            return View(_dept);
        }

        [HttpPost]
        public async Task<IActionResult> ListEmp(int DeptId)
        {
            var _dept = await DeptServ.GetAsync();
            ViewBag.Employee= _dept.Where(d => d.DeptId == DeptId).SelectMany(d => d.Emps).ToList();
            ViewBag.DeptId = DeptId;
            return View(_dept);
        }
    }
}
