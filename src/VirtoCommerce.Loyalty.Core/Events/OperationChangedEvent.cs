using System.Collections.Generic;
using System.Text.Json.Serialization;
using VirtoCommerce.Loyalty.Core.Models;
using VirtoCommerce.Platform.Core.Events;

namespace VirtoCommerce.Loyalty.Core.Events
{
    public class OperationChangedEvent : GenericChangedEntryEvent<PointsOperation>
    {
        [JsonConstructor]
        public OperationChangedEvent(IEnumerable<GenericChangedEntry<PointsOperation>> changedEntries)
            : base(changedEntries)
        {
        }

    }
}
