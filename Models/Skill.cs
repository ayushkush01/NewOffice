using System.ComponentModel.DataAnnotations;

namespace Office.Models
{
    public class Skill
    {
        public int Id { get; set; }
        [StringLength(100)]
        [Required]
        public string Name { get; set; }
    }
}