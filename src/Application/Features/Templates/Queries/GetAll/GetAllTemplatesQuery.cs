using AutoMapper;
using BlazorHero.CleanArchitecture.Application.Interfaces.Repositories;
using LazyCache;
using MediatR;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using BlazorHero.CleanArchitecture.Shared.Wrapper;
using BlazorHero.CleanArchitecture.Domain.Entities.DreamWedds;
using BlazorHero.CleanArchitecture.Shared.Constants.Application;
using System.Threading;
using DreamWeddsManager.Application.Enums;

namespace BlazorHero.CleanArchitecture.Application.Features.Templates.Queries
{

    public class GetAllTemplatesQuery : IRequest<Result<List<GetAllTemplatesResponse>>>
    {
        public GetAllTemplatesQuery()
        {
        }
    }

    internal class GetAllTemplatesCachedQueryHandler : IRequestHandler<GetAllTemplatesQuery, Result<List<GetAllTemplatesResponse>>>
    {
        private readonly IUnitOfWork<int> _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IAppCache _cache;

        public GetAllTemplatesCachedQueryHandler(IUnitOfWork<int> unitOfWork, IMapper mapper, IAppCache cache)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _cache = cache;
        }

        public async Task<Result<List<GetAllTemplatesResponse>>> Handle(GetAllTemplatesQuery request, CancellationToken cancellationToken)
        {
            Func<Task<List<TemplateMaster>>> getAllTemplates = () => _unitOfWork.Repository<TemplateMaster>().GetAllAsync();
            var list = await _cache.GetOrAddAsync(ApplicationConstants.Cache.GetAllTemplatesCacheKey, getAllTemplates);
            var mappedTemplates = _mapper.Map<List<GetAllTemplatesResponse>>(list.Where(x => x.Type == (int)TemplateType.Wedding));
            return await Result<List<GetAllTemplatesResponse>>.SuccessAsync(mappedTemplates);
        }
    }

    public class GetAllTemplatesResponse
    {
        [MaxLength(250)]
        public string Name { get; set; }
        public int Type { get; set; }
        public int Status { get; set; }
        [MaxLength(2500)]
        public string Content { get; set; }
        [MaxLength(250)]
        public string Subject { get; set; }
        [MaxLength(250)]
        public string Tags { get; set; }
        [MaxLength(500)]
        public string TemplateUrl { get; set; }
        [MaxLength(500)]
        public string ThumbnailImageUrl { get; set; }
        [MaxLength(250)]
        public string TagLine { get; set; }
        public string AboutTemplate { get; set; }   
    }
}
