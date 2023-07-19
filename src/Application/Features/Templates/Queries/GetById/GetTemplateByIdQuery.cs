using AutoMapper;
using BlazorHero.CleanArchitecture.Application.Interfaces.Repositories;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using BlazorHero.CleanArchitecture.Shared.Wrapper;
using BlazorHero.CleanArchitecture.Domain.Entities.DreamWedds;
using BlazorHero.CleanArchitecture.Shared.Constants.Application;
using System.Threading;
using BlazorHero.CleanArchitecture.Application.Enums;
using System.Collections.Generic;

namespace BlazorHero.CleanArchitecture.Application.Features.Templates.Queries.GetById
{
    public class GetTemplateByIdQuery : IRequest<Result<GetTemplateByIdResponse>>
    {
        public int Id { get; set; }
        public GetTemplateByIdQuery(int id)
        {
            Id = id;
        }
    }

    internal class GetTemplateByIdQueryHandler : IRequestHandler<GetTemplateByIdQuery, Result<GetTemplateByIdResponse>>
    {
        private readonly IUnitOfWork<int> _unitOfWork;
        private readonly IMapper _mapper;

        public GetTemplateByIdQueryHandler(IUnitOfWork<int> unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Result<GetTemplateByIdResponse>> Handle(GetTemplateByIdQuery query, CancellationToken cancellationToken)
        {
            var Template = await _unitOfWork.Repository<TemplateMaster>().Entities.Include(x => x.TemplateImages).FirstOrDefaultAsync(x => x.Id == query.Id);
            var mappedTemplate = _mapper.Map<GetTemplateByIdResponse>(Template);
            return await Result<GetTemplateByIdResponse>.SuccessAsync(mappedTemplate);
        }
    }

    public class GetTemplateByIdResponse
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
        [MaxLength(250)]
        public string TemplateFolderPath { get; set; }
        [MaxLength(500)]
        public string ThumbnailImageUrl { get; set; }
        [MaxLength(250)]
        public string TagLine { get; set; }
        public int? Cost { get; set; }
        public string Features { get; set; }
        public string AuthorName { get; set; }
        public string AboutTemplate { get; set; }
        public virtual ICollection<TemplateImage> TemplateImages { get; set; }
    }
}