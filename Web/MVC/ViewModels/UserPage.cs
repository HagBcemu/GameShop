namespace MVC.ViewModels
{
	public class UserPage
	{
		public string Name { get; set; }
		public string Email { get; set; }
		public List<Orders> Orders { get; set; }		
	}
}
