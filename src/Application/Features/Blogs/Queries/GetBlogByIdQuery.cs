using AutoMapper;
using BlazorHero.CleanArchitecture.Application.Interfaces.Repositories;
using MediatR;
using LazyCache;
using MediatR;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using BlazorHero.CleanArchitecture.Shared.Wrapper;
using BlazorHero.CleanArchitecture.Domain.Entities.Catalog;

namespace BlazorHero.CleanArchitecture.Application.Features.Blogs.Queries
{

    public class GetBlogByIdQuery : IRequest<Result<GetBlogByIdResponse>>
    {
        public int Id { get; set; }
    }

    internal class GetBlogByIdQueryQueryHandler : IRequestHandler<GetBlogByIdQuery, Result<GetBlogByIdResponse>>
    {
        private readonly IUnitOfWork<int> _unitOfWork;
        private readonly IMapper _mapper;

        public GetBlogByIdQueryQueryHandler(IUnitOfWork<int> unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Result<GetBlogByIdResponse>> Handle(GetBlogByIdQuery query, CancellationToken cancellationToken)
        {
            var brand = await _unitOfWork.Repository<Brand>().GetByIdAsync(query.Id);
            var mappedBrand = _mapper.Map<GetBlogByIdResponse>(brand);
            return await Result<GetBlogByIdResponse>.SuccessAsync(mappedBrand);
        }
    }
}
