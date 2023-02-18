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

        [StringLength(75, MinimumLength = 3)]
		[Required]
        public string ModName { get; set; }

        [Range(1, 100)]
        [DataType(DataType.Currency)]
		[Required]
        [Column(TypeName = "decimal(18, 2)")]
        public decimal Price { get; set; }

        [StringLength(50, MinimumLength = 8)]
        [Required]
        public string ModType { get; set; }
	}
}