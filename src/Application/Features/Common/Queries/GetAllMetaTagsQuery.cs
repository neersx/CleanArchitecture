using AutoMapper;
using BlazorHero.CleanArchitecture.Application.Interfaces.Repositories;
using LazyCache;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using BlazorHero.CleanArchitecture.Shared.Wrapper;
using System;
using BlazorHero.CleanArchitecture.Domain.Entities.DreamWedds;
using BlazorHero.CleanArchitecture.Shared.Constants.Application;
using System.Collections.Generic;

namespace BlazorHero.CleanArchitecture.Application.Features.Common.Queries
{
    public class GetAllMetaTagsQuery : IRequest<Result<List<GetAllMetaTagsResponse>>>
    {
        public GetAllMetaTagsQuery() { }
    }

    internal class GetAllBlogsCachedQueryHandler
        : IRequestHandler<GetAllMetaTagsQuery, Result<List<GetAllMetaTagsResponse>>>
    {
        private readonly IUnitOfWork<int> _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IAppCache _cache;

        public GetAllBlogsCachedQueryHandler(
            IUnitOfWork<int> unitOfWork,
            IMapper mapper,
            IAppCache cache
        )
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _cache = cache;
        }

        public async Task<Result<List<GetAllMetaTagsResponse>>> Handle(
            GetAllMetaTagsQuery request,
            CancellationToken cancellationToken
        )
        {
            Func<Task<List<MetaTags>>> getAllMetaTags = () => _unitOfWork.Repository<MetaTags>().GetAllAsync();
            var list = await _cache.GetOrAddAsync(
                ApplicationConstants.Cache.GetAllMetaTagsCacheKey,
                getAllMetaTags
            );
            var mappedTags = _mapper.Map<List<GetAllMetaTagsResponse>>(list);
            return await Result<List<GetAllMetaTagsResponse>>.SuccessAsync(mappedTags);
        }
    }

}
