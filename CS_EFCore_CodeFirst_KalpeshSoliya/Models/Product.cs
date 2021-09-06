using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CS_EFCore_CodeFirst_KalpeshSoliya.Models
{
	public class Products
	{
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public int ProductRowId { get; set; }
		[Required]
		[StringLength(50)]
		public string ProductId { get; set; }
		[Required]
		[StringLength(200)]
		public string ProductName { get; set; }
		[Required]
		// Expected to be a Foreign Key
		public int CategoryRowId { get; set; }
		// The One-To-One Relatioship
		public Category Categtory { get; set; }
		// Many-To-Many Relatioship
		public ICollection<Vendors> Vendors { get; set; }
	}
}
