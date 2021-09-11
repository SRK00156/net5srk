using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CS_EFCore_CodeFirst_KalpeshSoliya.Models
{
    public class Vender
    {
		[Key]
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public int VenderRowId { get; set; }
		[Required]
		[StringLength(50)]
		public string VenderId { get; set; }
		[Required]
		[StringLength(200)]
		public string VenderName { get; set; }
		// Many-To-Many Relatioship
		//public ICollection<Productt> Products { get; set; }
		public ICollection<VendorProduct> VendorProducts { get; set; }
	}
}
