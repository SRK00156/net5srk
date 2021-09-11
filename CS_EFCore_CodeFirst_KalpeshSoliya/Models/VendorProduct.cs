using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CS_EFCore_CodeFirst_KalpeshSoliya.Models
{
    public class VendorProduct
    {
		[Key]
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public int VenderProductRowId { get; set; }
		[Required]
		// Expected to be a Foreign Key
		public int VenderRowId { get; set; }
		public Vender Vender { get; set; }
		[Required]
		// Expected to be a Foreign Key
		public int ProductRowId { get; set; }
		public Products Product { get; set; }

	}
}
