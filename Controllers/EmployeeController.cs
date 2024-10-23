using Office.Models;
using Office.Persistance;
using Office.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Office.Controllers
{
    public class EmployeeController : Controller
    {
        List<Project> Projects = ProjectCrud.GetProjects();
        List<Skill> Skills = SkillsCrud.GetSkills();
        List<Department> Departments = DepartmentCrud.GetDepartments();
        List<Team> Teams = TeamCrud.GetTeams();

        public ActionResult Index()
        {
            var Emp = new Employee() 
            { 
                DateOfBirth = DateTime.Now, 
                JoiningDate = DateTime.Now 
            };
            var viewModel = new EmployeeFormViewModel
            {
                Employee = Emp,
                Projects = Projects,
                Skills = Skills,
                Departments = Departments,
                Teams = Teams
            };
            return View("Office",viewModel);
        }

        [HttpPost]
        public ActionResult List()
        {
            var listModel = EmployeeCrud.GetEmployees();
            return PartialView("List", listModel);
        }
        [HttpPost]
        public ActionResult Form(int id)
        {
            if(id <= 0)
            {
                var Emp = new Employee() { DateOfBirth = DateTime.Now, JoiningDate = DateTime.Now, Id =0 };
                var viewModel = new EmployeeFormViewModel
                {
                    Employee = Emp,
                    Projects = Projects,
                    Skills = Skills,
                    Departments = Departments,
                    Teams = Teams
                };
                return PartialView("EmployeeNew", viewModel);
            }
            else
            {
                var Employee = EmployeeCrud.GetEmployeeById(id);
                
                var editModel = new EmployeeFormViewModel
                {
                    Employee = Employee,
                    Projects = Projects,
                    Skills = Skills,
                    Departments = Departments,
                    Teams = Teams,
                    SelectedSkills = EmployeeSkillCrud.GetEmployeeSkillByEmpId(id).Select(s => s.Id).ToArray(),
                    SelectedProjects = EmployeeProjectCrud.GetEmployeeProjectByEmpId(id).Select(s => s.Id).ToArray()
                };
                return PartialView("EmployeeNew", editModel);
            }
            
        }
        [HttpPost]
        public void Save(EmployeeFormViewModel model)
        {
                var emp = model.Employee;
                EmployeeCrud.InsertOrUpdateEmployeesWithProjectsAndSkills(emp, model.SelectedProjects, model.SelectedSkills);
        }

        [HttpPost]
        public void DeleteEmployee(string ids)
        {
            var idArray = ids.Split(',');

            foreach (var id in idArray)
            {
                EmployeeCrud.DeleteEmployeeWithProjectsAndSkills(int.Parse(id));
            }
        }

    }

}
