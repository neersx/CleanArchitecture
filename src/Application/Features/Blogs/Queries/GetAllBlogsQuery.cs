using AutoMapper;
using BlazorHero.CleanArchitecture.Application.Interfaces.Repositories;
using BlazorHero.CleanArchitecture.Domain.Entities.DreamWedds;
using BlazorHero.CleanArchitecture.Shared.Constants.Application;
using BlazorHero.CleanArchitecture.Shared.Wrapper;
using LazyCache;
using MediatR;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace BlazorHero.CleanArchitecture.Application.Features.Blogs.Queries
{
    public class GetAllBlogsQuery : IRequest<Result<List<GetAllBlogsResponse>>>
    {
        public GetAllBlogsQuery() { }
    }

    internal class GetAllBlogsCachedQueryHandler
        : IRequestHandler<GetAllBlogsQuery, Result<List<GetAllBlogsResponse>>>
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

        public async Task<Result<List<GetAllBlogsResponse>>> Handle(
            GetAllBlogsQuery request,
            CancellationToken cancellationToken
        )
        {
            Func<Task<List<Blog>>> getAllBlogs = () => _unitOfWork.Repository<Blog>().GetAllAsync();
            var list = await _cache.GetOrAddAsync(
                ApplicationConstants.Cache.GetAllBlogsCacheKey,
                getAllBlogs
            );
            var mappedBlogs = _mapper.Map<List<GetAllBlogsResponse>>(list);
            return await Result<List<GetAllBlogsResponse>>.SuccessAsync(mappedBlogs);
        }
    }

    public class GetAllBlogsResponse
    {
        public string BlogName { get; set; }
        public string Title { get; set; }
        public string BlogSubject { get; set; }
        public string Quote { get; set; }
        public string AuthorName { get; set; }
        public string Content { get; set; }
        public string ImageUrl { get; set; }
        public int BlogType { get; set; } = 0;
        public string SpecialNote { get; set; }
        public DateTime CreatedOn { get; set; }
    }
}
