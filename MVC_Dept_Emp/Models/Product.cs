using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace MVC_Dept_Emp.Models
{
    public class Product
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string? Name { get; set; }
        [Required]

        public double Price { get; set; }
        [Required]

        public string? Imageurl { get; set; }
        [Required]
        [Display(Name = "Cname")]

        public int Cid { get; set; }
        [Display(Name = "Cname")]
        public string? Cname { get; set; }
    }
}
