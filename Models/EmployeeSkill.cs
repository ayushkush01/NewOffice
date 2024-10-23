using System.ComponentModel.DataAnnotations;

namespace Office.Models
{
    public class EmployeeSkill
    {
        [Required]
        public int EmployeeId { get; set; }
        public string EmployeeName { get; set; }
        [Required]
        public int SkillId { get; set; }
        public string SkillName { get; set; }
    }
}