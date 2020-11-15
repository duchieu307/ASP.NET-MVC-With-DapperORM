using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using System.Web;
using MVCDapperCRUD.Models;
using Dapper;

namespace MVCDapperCRUD.Controllers
{
    public class EmployeeController : Controller
    {
        public IActionResult Index()
        {
            return View(DapperORM.ExecuteReturnList<EmployeeModel>("EmployeeViewAll",null));
        }

        [HttpGet]
        public IActionResult AddOrEdit(int id = 0)
        {
            if (id == 0)
            {
                return View();
            } else
            {
                DynamicParameters param = new DynamicParameters();
                param.Add("@EmployeeID", id);
                return View(DapperORM.ExecuteReturnList<EmployeeModel>("EmployeeViewByID", param)
                    .FirstOrDefault<EmployeeModel>());
            }
        }

        [HttpPost]
        public IActionResult AddOrEdit(EmployeeModel emp)
        {
            DynamicParameters param = new DynamicParameters();

            param.Add("@EmployeeID", emp.EmployeeID);
            param.Add("@Name", emp.Name);
            param.Add("@Position", emp.Position);
            param.Add("@Age", emp.Age);
            param.Add("@Salary", emp.Salary);
            DapperORM.ExecuteWithoutReturn("EmployeeAddOrEdit", param);

            return RedirectToAction("Index");
        }

        public IActionResult DeleteEmployee(int id)
        {
            DynamicParameters param = new DynamicParameters();
            param.Add("@EmployeeID", id);
            DapperORM.ExecuteWithoutReturn("EmployeeDeleteByID", param);
            return RedirectToAction("Index");
        }
    }
}
