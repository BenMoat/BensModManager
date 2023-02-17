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
                // Look for any vehicles.
                if (context.Modification.Any())
                {
                    
                    return;   // DB has been seeded
                }

                context.Modification.AddRange(
                    new Modification
                    {
                        Mod = "Ferrari",
                        //Price = 0.47,
                        ModType = "TEST",
                    },

                    new Modification
                    {
                        Mod = "BMW",
                        //Price = "TEST",
                        ModType = "TEST",
                    },
                    
                    new Modification
                    {
                        Mod = "Abarth",
                        //Price = "TEST",
                        ModType = "TEST",
                    }
                );
                context.SaveChanges();
            }
        }
    }
}