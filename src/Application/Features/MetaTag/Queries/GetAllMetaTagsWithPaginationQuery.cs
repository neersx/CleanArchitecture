using AutoMapper;
using AutoMapper.QueryableExtensions;
using BlazorHero.CleanArchitecture.Application.Interfaces.Repositories;
using BlazorHero.CleanArchitecture.Application.Interfaces.Services;
using MediatR;
using System.Threading.Tasks;
using BlazorHero.CleanArchitecture.Domain.Entities.DreamWedds;
using System.Threading;
using BlazorHero.CleanArchitecture.Application.Application.Common.Models;
using System.Linq;
using BlazorHero.CleanArchitecture.Application.Common.Mappings;
using BlazorHero.CleanArchitecture.Application.Specifications.Base;

namespace BlazorHero.CleanArchitecture.Application.Features.MetaTag.Queries
{
    public class AllMetaTagsWithPaginationQuery : PaginationRequest, IRequest<PaginatedData<MetaTagsPaginationResponse>>
    {
        public AllMetaTagsWithPaginationQuery() { }
    }

    public class AllMetaTagsQueryHandler : IRequestHandler<AllMetaTagsWithPaginationQuery, PaginatedData<MetaTagsPaginationResponse>>
    {
        private readonly ICurrentUserService _currentUserService;
        private readonly IUnitOfWork<int> _unitOfWork;
        private readonly IMapper _mapper;

        public AllMetaTagsQueryHandler(
            ICurrentUserService currentUserService,
           IUnitOfWork<int> unitOfWork,
        IMapper mapper
            )
        {
            _currentUserService = currentUserService;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<PaginatedData<MetaTagsPaginationResponse>> Handle(AllMetaTagsWithPaginationQuery request, CancellationToken cancellationToken)
        {
            //var filters = PredicateBuilder.FromFilter<Document>(request.FilterRules);

            var data = await _unitOfWork.Repository<MetaTags>().Entities.Where(x => !x.IsDeleted)
                .ProjectTo<MetaTagsPaginationResponse>(_mapper.ConfigurationProvider)
                .PaginatedDataAsync(request.Page, request.Rows);

            return data;
        }

        internal class DocumentsQuery : HeroSpecification<MetaTags>
        {
            public DocumentsQuery(string userId)
            {
                this.AddInclude(x => x.Blog);
                this.Criteria = p => (p.CreatedBy == userId && p.IsImage == true);
            }
        }

    }

    public class MetaTagsPaginationResponse : IMapFrom<MetaTags>
    {
        public void Mapping(Profile profile)
        {
            profile.CreateMap<MetaTags, MetaTagsPaginationResponse>();
            profile.CreateMap<MetaTagsPaginationResponse, MetaTags>(MemberList.None);
        }
        public string Name { get; set; }
        public string Property { get; set; }
        public string TagPrefix { get; set; } // fb, og, twitter
        public string Content { get; set; }
        public string PageName { get; set; }
        public string PageTitle { get; set; }
        public bool IsImage { get; set; }

    }
}
