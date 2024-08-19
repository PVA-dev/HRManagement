namespace HRManagement.Models
{
	public class Position : BaseEntity
	{
		public string Name { get; set; }
		public bool IsDeleted { get; set; } = false;
	}
}
