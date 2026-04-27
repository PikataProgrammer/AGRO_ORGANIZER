using Microsoft.Extensions.Caching.Memory;

namespace AgroOrganizer.Controllers;

public class MarketController
{
    public static WebApplication SetUpMarketRoutes(WebApplication app, string baseRoute)
    {
        app.MapGet(baseRoute + "/prices", (IMemoryCache cache) =>
        {
            // Check if the scrapper write info 
            var wheat = cache.TryGetValue("WheatPrice", out decimal w) ? w : 0;
            var corn = cache.TryGetValue("CornPrice", out decimal c) ? c : 0;
            var sunflower = cache.TryGetValue("SunflowerPrice", out decimal s) ? s : 0;
            var barley = cache.TryGetValue("BarleyPrice", out decimal b) ? b : 0;
            var rapeseed = cache.TryGetValue("RapeseedPrice", out decimal r) ? r : 0;
            
            return Results.Ok(new 
            { 
                wheat = wheat,
                corn = corn,
                sunflower = sunflower,
                barley = barley,
                rapeseed = rapeseed,
                currency = "EUR/тон",
                lastUpdated = DateTime.Now
            });

        }).WithName("GetAllMarketPrices").WithTags("Market");

        return app;
    }
}