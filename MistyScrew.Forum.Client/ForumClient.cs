using NitroBolt.Functional;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace MistyScrew.Forum
{
    public partial class ForumClient
    {
        public static readonly string BaseUrl = "https://forumbgz.ru/";
        public async static Task<Area[]> Areas()
        {
            using var httpClient = new HttpClient();

            var html_bytes = await httpClient.GetByteArrayAsync(BaseUrl + "ubbthreads.php");
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
            var (name, title, description) = ParseBoardTitle(tr.Elements("td").ElementAtOrDefault(1));

            var threadCount = ConvertHlp.ToInt(tr.Elements("td").ElementAtOrDefault(2)?.Value?.Trim());
            var postCount = ConvertHlp.ToInt(tr.Elements("td").ElementAtOrDefault(3)?.Value?.Trim());
            var isFlashed = ParseIsFlashed(tr.Elements("td").ElementAtOrDefault(0));
            var lastPost = ParseLastPost(tr.Elements("td").ElementAtOrDefault(4));
            var moderators = ParseModerators(tr.Elements("td").ElementAtOrDefault(5));

            if (name == null)
                return null;

            return new Board(name, title, description, moderators: moderators, state:new BoardState(isFlashed, threadCount, postCount, lastPost));
        }
        static (string name, string title, string description) ParseBoardTitle(XElement titleTd)
        {
            var title = titleTd?.Element("font")?.Element("a")?.Value?.Trim();
            var name = title;
            var description = titleTd.Element("table")?.Elements("tr").ElementAtOrDefault(0)?.Elements("td").ElementAtOrDefault(1)?.Value?.Trim();
            return (name, title, description);
        }
        static bool ParseIsFlashed(XElement flashedTd)
        {
            var imgSrc = flashedTd?.Element("a")?.Element("img")?.Attribute("src")?.Value;
            return imgSrc switch
            {
                "/images/nonewposts.gif" => false,
                "/images/newposts.gif" => true,
                _ => false, //TODO Добавить logging
            };
        }
        static BoardStateLastPost ParseLastPost(XElement lastPostTd)
        {
            var time = lastPostTd.FirstNode.As<XText>()?.Value?.Trim();
            var a = lastPostTd.Element("a");
            if (a == null)
                return null;
            var postId = ParseQuery(a?.Attribute("href")?.Value)?.Find("Number")?.FirstOrDefault();
            var userName = a?.Value;
            if (userName?.StartsWith("by ") == true)
                userName = userName.Substring("by ".Length);
            if (userName?.StartsWith("от ") == true)
                userName = userName.Substring("от ".Length);
            return new BoardStateLastPost(postId, new User_Name(userName), DateTime.Parse(time, RuCulture));
        }
        static User_Name[] ParseModerators(XElement moderatorsTd)
        {
            return moderatorsTd.Elements("a")
                .Select(a => ParseQuery(a?.Attribute("href")?.Value).Find("User")?.FirstOrDefault())
                .Where(name => name != null)
                .Select(name => new User_Name(name))
                .ToArray();
        }
        static System.Text.Encoding Ru = System.Text.Encoding.GetEncoding(1251);
        static System.Globalization.CultureInfo RuCulture = System.Globalization.CultureInfo.GetCultureInfo("RU-ru");

        static Dictionary<string, string[]> ParseQuery([AllowNull] string url)
        {
            var questionIndex = url?.IndexOf('?');
            if (questionIndex >= 0)
                url = url.Substring(questionIndex.Value + 1);
            var sharpIndex = url?.IndexOf('#');
            if (sharpIndex >= 0)
                url = url.Substring(0, sharpIndex.Value);
            return (url?.Split('&')
                 .Select(pair => pair.Split('='))
                )
                .OrEmpty()
                .GroupBy(pair => pair.ElementAtOrDefault(0) ?? "", pair => UrlDecode(pair.ElementAtOrDefault(1).OrEmpty()))
                .ToDictionary(group => group.Key, group => group.ToArray());
        }
        static string UrlDecode(string value)//TODO символы-проценты складывать в byte[] и декодировать целиком
        {
            var result = new StringBuilder();
            var encoded = new StringBuilder();
            var isEncoded = false;

            foreach (var ch in value)
            {
                if (ch == '%')
                {
                    Decode(encoded);
                    isEncoded = true;
                }
                else if (isEncoded)
                {
                    encoded.Append(ch);
                }
                else
                    result.Append(ch);

                if (encoded.Length == 2)
                {
                    Decode(encoded);
                }

            }
            Decode(encoded);

            return result.ToString();

            void Decode(StringBuilder encoded)
            {
                if (encoded.Length != 0)
                {
                    var n = int.Parse(encoded.ToString(), System.Globalization.NumberStyles.HexNumber);
                    var s = Ru.GetString(new[] { (byte)n });
                    result.Append(s);
                }
                encoded.Clear();
                isEncoded = false;
            }
        }
    }
 
}
