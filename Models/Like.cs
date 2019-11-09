using System.ComponentModel.DataAnnotations;

namespace Cars_and_Owners.Models
{
	public class Like
	{
		[Key]
		public int Id { get; set; }
		[Required]
		public int UserId { get; set; }
		[Required]
		public int CarId { get; set; }


		public User User { get; set; }
		public Car Car { get; set; }
	}
}