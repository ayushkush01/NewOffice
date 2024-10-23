using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Office.Models
{
    public class Employee
    {
        public int Id { get; set; }
        [Required]
        [StringLength(255)]
        public string FirstName { get; set; }
        [Required]
        [StringLength(255)]
        public string LastName { get; set; }
        public DateTime DateOfBirth { get; set; }
        public DateTime JoiningDate { get; set; }
        
        public string Address { get; set; }
        [Required]
        public string State { get; set; }
        public Department Department { get; set; }
        [Required]
        public int DepartmentId { get; set; }
        public Team Team { get; set; }
        [Required]
        public int TeamId { get; set; }
        public List<Skill> Skills { get; set; }
        public List<Project> Projects { get; set; }
        public bool IsInsured { get; set; }
        [Required]
        public string Gender { get; set; }
    }
}