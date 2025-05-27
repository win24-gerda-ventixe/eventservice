using Infrastructure.Contexts;
using Infrastructure.Entities;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Seeds
{
    public static class AppDbSeeder
    {
        public static async Task SeedEvents(IServiceProvider services)
        {
            using var scope = services.CreateScope();
            var context = scope.ServiceProvider.GetRequiredService<DataContext>();

            if (!context.Events.Any())
            {
                // Skapa några paketer
                var packages = new List<PackageEntity>
        {
            new() { Title = "Standard", SeatingArrangment = "Rader", Placement = "Balkong", Price = 199.99m, Currency = "SEK" },
            new() { Title = "Premium", SeatingArrangment = "Bord", Placement = "Främre raden", Price = 499.99m, Currency = "SEK" },
            new() { Title = "VIP", SeatingArrangment = "Lounge", Placement = "Center", Price = 999.99m, Currency = "SEK" }
        };

                await context.Packages.AddRangeAsync(packages);
                await context.SaveChangesAsync(); // Spara först för att generera PackageId

                // Skapa 10 event
                var random = new Random();
                var titles = new[] { "Konsert", "Teater", "Stand-up", "Föreläsning", "Festival", "Filmvisning", "Workshop", "Mässa", "Opera", "Dansshow" };
                var locations = new[] { "Stockholm", "Göteborg", "Malmö", "Uppsala", "Västerås" };
                var categories = new[] { "Musik", "Kultur", "Utbildning", "Underhållning" };
                var statuses = new[] { "Aktiv", "Inställd", "Fullbokad" };

                var events = new List<EventEntity>();

                for (int i = 0; i < 10; i++)
                {
                    var ev = new EventEntity
                    {
                        Title = titles[i],
                        Description = $"Beskrivning för {titles[i]}",
                        Location = locations[random.Next(locations.Length)],
                        Price = random.Next(100, 1000),
                        EventDate = DateTime.Today.AddDays(random.Next(10, 100)),
                        Time = DateTime.Today.AddHours(random.Next(18, 23)),
                        Image = $"https://example.com/image{i}.jpg",
                        Category = categories[random.Next(categories.Length)],
                        Status = statuses[random.Next(statuses.Length)],
                        Packages = new List<EventPackageEntity>()
                    };

                    // Lägg till 1-3 slumpmässiga paket till varje event
                    var numberOfPackages = random.Next(1, 4);
                    var selectedPackages = packages.OrderBy(x => Guid.NewGuid()).Take(numberOfPackages).ToList();
                    foreach (var pkg in selectedPackages)
                    {
                        ev.Packages.Add(new EventPackageEntity
                        {
                            Event = ev,
                            Package = pkg
                        });
                    }

                    events.Add(ev);
                }

                await context.Events.AddRangeAsync(events);
                await context.SaveChangesAsync();
            }
        }

    }
}
