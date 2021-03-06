﻿using System;
using System.Collections.Generic;
using System.Linq;

namespace Bitdiff.Utils
{
    public static class EnumerableExtensions
    {
        public static void Batch<T>(this IEnumerable<T> items, int batchSize, Action<IEnumerable<T>, int> action)
        {
            new Batcher().Batch(items, batchSize, action);
        }

        public static PaginatedList<TResult> SelectWithPagination<TSource, TResult>
            (this PaginatedList<TSource> source, Func<TSource, TResult> selector)
        {
            return new PaginatedList<TResult>(source.Select(selector), source.Paginator);
        }

        public static void Batch<T>(this IEnumerable<T> items, int batchSize, Action<IEnumerable<T>, int> action, Action<object, BatchCompleteEventArgs> singleBatchCompleteEventHandler, Action<object, BatchEventArgs> completeEventHandler)
        {
            var batcher = new Batcher();

            if (singleBatchCompleteEventHandler != null)
                batcher.SingleBatchComplete += new EventHandler<BatchCompleteEventArgs>(singleBatchCompleteEventHandler);

            if (completeEventHandler != null)
                batcher.Complete += new EventHandler<BatchEventArgs>(completeEventHandler);

            batcher.Batch(items, batchSize, action);
        }

        public static bool HasAny<T>(this IEnumerable<T> items)
        {
            return items != null && items.Any();
        }

        public static bool DoesNotHaveAny<T>(this IEnumerable<T> items)
        {
            return !items.HasAny();
        }
    }
}