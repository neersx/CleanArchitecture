using System.ComponentModel.DataAnnotations;
using AutoMapper;
using BlazorHero.CleanArchitecture.Application.Interfaces.Repositories;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Localization;
using BlazorHero.CleanArchitecture.Shared.Wrapper;
using BlazorHero.CleanArchitecture.Domain.Entities.DreamWedds;
using BlazorHero.CleanArchitecture.Shared.Constants.Application;

namespace BlazorHero.CleanArchitecture.Application.Features.Template.Commands.AddEdit
{
    public partial class AddEditTemplateCommand : IRequest<Result<int>>
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Content { get; set; }
        [Required]
        public string Tags { get; set; }
    }

    internal class AddEditTemplateCommandHandler : IRequestHandler<AddEditTemplateCommand, Result<int>>
    {
        private readonly IMapper _mapper;
        private readonly IStringLocalizer<AddEditTemplateCommandHandler> _localizer;
        private readonly IUnitOfWork<int> _unitOfWork;

        public AddEditTemplateCommandHandler(IUnitOfWork<int> unitOfWork, IMapper mapper, IStringLocalizer<AddEditTemplateCommandHandler> localizer)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _localizer = localizer;
        }

        public async Task<Result<int>> Handle(AddEditTemplateCommand command, CancellationToken cancellationToken)
        {
            if (command.Id == 0)
            {
                var Template = _mapper.Map<TemplateMaster>(command);
                await _unitOfWork.Repository<TemplateMaster>().AddAsync(Template);
                await _unitOfWork.CommitAndRemoveCache(cancellationToken, ApplicationConstants.Cache.GetAllTemplatesCacheKey);
                return await Result<int>.SuccessAsync(Template.Id, _localizer["Template Saved"]);
            }
            else
            {
                var Template = await _unitOfWork.Repository<TemplateMaster>().GetByIdAsync(command.Id);
                if (Template != null)
                {
                    Template.Name = command.Name ?? Template.Name;
                    Template.Content = command.Content ?? Template.Content;
                    await _unitOfWork.Repository<TemplateMaster>().UpdateAsync(Template);
                    await _unitOfWork.CommitAndRemoveCache(cancellationToken, ApplicationConstants.Cache.GetAllTemplatesCacheKey);
                    return await Result<int>.SuccessAsync(Template.Id, _localizer["Template Updated"]);
                }
                else
                {
                    return await Result<int>.FailAsync(_localizer["Template Not Found!"]);
                }
            }
        }
    }
}