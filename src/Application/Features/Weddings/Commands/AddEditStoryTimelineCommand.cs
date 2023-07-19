using AutoMapper;
using BlazorHero.CleanArchitecture.Application.Interfaces.Repositories;
using MediatR;
using Microsoft.Extensions.Localization;
using System;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using System.Threading;
using System.Threading.Tasks;
using BlazorHero.CleanArchitecture.Shared.Wrapper;
using System;
using BlazorHero.CleanArchitecture.Domain.Entities.DreamWedds;
using BlazorHero.CleanArchitecture.Shared.Constants.Application;

namespace BlazorHero.CleanArchitecture.Application.Features.StorytimeLines.Commands
{
    public partial class StorytimeLineRequestModel : IRequest<Result<int>>
    {
        public int Id { get; set; }
        public DateTime StoryDate { get; set; }
        [Required]
        [MaxLength(100)]
        public string Title { get; set; }
        [Required]
        [MaxLength(1000)]
        public string Story { get; set; }
        public string ImageUrl { get; set; }
        public int WeddingId { get; set; }
        [MaxLength(100)]
        public string Location { get; set; }

    }
    internal class AddEditStorytimeLineCommandHandler : IRequestHandler<StorytimeLineRequestModel, Result<int>>
    {
        private readonly IMapper _mapper;
        private readonly IStringLocalizer<AddEditStorytimeLineCommandHandler> _localizer;
        private readonly IUnitOfWork<int> _unitOfWork;

        public AddEditStorytimeLineCommandHandler(IUnitOfWork<int> unitOfWork, IMapper mapper, IStringLocalizer<AddEditStorytimeLineCommandHandler> localizer)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _localizer = localizer;
        }

        public async Task<Result<int>> Handle(StorytimeLineRequestModel command, CancellationToken cancellationToken)
        {
            if (command.Id == 0)
            {
                var StorytimeLine = _mapper.Map<TimeLine>(command);
                StorytimeLine.ImageUrl = $"assets/images/wedding/{command.WeddingId}/timeline/background.jpg";
                await _unitOfWork.Repository<TimeLine>().AddAsync(StorytimeLine);
                await _unitOfWork.CommitAndRemoveCache(cancellationToken, ApplicationConstants.Cache.GetWeddingCache);
                return await Result<int>.SuccessAsync(StorytimeLine.Id, _localizer["StorytimeLine Added"]);
            }
            else
            {
                var StorytimeLine = await _unitOfWork.Repository<TimeLine>().GetByIdAsync(command.Id);
                if (StorytimeLine != null)
                {
                    StorytimeLine.StoryDate = command.StoryDate;
                    StorytimeLine.Title = command.Title;
                    StorytimeLine.Story = command.Story;
                    StorytimeLine.Location = command.Location;
                    StorytimeLine.ImageUrl = $"assets/images/wedding/{command.WeddingId}/timeline/background.jpg";

                    await _unitOfWork.Repository<TimeLine>().UpdateAsync(StorytimeLine);
                    await _unitOfWork.CommitAndRemoveCache(cancellationToken, ApplicationConstants.Cache.GetWeddingCache);
                    return await Result<int>.SuccessAsync(StorytimeLine.Id, _localizer["StorytimeLine Updated"]);
                }
                else
                {
                    return await Result<int>.FailAsync(_localizer["StorytimeLine Not Found!"]);
                }
            }
        }
    }
}
