using System;

namespace Bitdiff.Utils
{
    public class BatchCompleteEventArgs : EventArgs
    {
        private readonly int _numberOfItemsProcessed;
        private readonly TimeSpan _elapsed;
        private readonly int _totalItemsProcessed;

        public BatchCompleteEventArgs(int numberOfItemsProcessed, TimeSpan elapsed, int totalItemsProcessed)
        {
            _numberOfItemsProcessed = numberOfItemsProcessed;
            _elapsed = elapsed;
            _totalItemsProcessed = totalItemsProcessed;
        }

        public int TotalItemsProcessed
        {
            get { return _totalItemsProcessed; }
        }

        public TimeSpan Elapsed
        {
            get { return _elapsed; }
        }

        public int NumberOfItemsProcessed
        {
            get { return _numberOfItemsProcessed; }
        }
    }
}