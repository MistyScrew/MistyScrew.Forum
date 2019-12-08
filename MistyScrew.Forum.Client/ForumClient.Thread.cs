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
        public async static Task<Post[]> Thread(int threadId)
        {
            using var httpClient = new HttpClient();

            var html_bytes = await httpClient.GetByteArrayAsync(BaseUrl + $"showflat.php?Number={threadId}");
            var html_raw = Ru.GetString(html_bytes);
            html_raw = html_raw.Replace("this.scrollHeight > 49", "");
            var xhtml = NitroBolt.HtmlParsing.HtmlHelper.LoadXElementFromText(html_raw);

            var htmlBody = xhtml.Element("body");

            var bordersTable = htmlBody.Elements("table").ElementAtOrDefault(3)?.Descendants("table")
                .FirstOrDefault(_table => _table.Attribute("class")?.Value == "tableborders");

            var postTable = bordersTable?.Elements("tr").ElementAtOrDefault(1)?.Element("td")?.Element("table");
            var postTrs = (postTable?.Elements("tr")?.ToArray()).OrEmpty();

            var posts = new List<Post>();
            for (var i = 0; i < postTrs.Length; i+=2)
            {
                var postHeaderTr = postTrs[i];
                var postBodyTr = postTrs[i + 1];

                var userTd = postHeaderTr?.Elements("td")?.ElementAtOrDefault(0);
                var subjectTd = postHeaderTr?.Elements("td")?.ElementAtOrDefault(1);
                var bodyTd = postBodyTr?.Element("td");

                var title = subjectTd?.Element("table")?.Element("tr")?.Element("td")?.Element("b")?.Value?.Trim();
                var postIdName = userTd?.Element("a")?.Attribute("name")?.Value;
                var postId = ConvertHlp.ToInt(postIdName?.StartsWith("Post") == true ? postIdName.Substring("Post".Length) : postIdName);

                if (postId == null || title?.EmptyAsNull() == null)
                    continue;


                var postBody = bodyTd?.Element("table")?.Element("tr")?.Element("td")?.Element("font");

                var bodyHtml = postBody.Nodes().Select(node => node.ToString()).JoinToString();

                posts.Add(new Post(postId.Value, title, bodyHtml));
            }

            return posts.ToArray();

        }
        //static Thread ParseThreadTr(XElement threadTr)
        //{
        //    var titleTd = threadTr.Element("td");
        //    var a = titleTd.Element("a");

        //    var args = ParseQuery(a?.Attribute("href")?.Value);

        //    var title = a?.Value?.Trim();
        //    var id = args.Find("Main")?.FirstOrDefault();

        //    if (id == null)
        //        return null;

        //    return new Thread(id: id, title: title);
        //}
    }


 
}
