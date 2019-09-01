namespace ParserTechnique.BL.Core.Citilink
{
    public class CitilinkSettings : IParserSettings
    {
        public string BaseUrl { get; set; } = "https://www.citilink.ru/catalog/mobile/smartfony/?available=1";
        public string Prefix { get; set; } = "p={CurrentId}";
        public int StartPoint { get; set; } = 1;
        public int EndPoint { get; set; } = 1;
    }
}
