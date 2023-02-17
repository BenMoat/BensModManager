using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BensModManager.Models
{
	public class Mod
	{
		public int ID { get; set; }
		public string ModName { get; set; }
        public string Price { get; set; }
		public string ModType { get; set; }
	}
}