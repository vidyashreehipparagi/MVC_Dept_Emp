using System.ComponentModel.DataAnnotations;

namespace MVC_Dept_Emp.Models
{
    public class Department
    {
        [Key]
        [ScaffoldColumn(false)]
        public int Did { get; set; }
        [Required]
        public string Dname { get; set; }
    }
}
