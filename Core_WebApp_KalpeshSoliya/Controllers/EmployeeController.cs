using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core_DataAccess_KalpeshSoliya.Models;
using Core_DataAccess_KalpeshSoliya.Services;
using Core_WebApp_KalpeshSoliya.Models;
using Microsoft.AspNetCore.Http;
using Core_WebApp_KalpeshSoliya.CustomSession;
using Microsoft.AspNetCore.Authorization;

namespace Core_WebApp_KalpeshSoliya.Controllers
{
    public class EmployeeController : Controller
    {
        CustomValidator valid = new CustomValidator();

        private readonly IService<Emp, int> empServ;
        private readonly IService<Dept, int> deptServ;
        private readonly IServiceEmpDept empdeptServ;
        public EmployeeController(IService<Emp, int> _emp_serv, IService<Dept, int> _dept_serv, IServiceEmpDept _empdept_serv)
        {
            empServ = _emp_serv;
            deptServ = _dept_serv;
            empdeptServ = _empdept_serv;
        }
        [Authorize(Roles = "Admin,Lead,Manager")]
        public async Task<IActionResult> Index()
        {
            var dept = HttpContext.Session.GetObject<Dept>("Dept");
            var deptNo = HttpContext.Session.GetInt32("DepartmentNo");
            if (deptNo == null || deptNo == 0 )
            {
                var emps = await empServ.GetAsync();
                return View(emps);
            }

            var data = empServ.GetAsync().Result.Where(e => e.DeptId == deptNo);
            return View(data);
        }

        [Authorize(Roles = "Admin,Lead")]
        public async Task<IActionResult> Create()
        {
            var dept = await deptServ.GetAsync();
            ViewBag.Department = dept;
            var emp = new Emp();
            return View(emp);
        }

        [HttpPost]
        public async Task<IActionResult> Create(Emp _emp)
        {
            //try
            //{
                if(ModelState.IsValid)
                {
                    if (!valid.IsValid(_emp.EmpId)) throw new Exception("EmpId cannot be -ve");
                    if (!valid.IsValid(_emp.EmpSalary)) throw new Exception("EmpSalary cannot be -ve");
                    if (!empdeptServ.IsDepartmentCapable(_emp.DeptId == null ? 0 : _emp.DeptId.Value).Result) throw new Exception("Department capacity full");

                    var res = await empServ.CreateAsync(_emp);
                    return RedirectToAction("Index");
                }
                return View(_emp);
            //}
            //catch(Exception ex)
            //{
            //    return View("Error", new ErrorViewModel()
            //    {
            //        ControllerName = RouteData.Values["controller"].ToString(),
            //        ActionName = RouteData.Values["action"].ToString(),
            //        ErrorMessage = ex.Message
            //    }
            //    );
            //}
        }
        [Authorize(Roles = "Admin,Lead")]
        public async Task<IActionResult> Edit(int id)
        {
            var dept = await deptServ.GetAsync();
            ViewBag.Department = dept;
            var _emp = await empServ.GetAsync(id);
            return View(_emp);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, Emp _emp)
        {
            //try
            //{
                if(ModelState.IsValid)
                {
                    if (!valid.IsValid(_emp.EmpId)) throw new Exception("EmpId cannot be -ve");
                    if (!valid.IsValid(_emp.EmpSalary)) throw new Exception("EmpSalary cannot be -ve");
                    if (!empdeptServ.IsDepartmentCapable(_emp.DeptId == null ? 0 : _emp.DeptId.Value).Result) throw new Exception("Department capacity full");

                    var res = await empServ.UpdateAsync(id, _emp);
                    return RedirectToAction("Index");
                }
                return View(_emp);
            //}
            //catch(Exception ex)
            //{
            //    return View("Error", new ErrorViewModel()
            //    {
            //        ControllerName = RouteData.Values["controller"].ToString(),
            //        ActionName = RouteData.Values["action"].ToString(),
            //        ErrorMessage = ex.Message
            //    });
            //}
        }
        [Authorize(Roles = "Admin,Lead")]
        public async Task<IActionResult> Delete (int id)
        {
            await empServ.DeleteAsync(id);
            return RedirectToAction("Index");
        }

    }
}
