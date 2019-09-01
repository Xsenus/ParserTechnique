using System;
using System.Collections.Generic;
using System.Linq;
using AngleSharp.Html.Dom;

namespace ParserTechnique.BL.Core.DNS
{
    public class DNSParser : IParser<string[]> 
    {
        public int GetPageCount(IHtmlDocument document)
        {
            var countPage = document.QuerySelectorAll("li")
                .Where(item => item.ClassName != null && item.ClassName.Contains("pagination-widget__page"))
                .Last().TextContent.Trim();

            return Convert.ToInt32(countPage);
        }

        public string[] Parse(IHtmlDocument document)
        {
            var list = new List<string>();

            var items = document.QuerySelectorAll("div")
                .Where(item => item.ClassName != null && item.ClassName.Contains("n-catalog-product__main"));

            foreach (var item in items)
            {
                var name = item.QuerySelectorAll("a")
                    .Where(w => w.ClassName != null && w.ClassName
                    .Contains("ui-link"))
                    .First().TextContent.Trim();

                var price = item.QuerySelectorAll("div")
                    .Where(w => w.ClassName != null && w.ClassName
                    .Contains("product-price__current"))
                    .First().TextContent.Trim();

                var url = item.QuerySelectorAll("a")
                    .OfType<IHtmlAnchorElement>()
                    .First().Href;

                list.Add($"{name}; Price: {price}; Url: {url}");
            }

            return list.ToArray();
        }
    }
}
