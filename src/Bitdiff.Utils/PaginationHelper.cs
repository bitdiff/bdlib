using System;
using System.Collections.Generic;
using System.Linq;

namespace Bitdiff.Utils
{
    public class PaginationHelper
    {
        private readonly PaginationDefaults _paginationDefaults;

        public int RecordsPerPage { get; private set; }
        public long TotalRecords { get; private set; }
        public int CurrentPageNumber { get; private set; }

        public PaginationHelper(long totalRecords, int currentPage)
            : this(totalRecords, currentPage, PaginationDefaults.DefaultRecordsPerPage) { }

        public PaginationHelper(long totalRecords, int currentPage, int recordsPerPage)
            : this(totalRecords, currentPage, recordsPerPage, new PaginationDefaults(PaginationDefaults.DefaultPagesToShow, PaginationDefaults.DefaultNumberOfPagesLeftOfCurrent, PaginationDefaults.DefaultNumberOfPagesRightOfCurrent)) { }

        public PaginationHelper(long totalRecords, int currentPage, int recordsPerPage, PaginationDefaults paginationDefaults)
        {
            TotalRecords = totalRecords;
            RecordsPerPage = recordsPerPage;
            _paginationDefaults = paginationDefaults;

            SetCurrentPage(currentPage);
        }

        // Default url builder returns the page number as the link.
        private Func<int, string> _linkBuilder = page => page.ToString();

        // Default url text returns the page number as the text.
        private Func<int, string> _linkTextBuilder = page => page.ToString();

        public PaginationHelper Link(Func<int, string> linkBuilder)
        {
            _linkBuilder = linkBuilder;
            return this;
        }

        private void SetCurrentPage(int pageNumber)
        {
            if (pageNumber < 1)
                pageNumber = 1;

            if (pageNumber > LastPageNumber && pageNumber != FirstPageNumber)
                pageNumber = LastPageNumber;

            CurrentPageNumber = pageNumber;
        }

        public PaginationHelper LinkText(Func<int, string> linkTextBuilder)
        {
            _linkTextBuilder = linkTextBuilder;
            return this;
        }

        public string GetLink(int pageNumber)
        {
            return _linkBuilder(pageNumber);
        }

        public string GetLinkText(int pageNumber)
        {
            return _linkTextBuilder(pageNumber);
        }

        public bool HasNextPage
        {
            get { return CurrentPageNumber < TotalNumberOfPages; }
        }

        public bool HasPreviousPage
        {
            get { return CurrentPageNumber > 1; }
        }

        public int PreviousPageNumber
        {
            get { return CurrentPageNumber - 1; }
        }

        public VisiblePage PreviousVisiblePage
        {
            get { return GetPageToShow(PreviousPageNumber); }
        }

        private VisiblePage GetPageToShow(int pageNumber)
        {
            return new VisiblePage(pageNumber, _linkBuilder(pageNumber),
                                   _linkTextBuilder(pageNumber), pageNumber == CurrentPageNumber);
        }

        public int NextPageNumber
        {
            get { return CurrentPageNumber + 1; }
        }

        public VisiblePage NextVisiblePage
        {
            get { return GetPageToShow(NextPageNumber); }
        }

        public VisiblePage FirstVisiblePage
        {
            get { return GetPageToShow(FirstPageNumber); }
        }

        public VisiblePage LastVisiblePage
        {
            get { return GetPageToShow(LastPageNumber); }
        }

        public int FirstPageNumber
        {
            get { return 1; }
        }

        public int LastPageNumber
        {
            get { return TotalNumberOfPages; }
        }

        public int TotalNumberOfPages
        {
            get { return (int)Math.Ceiling(((double)TotalRecords) / RecordsPerPage); }
        }

        public List<VisiblePage> PagesToShow
        {
            get
            {
                int numberOfPages = TotalNumberOfPages;

                if (TotalNumberOfPages == 1)
                {
                    return new List<VisiblePage>();
                }

                if (numberOfPages <= _paginationDefaults.NumberOfPagesToShow)
                {
                    return BuildPageList(1, numberOfPages);
                }

                if (CurrentPageNumber <= _paginationDefaults.NumberOfPagesLeftOfCurrent)
                {
                    return BuildFirstPage();
                }

                if (CurrentPageNumber + _paginationDefaults.NumberOfPagesRightOfCurrent > numberOfPages)
                {
                    return BuildLastPage(numberOfPages);
                }

                return BuildPageList(CurrentPageNumber - _paginationDefaults.NumberOfPagesLeftOfCurrent,
                                     CurrentPageNumber + _paginationDefaults.NumberOfPagesRightOfCurrent);
            }
        }

        public bool HasFirstPage
        {
            get { return !HasPageNumber(1) && PagesToShow.Count > 0; }
        }

        public bool HasLastPage
        {
            get { return !HasPageNumber(TotalNumberOfPages) && PagesToShow.Count > 0; }
        }

        private List<VisiblePage> BuildLastPage(int numberOfPages)
        {
            return BuildPageList((numberOfPages - _paginationDefaults.NumberOfPagesToShow) + 1, numberOfPages);
        }

        private List<VisiblePage> BuildFirstPage()
        {
            return BuildPageList(1, _paginationDefaults.NumberOfPagesToShow);
        }

        private bool HasPageNumber(int pageNumber)
        {
            return PagesToShow.Exists(pageToShow => pageToShow.PageNumber == pageNumber);
        }

        private List<VisiblePage> BuildPageList(int startPage, int endPage)
        {
            var result = new List<VisiblePage>();

            for (int i = startPage; i <= endPage; i++)
            {
                result.Add(GetPageToShow(i));
            }
            return result;
        }

        public static int CalculateSkip(int currentPageNumber, int recordsPerPage)
        {
            return (currentPageNumber - 1) * recordsPerPage;
        }

        public int NumberOfRecordsFromBeginningToSkip
        {
            get { return CalculateSkip(CurrentPageNumber, RecordsPerPage); }
        }

        public long FirstVisibleResult
        {
            get { return NumberOfRecordsFromBeginningToSkip + 1; }
        }

        public long LastVisibleResult
        {
            get
            {
                if (HasNextPage)
                    return CurrentPageNumber * RecordsPerPage;

                return TotalRecords;
            }
        }

        public IEnumerable<T> PopulateAsNecessary<T>(IEnumerable<T> items)
        {
            return items.Skip(NumberOfRecordsFromBeginningToSkip).Take(RecordsPerPage);
        }
    }
}