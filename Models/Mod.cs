#region Using statements
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Reflection;
#endregion

namespace BensModManager.Models
{
	public class Mod
	{
        public int ID { get; set; }

        [Display(Name = "Mod Name")]
		[Required]
        public string ModName { get; set; }

        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:c}")]
        [DataType(DataType.Currency)]
		[Required]
        [Column(TypeName = "decimal(18, 2)")]
        public decimal Price { get; set; }

        [Display(Name = "Mod Type")]
        [Required]
        public string ModType { get; set; }

        [Required]
        public bool Obsolete { get; set; }

        public string Notes { get; set; }

        [Display(Name = "File Name")]
        public string FileName { get; set; }

        [Display(Name = "File Type")]
        public string FileType { get; set; }

        [Display(Name = "File Extension")]
        public string FileExtension { get; set; }

        [Display(Name = "File Path")]
        public string FilePath { get; set; }
	}
}