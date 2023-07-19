using AutoMapper;
using BlazorHero.CleanArchitecture.Application.Features.Common.Queries;
using BlazorHero.CleanArchitecture.Application.Interfaces.Repositories;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Threading;
using BlazorHero.CleanArchitecture.Shared.Wrapper;
using BlazorHero.CleanArchitecture.Domain.Entities.DreamWedds;

namespace BlazorHero.CleanArchitecture.Application.Features.Blogs.Queries
{
    public class GetBlogByNameQuery : IRequest<Result<GetBlogByIdResponse>>
    {
        public string Name { get; set; }
        public GetBlogByNameQuery(string name)
        {
            Name = name;
        }
    }

    internal class GetBlogByNameQueryHandler : IRequestHandler<GetBlogByNameQuery, Result<GetBlogByIdResponse>>
    {
        private readonly IUnitOfWork<int> _unitOfWork;
        private readonly IMapper _mapper;

        public GetBlogByNameQueryHandler(IUnitOfWork<int> unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Result<GetBlogByIdResponse>> Handle(GetBlogByNameQuery query, CancellationToken cancellationToken)
        {
            var blog = await _unitOfWork.Repository<Blog>().Entities.Include(x => x.MetaTags).Include(x => x.Comments).FirstAsync(name => name.BlogName == query.Name);
            var mappedBlog = _mapper.Map<GetBlogByIdResponse>(blog);
            return await Result<GetBlogByIdResponse>.SuccessAsync(mappedBlog);
        }
    }

    public class GetBlogByIdResponse
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
        public ICollection<GetAllMetaTagsResponse> MetaTags { get; set; }
    }

}
