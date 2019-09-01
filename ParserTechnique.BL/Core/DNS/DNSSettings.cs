namespace ParserTechnique.BL.Core.DNS
{
    public class DNSSettings : IParserSettings
    {
        public string BaseUrl { get; set; } = "https://www.dns-shop.ru/catalog/17a8a01d16404e77/smartfony/";
        public string Prefix { get; set; } = "p={CurrentId}";
        public int StartPoint { get; set; } = 1;
        public int EndPoint { get; set; } = 1;
    }
}
