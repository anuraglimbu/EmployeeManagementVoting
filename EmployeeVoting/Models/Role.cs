using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace EmployeeVoting.Models
{
    public class Role
    {
        [Key]
        public int role_id { get; set; }

        [Display(Name = "Department Id")]
        [Required(ErrorMessage = "You must give the Id of the Department")]
        public int department_id { get; set; }

        [Display(Name = "Department Role")]
        [Required(ErrorMessage = "You must give the name of Department Role")]
        [DataType(DataType.Text)]
        [StringLength(30, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 2)]
        public string role_name { get; set; }
    }
}
