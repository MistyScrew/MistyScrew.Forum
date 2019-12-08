using Newtonsoft.Json;
using NitroBolt.Functional;
using System;
using System.Threading.Tasks;

namespace MistyScrew.Forum.DevConsole
{
    class Program
    {
        static async Task Main(string[] args)
        {
            System.Text.Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance);

            var posts = await ForumClient.Thread(13392407);
            Console.WriteLine(JsonConvert.SerializeObject(posts, Formatting.Indented));

            //var threads = await ForumClient.Threads("society");
            //Console.WriteLine(JsonConvert.SerializeObject(threads, Formatting.Indented));

            //var areas = await ForumClient.Areas();
            //Console.WriteLine(JsonConvert.SerializeObject(areas, Formatting.Indented));
            //foreach (var area in areas)
            //{
            //    Console.WriteLine($"a: {area.Name}");
            //    foreach (var board in area.Boards.OrEmpty())
            //        Console.WriteLine($"  b:{board.Name} {board.Description}");
            //}
            //foreach (var area in areas)
            //    Console.WriteLine();
        }
    }
}
