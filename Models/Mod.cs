#region Using statements
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
#endregion

namespace BensModManager.Models
{
	public class Mod
	{
		public int ID { get; set; }

        [Display(Name = "Mod Name")]
		[Required]
        public string ModName { get; set; }

        [DataType(DataType.Currency)]
		[Required]
        [Column(TypeName = "decimal(18, 2)")]
        public decimal Price { get; set; }

        [Display(Name = "Mod Type")]
        [Required]
        public string ModType { get; set; }
	}
}