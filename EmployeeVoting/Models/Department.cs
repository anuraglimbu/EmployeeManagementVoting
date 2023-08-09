using System.ComponentModel.DataAnnotations;

namespace EmployeeVoting.Models
{
    public class Department
    {
        [Key]
        public int department_id { get; set; }

        [Display(Name = "Department Name")]
        [Required(ErrorMessage = "You must give the department name")]
        [DataType(DataType.Text)]
        [StringLength(30, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 2)]
        public string department_name { get; set; }
    }
}
