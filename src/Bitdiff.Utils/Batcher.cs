using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace Bitdiff.Utils
{
    public class Batcher : IBatcher
    {
        private int _batchNumber;
        public event EventHandler<BatchEventArgs> Complete = (s, e) => { };
        public event EventHandler<BatchCompleteEventArgs> SingleBatchComplete = (s, e) => { };

        private void OnComplete(BatchEventArgs e)
        {
            var handler = Complete;
            handler?.Invoke(this, e);
        }

        private void OnSingleBatchComplete(BatchCompleteEventArgs e)
        {
            var handler = SingleBatchComplete;
            handler?.Invoke(this, e);
        }

        public void Batch<T>(IEnumerable<T> items, int batchSize, Action<IEnumerable<T>, int> action)
        {
            var itemsToProcess = new List<T>();
            var index = 1;

            var totalItemsProcessed = 0;

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

        public async Task AsyncBatch<T>(IEnumerable<T> items, int batchSize, Func<IEnumerable<T>, int, Task> action)
        {
            var itemsToProcess = new List<T>();
            var index = 1;

            var totalItemsProcessed = 0;

            foreach (var item in items)
            {
                itemsToProcess.Add(item);
                if (index % batchSize == 0)
                    totalItemsProcessed = await ProcessAsync(itemsToProcess, action, totalItemsProcessed);
                index++;
            }

            if (itemsToProcess.Any())
                totalItemsProcessed = await ProcessAsync(itemsToProcess, action, totalItemsProcessed);

            OnComplete(new BatchEventArgs(totalItemsProcessed));
        }

        private void Process<T>(ICollection<T> itemsToProcess, Action<IEnumerable<T>, int> action, ref int totalItemsProcessed)
        {
            var stopwatch = Stopwatch.StartNew();
            action(itemsToProcess, ++_batchNumber);
            stopwatch.Stop();

            totalItemsProcessed += itemsToProcess.Count;
            OnSingleBatchComplete(new BatchCompleteEventArgs(itemsToProcess.Count, stopwatch.Elapsed, totalItemsProcessed));

            itemsToProcess.Clear();
        }

        private async Task<int> ProcessAsync<T>(ICollection<T> itemsToProcess, Func<IEnumerable<T>, int, Task> action, int totalItemsProcessed)
        {
            var stopwatch = Stopwatch.StartNew();
            await action(itemsToProcess, ++_batchNumber);
            stopwatch.Stop();

            totalItemsProcessed += itemsToProcess.Count;
            OnSingleBatchComplete(new BatchCompleteEventArgs(itemsToProcess.Count, stopwatch.Elapsed, totalItemsProcessed));

            itemsToProcess.Clear();

            return totalItemsProcessed;
        }
    }
}