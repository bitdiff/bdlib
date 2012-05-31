namespace Bitdiff.Utils
{
    public class PaginationDefaults
    {
        public PaginationDefaults(int numberOfPagesToShow, int numberOfPagesLeftOfCurrent, int numberOfPagesRightOfCurrent)
        {
            NumberOfPagesToShow = numberOfPagesToShow;
            NumberOfPagesLeftOfCurrent = numberOfPagesLeftOfCurrent;
            NumberOfPagesRightOfCurrent = numberOfPagesRightOfCurrent;
        }

        public int NumberOfPagesRightOfCurrent { get; private set; }
        public int NumberOfPagesLeftOfCurrent { get; private set; }
        public int NumberOfPagesToShow { get; private set; }

        public static int GetPage(int? currentPageNumber)
        {
            return currentPageNumber ?? 1;
        }

        public static int DefaultPagesToShow { get { return 11; } }
        public static int DefaultNumberOfPagesLeftOfCurrent { get { return 5; } }
        public static int DefaultNumberOfPagesRightOfCurrent { get { return 5; } }
        public static int DefaultRecordsPerPage { get { return 10; } }
    }
}
