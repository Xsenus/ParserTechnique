using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

namespace ParserTechnique.BL.Core.DNS
{
    class DNSHtmlLoader
    {
        readonly HttpClient client;
        readonly string url;

        public DNSHtmlLoader(IParserSettings settings)
        {
            client = new HttpClient();
            url = $"{settings.BaseUrl}?{settings.Prefix}";
        }

        public async Task<string> GetSourceByPageId(int id)
        {
            var currentUrl = url.Replace("{CurrentId}", id.ToString());
            var respounse = await client.GetAsync(currentUrl);
            string source = null;

            if (respounse != null && respounse.StatusCode == HttpStatusCode.OK)
            {
                source = await respounse.Content.ReadAsStringAsync();
            }

            return source;
        }
    }
}
