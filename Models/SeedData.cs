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
                if (context.Mod.Any())
                {
                    return;
                }

                context.Mod.AddRange(
                    new Mod
                    {
                        ModName = "MST Intake and Turbo Inlet",
                        Price = 443.59M,
                        ModType = "Performance",
                        Obsolete = false,
                        Notes = "Blow out filter debris every month",
                        FileName = "MST Intake Invoice",
                        FileType = "image/jpg",
                        FileExtension = ".jpg",
                        FilePath = "C:\\Users\\Ben Moat\\OneDrive\\Shared\\Documents\\Supra\\BensModManager\\wwwroot\\files\\Mst Intake Invoice.jpg"

                    },

                    new Mod
                    {
                        ModName = "Wing Mirrors",
                        Price = 30.90M,
                        ModType = "Exterior",
                        Obsolete = true,
                        Notes = "Stored in garage with OEM mirror caps",
                        FileName = "Wing Mirrors",
                        FileType = "image/png",
                        FileExtension = ".png",
                        FilePath = "C:\\Users\\Ben Moat\\OneDrive\\Shared\\Documents\\Supra\\BensModManager\\wwwroot\\files\\Wing Mirrors.png"
                    },
                    
                    new Mod
                    {
                        ModName = "OHC Steering Wheel",
                        Price = 641.50M,
                        ModType = "Interior",
                        Obsolete = false,
                        Notes = "Torque spec: 65Nm",
                        FileName = "AUTOID Steering Wheel Invoice",
                        FileType = "application/pdf",
                        FileExtension = ".pdf",
                        FilePath = "C:\\Users\\Ben Moat\\OneDrive\\Shared\\Documents\\Supra\\BensModManager\\wwwroot\\files\\AUTOID Steering Wheel Invoice.pdf"
                    }
                );
                context.SaveChanges();
            }
        }
    }
}