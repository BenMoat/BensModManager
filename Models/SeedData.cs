using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using ForzaColourSearch.Data;
using System;
using System.Linq;

namespace ForzaColourSearch.Models
{
    public static class SeedData
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using (var context = new ForzaColourSearchContext(
                serviceProvider.GetRequiredService<
					DbContextOptions<ForzaColourSearchContext>>()))
            {
                // Look for any vehicles.
                if (context.Vehicle.Any())
                {
                    
                    return;   // DB has been seeded
                }

                context.Vehicle.AddRange(
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