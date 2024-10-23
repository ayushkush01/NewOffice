using System.ComponentModel.DataAnnotations;

namespace Office.Models
{
    public class EmployeeProject
    {
        [Required]
        public int EmployeeId { get; set; }
        public string EmployeeName { get; set; }
        [Required]
        public int ProjectId { get; set; }
        public string ProjectName { get; set; }

    }
}