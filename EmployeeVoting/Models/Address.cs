using System.ComponentModel.DataAnnotations;

namespace EmployeeVoting.Models
{
    public class Address
    {
        [Key]
        public int address_id { get; set; }

        [Display(Name = "Address")]
        [Required(ErrorMessage = "You must give the address name")]
        [DataType(DataType.Text)]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 2)]
        public string address_name { get; set; }

        [Display(Name = "Employee Id")]
        [Required(ErrorMessage = "You must give the Id of the Employee")]
        public int employee_id { get; set; }
    }
}
