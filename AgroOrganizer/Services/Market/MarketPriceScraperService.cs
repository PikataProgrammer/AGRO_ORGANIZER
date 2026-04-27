using HtmlAgilityPack;
using Microsoft.Extensions.Caching.Memory;

namespace AgroOrganizer.Services.Market;

public class MarketPriceScraperService : BackgroundService // for mechanisms that running in background
{
    private readonly IMemoryCache _memoryCache;
    private readonly ILogger<MarketPriceScraperService> _logger;

    public MarketPriceScraperService(IMemoryCache memoryCache, ILogger<MarketPriceScraperService> logger)
    {
        _memoryCache = memoryCache;
        _logger = logger;
    }
    
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            _logger.LogInformation("Започва извличане на цени от Зърноборса за всички култури...");

            // 1. ПШЕНИЦА (Тук ползвам твоя точен XPath, който вече знаем, че работи)
            await ScrapeAndCachePriceAsync(
                url: "https://zarnoborsa.com/stocks/wheat", 
                cacheKey: "WheatPrice", 
                xPath: "/html/body/section[3]/div/div/div/div/div[1]/div/div/table/tbody[4]/tr[1]/td[2]", 
                stoppingToken);

            // 2. ЦАРЕВИЦА 
            await ScrapeAndCachePriceAsync(
                url: "https://zarnoborsa.com/stocks/corn", 
                cacheKey: "CornPrice", 
                xPath: "/html/body/section[3]/div/div/div/div/div[1]/div/div/table/tbody[4]/tr[1]/td[2]", 
                stoppingToken);

            // 3. СЛЪНЧОГЛЕД
            await ScrapeAndCachePriceAsync(
                url: "https://zarnoborsa.com/stocks/sunflowerseed", 
                cacheKey: "SunflowerPrice", 
                xPath: "/html/body/section[3]/div/div/div/div/div[1]/div/div/table/tbody[4]/tr[1]/td[2]", 
                stoppingToken);

            // 4. ЕЧЕМИК
            await ScrapeAndCachePriceAsync(
                url: "https://zarnoborsa.com/stocks/barley", 
                cacheKey: "BarleyPrice", 
                xPath: "/html/body/section[3]/div/div/div/div/div[1]/div/div/table/tbody[3]/tr[1]/td[2]", 
                stoppingToken);
            
            // 5. РАПИЦА
            await ScrapeAndCachePriceAsync(
                url: "https://zarnoborsa.com/stocks/rapeseed", 
                cacheKey: "RapeseedPrice", 
                xPath: "/html/body/section[3]/div/div/div/div/div[1]/div/div/table/tbody[2]/tr[1]/td[2]", 
                stoppingToken);

            _logger.LogInformation("Всички цени са обновени.");

            // Stop the worker for 12 hours, because the site could block our IP
            await Task.Delay(TimeSpan.FromHours(12), stoppingToken);
        }
    }
    
    private async Task ScrapeAndCachePriceAsync(string url, string cacheKey, string xPath, CancellationToken stoppingToken)
    {
        try
        {
            var web = new HtmlWeb();
            // try to be real person
            web.UserAgent = "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/120.0.0.0 Safari/537.36";
            
            var document = await web.LoadFromWebAsync(url, stoppingToken);
            
            var priceNode = document.DocumentNode.SelectSingleNode(xPath);
            
            if (priceNode == null)
            {
                priceNode = document.DocumentNode.SelectSingleNode("//td[contains(text(), 'България')]/following-sibling::td");
            }

            if (priceNode != null)
            {
                var rawText = priceNode.InnerText
                    .Replace("€", "")
                    .Replace("лв", "")
                    .Replace("BGN", "")
                    .Replace("EUR", "")
                    .Replace(" ", "")
                    .Replace(",", ".") 
                    .Trim();
                
                if (decimal.TryParse(rawText, System.Globalization.CultureInfo.InvariantCulture, out decimal price))
                {
                    _memoryCache.Set(cacheKey, price, TimeSpan.FromHours(12));
                    _logger.LogInformation($"Успешно обновена цена за {cacheKey}: {price:F2} евро/тон");
                }
                else
                {
                    _logger.LogWarning($"Намерихме елемента за {cacheKey}, но не можахме да превърнем текста '{rawText}' в число.");
                }
            }
            else
            {
                _logger.LogWarning($"Скраперът не намери елемента за {cacheKey}. Провери дали XPath адресът е правилен!");
                
                var fallbackPrice = cacheKey switch {
                    "WheatPrice" => 199.50m,
                    "CornPrice" => 185.20m,
                    "SunflowerPrice" => 410.00m,
                    "BarleyPrice" => 170.80m,
                    "RapeseedPrice" => 482.88m,
                    _ => 0m
                };
                _memoryCache.Set(cacheKey, fallbackPrice, TimeSpan.FromHours(12));
            }
        }
        catch (Exception e)
        {
            _logger.LogError(e, $"Грешка при скрапинг на {url}");
        }
    }
}