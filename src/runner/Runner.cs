// Purpose: Contains the Runner class. This class is used to run the program.
using Microsoft.Extensions.Logging;
using Amelia.CEXPOC.Api;

namespace Amelia.CEXPOC.RunnerNamespace
{
    public class Runner
    {
        private readonly ILogger<Runner> _logger;
        private readonly ProductLines _productLines;
        private readonly SuperCatagories _superCatagories;

        public Runner(ILogger<Runner> logger, ProductLines productLines, SuperCatagories superCatagories)
        {
            _logger = logger;
            _productLines = productLines;
            _superCatagories = superCatagories;
        }

        public async Task Run()
        {
            _logger.LogInformation("Starting the application.");
            var superCats = await _superCatagories.GetSuperCatsAsync();

            if (null == superCats)
            {
                Console.WriteLine("No superCats found!");
                return;
            }

            foreach (var superCat in superCats)
            {
                Console.WriteLine($"{superCat.SuperCatId}: {superCat.SuperCatFriendlyName}");
            }

            var searchTerm = "";
            bool validInput = false;
            while (!validInput)
            {
                Console.WriteLine("\nEnter a superCatId:");
                searchTerm = Console.ReadLine();
                if (null != superCats && superCats.Exists(sc => sc.SuperCatId.ToString() == searchTerm))
                {
                    validInput = true;
                    searchTerm = superCats.Find(sc => sc.SuperCatId.ToString() == searchTerm).SuperCatFriendlyName;
                }
                else
                {
                    Console.WriteLine("Invalid input! Select valid catagoryID.");
                }
            }

            var gamingSuperCatIds = superCats.Where(sc => sc.SuperCatFriendlyName == searchTerm).Select(sc => sc.SuperCatId);

            var productLinesTest = await _productLines.GetProductLinesAsync(gamingSuperCatIds.ToList());

            foreach (var productLine in productLinesTest)
            {
                Console.WriteLine($"{productLine.ProductLineId}: {productLine.ProductLineName}");
            }
        }
    }
}