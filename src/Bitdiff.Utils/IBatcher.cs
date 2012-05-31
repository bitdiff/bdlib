using System;
using System.Collections.Generic;

namespace Bitdiff.Utils
{
    public interface IBatcher
    {
        void Batch<T>(IEnumerable<T> items, int batchSize, Action<IEnumerable<T>> action);
        event EventHandler<BatchEventArgs> Complete;
        event EventHandler<BatchCompleteEventArgs> SingleBatchComplete;
    }
}