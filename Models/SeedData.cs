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
                // Look for any mods.
                if (context.Mod.Any())
                {
                    
                    return;   // DB has been seeded
                }

                context.Mod.AddRange(
                    new Mod
                    {
                        ModName = "MST Intake and Turbo Inlet",
                        Price = 443.59M,
                        ModType = "Performance",
                    },

                    new Mod
                    {
                        ModName = "Motech Performance Wheel Spacers & Locking Nut",
                        Price = 214.95M,
                        ModType = "Exterior",
                    },
                    
                    new Mod
                    {
                        ModName = "OHC Steering Wheel",
                        Price = 641.50M,
                        ModType = "Interior",
                    }
                );
                context.SaveChanges();
            }
        }
    }
}