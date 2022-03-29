using GraphQL.Builders;
using VirtoCommerce.Loyalty.Xapi.Schemas;
using VirtoCommerce.Platform.Core.Common;

namespace VirtoCommerce.Loyalty.Xapi.Extensions
{
    public static class ConnectionBuilderExtensions
    {
        public static ConnectionBuilder<object> Arguments(this ConnectionBuilder<object> connectionBuilder)
        {
            connectionBuilder.FieldType.Arguments = AbstractTypeFactory<OperationsQueryConnectionArguments>.TryCreateInstance().AddArguments(connectionBuilder.FieldType.Arguments);
            return connectionBuilder;
        }
    }
}
