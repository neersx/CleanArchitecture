using AutoMapper;
using BlazorHero.CleanArchitecture.Application.Interfaces.Repositories;
using LazyCache;
using MediatR;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using BlazorHero.CleanArchitecture.Shared.Wrapper;
using BlazorHero.CleanArchitecture.Domain.Entities.DreamWedds;
using BlazorHero.CleanArchitecture.Shared.Constants.Application;
using System.Threading;

namespace BlazorHero.CleanArchitecture.Application.Features.Faqs.Queries
{
    public class GetAllFaqsQuery : IRequest<Result<List<GetAllFaqsListResponse>>>
    {
        public GetAllFaqsQuery() { }
    }

    internal class GetAllFaqsCachedQueryHandler
        : IRequestHandler<GetAllFaqsQuery, Result<List<GetAllFaqsListResponse>>>
    {
        private readonly IUnitOfWork<int> _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IAppCache _cache;

        public GetAllFaqsCachedQueryHandler(
            IUnitOfWork<int> unitOfWork,
            IMapper mapper,
            IAppCache cache
        )
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _cache = cache;
        }

        public async Task<Result<List<GetAllFaqsListResponse>>> Handle(
            GetAllFaqsQuery request,
            CancellationToken cancellationToken
        )
        {
            Func<Task<List<Faq>>> getAllFaqs = () => _unitOfWork.Repository<Faq>().GetAllAsync();
            var list = await _cache.GetOrAddAsync(
                ApplicationConstants.Cache.GetAllFaqCacheKey,
                getAllFaqs
            );
            var mappedFaqs = _mapper.Map<List<GetAllFaqsListResponse>>(list);
            return await Result<List<GetAllFaqsListResponse>>.SuccessAsync(mappedFaqs);
        }
    }
    public class GetAllFaqsListResponse
    {
        public int Id { get; set; }
        [MaxLength(500)]
        [Required]
        public string Question { get; set; }
        [Required]
        public string Answer { get; set; }
        [MaxLength(200)]
        public string Website { get; set; }
        public bool IsMainQue { get; set; } = false;
        public int? ParentQuestionId { get; set; }
        public int? Sequence { get; set; }
        public DateTime CreatedOn { get; set; }
        public string LastModifiedBy { get; set; }
        public DateTime? LastModifiedOn { get; set; }
    }
}
