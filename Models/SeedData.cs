#region Using statements
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using BensModManager.Data;
using System;
using System.Linq;
#endregion

namespace BensModManager.Models
{
    public static class SeedData
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using (var context = new BensModManagerContext(
                serviceProvider.GetRequiredService<
					DbContextOptions<BensModManagerContext>>()))
            {
                //Check if any records exist
                if (context.Mods.Any())
                {
                    return;
                }

                context.Mods.AddRange(
                    new Mod
                    {
                        ModName = "MST Intake and Turbo Inlet",
                        Price = 443.59M,
                        ModType = "Performance",
                        Obsolete = false,
                        Notes = "Blow out filter debris every month",
                        FileName = "",
                        FileType = "",
                        FileExtension = "",
                        FilePath = ""
                    },

                    new Mod
                    {
                        ModName = "Wing Mirrors",
                        Price = 30.90M,
                        ModType = "Exterior",
                        Obsolete = true,
                        Notes = "Stored in garage with OEM mirror caps",
                        FileName = "",
                        FileType = "",
                        FileExtension = "",
                        FilePath = ""
                    },
                    
                    new Mod
                    {
                        ModName = "OHC Steering Wheel",
                        Price = 641.50M,
                        ModType = "Interior",
                        Obsolete = false,
                        Notes = "Torque spec: 65Nm",
                        FileName = "",
                        FileType = "",
                        FileExtension = "",
                        FilePath = ""
                    }
                );
                context.SaveChanges();
            }
        }
    }
}