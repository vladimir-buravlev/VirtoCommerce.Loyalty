using System.Collections.Generic;
using VirtoCommerce.Loyalty.Core.Models;
using VirtoCommerce.Platform.Core.Events;

namespace VirtoCommerce.Loyalty.Core.Events
{
    public class OperationChangeEvent : GenericChangedEntryEvent<PointsOperation>
    {
        public OperationChangeEvent(IEnumerable<GenericChangedEntry<PointsOperation>> changedEntries)
           : base(changedEntries)
        {
        }
    }
}
