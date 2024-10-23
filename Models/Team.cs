using System.ComponentModel.DataAnnotations;

namespace Office.Models
{
    public class Team
    {
        public int Id { get; set; }
        [Required]
        [StringLength(100)]
        public string Name { get; set; }
    }
}