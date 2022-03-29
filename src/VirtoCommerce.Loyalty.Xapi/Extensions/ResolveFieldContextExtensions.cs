using GraphQL;
using VirtoCommerce.ExperienceApiModule.Core.Infrastructure;
using VirtoCommerce.Platform.Core.Common;

namespace VirtoCommerce.Loyalty.Xapi.Extensions
{
    public static class ResolveFieldContextExtensions
    {
        public static T ExtractQuery<T>(this IResolveFieldContext context) where T : IExtendableQuery
        {
            var query = AbstractTypeFactory<T>.TryCreateInstance();
            query.Map(context);
            return query;
        }
    }
}



