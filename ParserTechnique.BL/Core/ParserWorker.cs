using AngleSharp.Html.Parser;
using ParserTechnique.BL.Core.Citilink;
using ParserTechnique.BL.Core.DNS;
using System;

namespace ParserTechnique.BL.Core
{
    public class ParserWorker<T> where T : class
    {
        IParser<T> parser;
        IParserSettings parserSettings;

        DNSHtmlLoader citilinkHtmlLoader;

        bool isActive;

        #region Properties

        public IParser<T> Parser
        {
            get
            {
                return parser;
            }
            set
            {
                parser = value;
            }
        }

        public IParserSettings ParserSettings
        {
            get
            {
                return parserSettings;
            }
            set
            {
                parserSettings = value;
                citilinkHtmlLoader = new DNSHtmlLoader(value);
            }
        }

        public bool IsActive
        {
            get
            {
                return isActive;
            }
        }
        #endregion

        public event Action<object, T> OnNewData;
        public event Action<object> OnCompleated;

        public ParserWorker(IParser<T> parser)
        {
            this.parser = parser;
        }

        public ParserWorker(IParser<T> parser, IParserSettings parserSettings) : this(parser)
        {
            this.parserSettings = parserSettings;
        }

        public void Start()
        {
            isActive = true;
            Worker();
        }

        public void Abort()
        {
            isActive = false;
        }

        private async void Worker()
        {
            for (int i = parserSettings.StartPoint; i <= parserSettings.EndPoint; i++)
            {
                if (!isActive)
                {
                    OnCompleated?.Invoke(this);
                    return;
                }

                var source = await citilinkHtmlLoader.GetSourceByPageId(i);
                var domParser = new HtmlParser();
                var document = await domParser.ParseDocumentAsync(source);

                parserSettings.EndPoint = parser.GetPageCount(document);
                var result = parser.Parse(document);

                OnNewData?.Invoke(this, result);
            }

            OnCompleated?.Invoke(this);
            isActive = false;
        }
    }
}
