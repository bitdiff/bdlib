namespace Bitdiff.Utils
{
    public class VisiblePage
    {
        public VisiblePage(int pageNumber, string link, string linkText, bool isCurrentPage)
        {
            PageNumber = pageNumber;
            Link = link;
            LinkText = linkText;
            IsCurrentPage = isCurrentPage;
        }

        public string Link { get; private set; }
        public int PageNumber { get; private set; }
        public string LinkText { get; private set; }
        public bool IsCurrentPage { get; private set; }
    }
}