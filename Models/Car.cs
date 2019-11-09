using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Cars_and_Owners.Models
{
	public class Car
	{

		[Key]
		public int Id { get; set; }

		[Required(ErrorMessage = "Every Car definitely has one of these")]
		public string Make { get; set; }

		[Required(ErrorMessage = "Every Car definitely has one of these")]
		public string Model { get; set; }

		[Required(ErrorMessage = "Every Car definitely has one of these")]
		public int Year { get; set; }

		public string Color { get; set; }

		public List<Like> Likes { get; set; }
	}
}