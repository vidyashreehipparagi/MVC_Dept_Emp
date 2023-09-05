using System.ComponentModel.DataAnnotations;

namespace MVC_Dept_Emp.Models
{
    public class Employee
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string? Name { get; set; }
        [Required]

        public double Salary { get; set; }
        [Required]

        public string? Imageurl { get; set; }
        [Required]
        [Display(Name = "Dname")]

        public int Did { get; set; }
        [Display(Name = "Dname")]
        public string? Dname { get; set; }
    }
}
