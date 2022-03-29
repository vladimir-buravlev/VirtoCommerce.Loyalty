using GraphQL.Types;
using VirtoCommerce.ExperienceApiModule.Core.Services;
using VirtoCommerce.ExperienceApiModule.XOrder.Schemas;
using VirtoCommerce.Loyalty.Core.Models;

namespace VirtoCommerce.Loyalty.Xapi.Schemas
{
    public class LoyaltedOrderType : CustomerOrderType
    {
        public LoyaltedOrderType(IDynamicPropertyResolverService dynamicPropertyResolverService)
            : base(dynamicPropertyResolverService)
        {
            Field<BooleanGraphType>("loyaltyCalculated", resolve: context => (context.Source.Order as LoyaltedOrder).LoyaltyCalculated);
        }
    }
}
