using AutoMapper;
using VirtoCommerce.ExperienceApiModule.Core.Infrastructure;
using VirtoCommerce.Loyalty.Core.Models.Search;
using VirtoCommerce.Loyalty.Core.Services;
using VirtoCommerce.SearchModule.Core.Services;

namespace VirtoCommerce.Loyalty.Xapi.Queries
{
    public class SearchOperationsQueryHandler : IQueryHandler<SearchOperationsQuery, LoyaltySearchResult>
    {
        private readonly IMapper _mapper;
        private readonly ISearchPhraseParser _searchPhraseParser;
        private readonly ILoyaltySearchService _loyaltySearchService;


        public SearchOperationsQueryHandler(IMapper mapper, ISearchPhraseParser searchPhraseParser, ILoyaltySearchService loyaltySearchService)
        {
            _mapper = mapper;
            _searchPhraseParser = searchPhraseParser;
            _loyaltySearchService = loyaltySearchService;
        }

        public Task<LoyaltySearchResult> Handle(SearchOperationsQuery request, CancellationToken cancellationToken)
        {
            var searchCriteria = new LoyaltySearchCriteriaBuilder(_searchPhraseParser, _mapper)
                                        .ParseFilters(request.Filter)
                                        .WithUserId(request.UserId)
                                        .WithStoreId(request.StoreId)
                                        .WithPaging(request.Skip, request.Take)
                                        .WithSorting(request.Sort)
                                        .Build();
            var searchResult = _loyaltySearchService.SearchOperationsAsync(searchCriteria);
            return searchResult;
        }
    }
}
