using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Cars_and_Owners.Models
{
	public class User
	{
		[Key]
		public int Id { get; set; }

		[Required(ErrorMessage = "Bro. Come on now")]
		[Display(Name = "First Name")]
		public string FirstName { get; set; }

		[Required(ErrorMessage = "No last name?? Right.....")]
		[Display(Name = "Last Name")]
		public string LastName { get; set; }

		[Required(ErrorMessage = "Still need one of these. Duh.")]
		[EmailAddress]
		public string Email { get; set; }

		[Required(ErrorMessage = "Don't want things to be secure?")]
		[DataType(DataType.Password)]
		public string Password { get; set; }

		[NotMapped]
		[DataType(DataType.Password)]
		[Display(Name = "Confirm Password")]
		[Compare("Password", ErrorMessage = "LOL. You Muffed")]
		[Required(ErrorMessage = "Need this so you know you typed things in right..")]
		public string ConfirmPassword { get; set; }

		public List<Like> Likes { get; set; }
	}
}