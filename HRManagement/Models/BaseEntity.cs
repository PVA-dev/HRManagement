using System.ComponentModel.DataAnnotations;

namespace HRManagement.Models
{
	public class BaseEntity
	{
		[Key]
		public int Id { get; set; }
	}
}
