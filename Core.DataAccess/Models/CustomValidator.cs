using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
namespace Core.DataAccess.Models
{
	public class NumericNonNegativeAttribute  : ValidationAttribute
	{
		public override bool IsValid(object value)
		{
			if (Convert.ToInt32(value) < 0) return false;
			return true;
		}
	}
}
