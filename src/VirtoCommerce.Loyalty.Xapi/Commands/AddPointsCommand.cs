using VirtoCommerce.ExperienceApiModule.Core.Infrastructure;

namespace VirtoCommerce.Loyalty.Xapi.Commands
{
    public class AddPointsCommand : ICommand<decimal>
    {
        public AddPointsCommand(string userId, string storeId, string reason, decimal amount/*, bool isDeposit*/)
        {
            UserId = userId;
            StoreId = storeId;
            Reason = reason;
            Amount = amount;
            //IsDeposit = isDeposit;
        }

        public string UserId { get; set; }
        public string StoreId { get; set; }
        public string Reason { get; set; }
        public decimal Amount { get; set; }
        //public bool IsDeposit { get; set; }
    }
}
