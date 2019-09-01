using AngleSharp.Html.Dom;

namespace ParserTechnique.BL.Core
{
    public interface IParser<T> where T : class
    {
        T Parse(IHtmlDocument document);

        int GetPageCount(IHtmlDocument document);
    }
}
