using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CS_EFCore_CodeFirst_KalpeshSoliya.Models
{
    public class Category
    {
		[Key]
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public int CategoryRowId { get; set; }
		[Required]
		[StringLength(50)]
		public string CategoryId { get; set; }
		[Required]
		[StringLength(200)]
		public string CategoryName { get; set; }
		// One-To-Many Relatioship
		public ICollection<Products> Products { get; set; }
	}
}
