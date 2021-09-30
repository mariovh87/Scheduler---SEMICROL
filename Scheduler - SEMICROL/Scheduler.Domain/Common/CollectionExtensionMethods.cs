using System.Collections.Generic;
using System.Collections.ObjectModel;
using EnsureThat;
using Scheduler.Domain.Exceptions;

namespace Scheduler.Domain.Common
{
    public static class CollectionExtensionMethods
    {
        public static void HasItems<T>(this Queue<T> collection)
        {
            Ensure.Collection.HasItems(collection, nameof(collection), o => o.WithException(new CollectionEmptyException("Collection =>" + nameof(collection) + " is empty")));
        }
    }
}
