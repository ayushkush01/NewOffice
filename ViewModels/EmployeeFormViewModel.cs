using Office.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Office.ViewModels
{
    public class EmployeeFormViewModel
    {
        public List<Employee> Employees { get; set; }
        public Employee Employee { get; set; }
        public List<Skill> Skills { get; set; }
        public List<Project> Projects { get; set; }
        public List<Team> Teams { get; set; }
        public List<Department> Departments { get; set; }
        public int[] SelectedSkills { get; set; }
        public int[] SelectedProjects { get; set; }
    }
}