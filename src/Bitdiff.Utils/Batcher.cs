using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace Bitdiff.Utils
{
    public class Batcher : IBatcher
    {
        public event EventHandler<BatchEventArgs> Complete = (s, e) => { };
        public event EventHandler<BatchCompleteEventArgs> SingleBatchComplete = (s, e) => { };

        private void OnComplete(BatchEventArgs e)
        {
            var handler = Complete;
            if (handler != null)
                handler(this, e);
        }
        
        private void OnSingleBatchComplete(BatchCompleteEventArgs e)
        {
            var handler = SingleBatchComplete;
            if (handler != null)
                handler(this, e);
        }

        public void Batch<T>(IEnumerable<T> items, int batchSize, Action<IEnumerable<T>> action)
        {
            var itemsToProcess = new List<T>();
            int index = 1;
            int totalItemsProcessed = 0;

            foreach (var item in items)
            {
                itemsToProcess.Add(item);
                if (index % batchSize == 0)
                    Process(itemsToProcess, action, ref totalItemsProcessed);
                index++;
            }

            if (itemsToProcess.Any())
                Process(itemsToProcess, action, ref totalItemsProcessed);

            OnComplete(new BatchEventArgs(totalItemsProcessed));
        }

        private void Process<T>(ICollection<T> itemsToProcess, Action<IEnumerable<T>> action, ref int totalItemsProcessed)
        {
            Stopwatch stopwatch = Stopwatch.StartNew();
            action(itemsToProcess);
            stopwatch.Stop();

            totalItemsProcessed += itemsToProcess.Count;
            OnSingleBatchComplete(new BatchCompleteEventArgs(itemsToProcess.Count, stopwatch.Elapsed, totalItemsProcessed));

            itemsToProcess.Clear();
        }
    }
}