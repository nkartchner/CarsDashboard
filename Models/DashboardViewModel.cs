using System.Collections.Generic;

namespace Cars_and_Owners.Models
{
	public class DashboardViewModel
	{
		public User User { get; set; }
		public List<Car> Cars { get; set; }
		public Like Like { get; set; }
	}
}