﻿using System;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;

class Program
{

    static async Task Main(string[] args)
    {
        var superCatagories = new SuperCatagories();
        var superCats = await superCatagories.GetSuperCatsAsync();

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
            if (superCats.Exists(sc => sc.SuperCatId.ToString() == searchTerm))
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

        var productLines = new ProductLines();
        var productLinesTest = await productLines.GetProductLinesAsync(gamingSuperCatIds.ToList());

        foreach (var productLine in productLinesTest)
        {
            Console.WriteLine($"{productLine.ProductLineId}: {productLine.ProductLineName}");
        }
    }
}