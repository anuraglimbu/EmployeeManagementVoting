using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace EmployeeVoting.Models
{
	public class EmployeeHistory
	{
		[Key]
		public int history_id { get; set; }

		[HiddenInput]
		public int employee_id { get; set; }

		[Display(Name = "Department Name")]
		[DataType(DataType.Text)]
		[StringLength(30, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 2)]
		public string department_name { get; set; }

		[Display(Name = "Role")]
		[DataType(DataType.Text)]
		[StringLength(30, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 2)]
		public string role_name { get; set; }

		public DateTime joined_role_date { get; set; }
	}
}
