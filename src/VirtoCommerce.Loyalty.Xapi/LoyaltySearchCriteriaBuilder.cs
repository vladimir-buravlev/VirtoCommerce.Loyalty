using AutoMapper;
using VirtoCommerce.Loyalty.Core.Models.Search;
using VirtoCommerce.Platform.Core.Common;
using VirtoCommerce.SearchModule.Core.Services;

namespace VirtoCommerce.Loyalty.Xapi
{
    public class LoyaltySearchCriteriaBuilder
    {
        private readonly ISearchPhraseParser _phraseParser;
        private readonly IMapper _mapper;
        private readonly LoyaltySearchCriteria _searchCriteria;

        public LoyaltySearchCriteriaBuilder(ISearchPhraseParser phraseParser, IMapper mapper)
        {
            _phraseParser = phraseParser;
            _mapper = mapper;
            _searchCriteria = AbstractTypeFactory<LoyaltySearchCriteria>.TryCreateInstance();
        }

        public virtual LoyaltySearchCriteria Build()
        {
            return _searchCriteria.Clone() as LoyaltySearchCriteria;
        }

        public LoyaltySearchCriteriaBuilder ParseFilters(string filterPhrase)
        {
            if (filterPhrase == null)
            {
                return this;
            }
            if (_phraseParser == null)
            {
                throw new OperationCanceledException("phrase parser must be set");
            }

            var parseResult = _phraseParser.Parse(filterPhrase);
            _mapper.Map(parseResult.Filters, _searchCriteria);
            _searchCriteria.Keyword = parseResult.Keyword;
            return this;
        }

        public LoyaltySearchCriteriaBuilder WithUserId(string userId)
        {
            _searchCriteria.UserId = userId ?? _searchCriteria.UserId;
            return this;
        }

        public LoyaltySearchCriteriaBuilder WithStoreId(string storeId)
        {
            _searchCriteria.StoreId = storeId ?? _searchCriteria.StoreId;
            return this;
        }

        public LoyaltySearchCriteriaBuilder WithPaging(int skip, int take)
        {
            _searchCriteria.Skip = skip;
            _searchCriteria.Take = take;
            return this;
        }

        public LoyaltySearchCriteriaBuilder WithSorting(string sort)
        {
            _searchCriteria.Sort = sort ?? _searchCriteria.Sort;

            return this;
        }
    }
}
