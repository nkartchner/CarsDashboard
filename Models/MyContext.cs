using Microsoft.EntityFrameworkCore;
namespace Cars_and_Owners.Models
{
	public class MyContext : DbContext
	{
		public MyContext(DbContextOptions options) : base(options) { }

		public DbSet<User> Users { get; set; }
		public DbSet<Car> Cars { get; set; }
		public DbSet<Like> Likes { get; set; }
	}
}
