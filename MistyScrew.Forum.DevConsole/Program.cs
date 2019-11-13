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

            var areas = await ForumClient.Areas();
            foreach (var area in areas)
            {
                Console.WriteLine($"a: {area.Name}");
                foreach (var board in area.Boards.OrEmpty())
                    Console.WriteLine($"  b:{board.Name} {board.Description}");
            }
        }
    }
}
