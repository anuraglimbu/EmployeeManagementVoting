using System.ComponentModel.DataAnnotations;

namespace EmployeeVoting.Models
{
	public class Employee
	{
		[Key]
		public int employee_id { get; set; }

		[Display(Name = "Employee Name")]
		[Required(ErrorMessage = "You must give the department name")]
		[DataType(DataType.Text)]
		[StringLength(30, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 2)]
		public string employee_name { get; set; }

		[Display(Name = "Date of Birth")]
		[Required(ErrorMessage = "You must give the date of birth")]
		[DataType(DataType.Date)]
		public DateTime employee_dob { get; set; }

		

		[Display(Name = "Email")]
		[Required(ErrorMessage = "You must give the date of birth")]
		[StringLength(30, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 7)]
		[DataType(DataType.EmailAddress)]
		public string employee_email { get; set; }

		[Display(Name = "Role Id")]
		[Required(ErrorMessage = "You must give the Id of the Department")]
		public int role_id { get; set; }
	}
}
