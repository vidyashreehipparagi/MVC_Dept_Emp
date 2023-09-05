using System.ComponentModel.DataAnnotations;

namespace MVC_Dept_Emp.Models
{
    public class Category
    {
        [Key]
        [ScaffoldColumn(false)]

        public int Cid { get; set; }
        [Required]
        public string? Cname { get; set; }
    }
}
