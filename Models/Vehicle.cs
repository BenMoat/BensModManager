using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ForzaColourSearch.Models
{
	public class Vehicle
	{
		public int ID { get; set; }

		[StringLength(50, MinimumLength = 2)]
		public string Mod { get; set; }

        [Display(Name = "Price")]
        public string Price { get; set; }

        [Display(Name = "Name")]
        public string ModType { get; set; }

	}
}