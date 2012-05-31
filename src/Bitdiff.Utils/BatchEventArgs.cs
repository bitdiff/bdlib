using System;

namespace Bitdiff.Utils
{
    public class BatchEventArgs : EventArgs
    {
        private readonly int _numberOfItemsProcessed;

        public BatchEventArgs(int numberOfItemsProcessed)
        {
            _numberOfItemsProcessed = numberOfItemsProcessed;
        }

        public int NumberOfItemsProcessed
        {
            get { return _numberOfItemsProcessed; }
        }
    }
}