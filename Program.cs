using System;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

class Program
{
    static async Task Main(string[] args)
    {
        var superCatagories = new SuperCatagories();
        var superCats = await superCatagories.GetSuperCatsAsync();
        foreach (var superCat in superCats)
        {
            Console.WriteLine($"SuperCatId: {superCat.SuperCatId}, SuperCatFriendlyName: {superCat.SuperCatFriendlyName}");
        }
    }
}