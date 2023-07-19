using AutoMapper;
using BlazorHero.CleanArchitecture.Application.Features.Templates.Queries.GetById;
using BlazorHero.CleanArchitecture.Application.Interfaces.Repositories;
using MediatR;
using System.Threading.Tasks;
using BlazorHero.CleanArchitecture.Shared.Wrapper;
using BlazorHero.CleanArchitecture.Domain.Entities.DreamWedds;
using System.Threading;

namespace BlazorHero.CleanArchitecture.Application.Features.Templates.Queries
{
    public class GetTemplateByNameQuery : IRequest<Result<GetTemplateByIdResponse>>
    {
        public string Name { get; set; }
        public GetTemplateByNameQuery(string name)
        {
            Name = name;
        }
    }

    internal class GetTemplateByNameQueryHandler : IRequestHandler<GetTemplateByNameQuery, Result<GetTemplateByIdResponse>>
    {
        private readonly IUnitOfWork<int> _unitOfWork;
        private readonly IMapper _mapper;

        public GetTemplateByNameQueryHandler(IUnitOfWork<int> unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Result<GetTemplateByIdResponse>> Handle(GetTemplateByNameQuery query, CancellationToken cancellationToken)
        {
            var Template = await _unitOfWork.Repository<TemplateMaster>().FindByAsync(name => name.Name == query.Name);
            var mappedTemplate = _mapper.Map<GetTemplateByIdResponse>(Template);
            return await Result<GetTemplateByIdResponse>.SuccessAsync(mappedTemplate);
        }
    }

}
