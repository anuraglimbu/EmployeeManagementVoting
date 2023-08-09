using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace EmployeeVoting.Models
{
	public class Contact
	{
		[Key]
		public int contact_id { get; set; }

		[Display(Name = "Phone Number")]
		[Required(ErrorMessage = "You must give the contact")]
		[DataType(DataType.PhoneNumber)]
		[StringLength(20, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 2)]
		public string employee_contact { get; set; }

		[Display(Name = "Employee Id")]
		[Required(ErrorMessage = "You must give the Id of the Employee")]
		public int employee_id { get; set; }
	}
}
