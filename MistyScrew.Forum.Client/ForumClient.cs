using NitroBolt.Functional;
using System;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace MistyScrew.Forum
{
    public class ForumClient
    {
        public async static Task<Area[]> Areas()
        {
            using var httpClient = new HttpClient();

            var html_bytes = await httpClient.GetByteArrayAsync("https://forumbgz.ru/ubbthreads.php");
            var html_raw = Ru.GetString(html_bytes);
            var xhtml = NitroBolt.HtmlParsing.HtmlHelper.LoadXElementFromText(html_raw);

            var body = xhtml.Element("body");

            var areaTables = body.Elements("table").ElementAtOrDefault(2)?.Descendants("table").Where(_table => _table.Attribute("class")?.Value == "tableborders").ToArray();

            return areaTables
                .Select(areaTable => TableToArea(areaTable))
                .ToArray();

        }
        static Area TableToArea(XElement areaTable)
        {
            var title = areaTable.Elements("tr").FirstOrDefault()?.Elements("td").FirstOrDefault()?.Value?.Trim();
            var boards = (areaTable.Elements("tr").Skip(1).Select(tr => TrToBoard(tr))).OrEmpty();

            return new Area(title, boards.Where(board => board != null).ToArray());
        }
        static Board TrToBoard(XElement tr)
        {
            var titleTd = tr.Elements("td").ElementAtOrDefault(1);

            var title = titleTd?.Element("font")?.Element("a")?.Value?.Trim();
            var name = title;
            var description = titleTd.Element("table")?.Elements("tr").ElementAtOrDefault(0)?.Elements("td").ElementAtOrDefault(1)?.Value?.Trim();

            if (name == null)
                return null;

            return new Board(name, title, description);
        }
        static System.Text.Encoding Ru = System.Text.Encoding.GetEncoding(1251);
    }
    public partial class Area
    {
        public readonly string Name;
        public readonly Board[] Boards;
    }

    public partial class Board
    {
        public readonly string Name;
        public readonly string Title;
        public readonly string Description;
        public readonly User_Name[] Moderators;
        public bool? IsFlashed;
    }
    public partial class BoardState
    {
        public readonly int? ThreadCount;
        public readonly int? PostCount;
        public readonly BoardStateLastPost LastPost;
    }
    public partial class BoardStateLastPost
    {
        public string Id;
        public User_Name User;
        public DateTime Time;
    }
    public partial class User_Name
    {
        public readonly string Name;
    }
}
