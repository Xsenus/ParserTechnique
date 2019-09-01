using AngleSharp.Html.Dom;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ParserTechnique.BL.Core.Citilink
{
    public class CitilinkParser : IParser<string[]>
    {
        public int GetPageCount(IHtmlDocument document)
        {
            var countPage = document.QuerySelectorAll("li")
                .Where(item => item.ClassName != null && item.ClassName.Contains("last"))
                .First().TextContent.Trim();

            return Convert.ToInt32(countPage);
        }

        public string[] Parse(IHtmlDocument document)
        {
            var list = new List<string>();              

            var items = document.QuerySelectorAll("div")
                .Where(item => item.ClassName != null && item.ClassName.Contains("js--subcategory-product-item subcategory-product-item product_data__gtm-js  product_data__pageevents-js ddl_product"));

            foreach (var item in items)
            { 
                var name = item.QuerySelectorAll("a")
                    .Where(w => w.ClassName != null && w.ClassName
                    .Contains("link_gtm-js link_pageevents-js ddl_product_link"))
                    .First().TextContent.Trim();

                var price = item.QuerySelectorAll("ins")
                    .Where(w => w.ClassName != null && w.ClassName
                    .Contains("subcategory-product-item__price-num"))
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
