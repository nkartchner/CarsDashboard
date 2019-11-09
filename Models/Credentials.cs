using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Cars_and_Owners.Models
{
	public class Credentials
	{

		[Required(ErrorMessage = "Don't want things to be secure?")]
		[EmailAddress]
		public string Email { get; set; }

		[DataType(DataType.Password)]
		[Display(Name = "Password")]
		[Compare("Password", ErrorMessage = "LOL. You Muffed")]
		[Required(ErrorMessage = "You Muffed.")]
		public string Password { get; set; }

	}
}