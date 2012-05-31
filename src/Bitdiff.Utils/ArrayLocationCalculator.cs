using System;

namespace Bitdiff.Utils
{
    public class ArrayLocationCalculator
    {
        public ArrayLocation Calculate(int totalRecords, int pageNumber, int recordsPerPage)
        {
            ArrayLocation location = new ArrayLocation();

            if (totalRecords < 1 || pageNumber < 1 || recordsPerPage < 1)
            {
                location.Start = 0;
                location.End = 0;

                return location;
            }

            int start = 1 + (pageNumber - 1) * recordsPerPage;
            int end = Math.Min(pageNumber * recordsPerPage, totalRecords);
            if (start > end)
            {
                start = Math.Max(1, end - recordsPerPage);
            }

            location.Start = start;
            location.End = end;
            return location;
        }
    }
}