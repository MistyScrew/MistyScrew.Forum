using NitroBolt.Functional;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace MistyScrew.Forum
{
    partial class ForumClient
    {
        public async static Task<Thread[]> Threads(string boardName)
        {
            using var httpClient = new HttpClient();

            var html_bytes = await httpClient.GetByteArrayAsync(BaseUrl + $"postlist.php?Board={boardName}");
            var html_raw = Ru.GetString(html_bytes);
            var xhtml = NitroBolt.HtmlParsing.HtmlHelper.LoadXElementFromText(html_raw);

            var body = xhtml.Element("body");

            var threadTable = body.Elements("table").ElementAtOrDefault(3)?.Descendants("table")
                .FirstOrDefault(_table => _table.Attribute("class")?.Value == "tableborders");

            var threads = (threadTable?.Elements("tr")).OrEmpty().Skip(1)
                .Select(tr => ParseThreadTr(tr))
                .Where(thread => thread != null)
                .ToArray();

            return threads;

        }
        static Thread ParseThreadTr(XElement threadTr)
        {
            var titleTd = threadTr.Element("td");
            var a = titleTd.Element("a");

            var args = ParseQuery(a?.Attribute("href")?.Value);

            var title = a?.Value?.Trim();
            var id = args.Find("Main")?.FirstOrDefault();

            if (id == null)
                return null;

            return new Thread(id: id, title: title);
        }
    }


 
}
