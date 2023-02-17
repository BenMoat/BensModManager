using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using BensModManager.Data;
using System;
using System.Linq;

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
                // Look for any modifications.
                if (context.Modification.Any())
                {
                    
                    return;   // DB has been seeded
                }

                context.Modification.AddRange(
                    new Modification
                    {
                        Mod = "MST Intake and Turbo Inlet",
                        Price = "443.59",
                        ModType = "Performance",
                    },

                    new Modification
                    {
                        Mod = "Motech Performance Wheel Spacers & Locking Nut",
                        Price = "214.95",
                        ModType = "Exterior",
                    },
                    
                    new Modification
                    {
                        Mod = "OHC Steering Wheel",
                        Price = "641.50",
                        ModType = "Interior",
                    }
                );
                context.SaveChanges();
            }
        }
    }
}