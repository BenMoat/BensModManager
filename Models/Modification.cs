using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BensModManager.Models
{
	public class Modification
	{
		public int ID { get; set; }
		public string Mod { get; set; }
        public string Price { get; set; }
		public string ModType { get; set; }
	}
}