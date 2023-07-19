using AutoMapper;

using BlazorHero.CleanArchitecture.Application.Interfaces.Repositories;
using MediatR;
using Microsoft.Extensions.Localization;
using System;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using System.Threading;
using BlazorHero.CleanArchitecture.Shared.Wrapper;
using BlazorHero.CleanArchitecture.Domain.Entities.DreamWedds;
using BlazorHero.CleanArchitecture.Shared.Constants.Application;

namespace BlazorHero.CleanArchitecture.Application.Features.Weddings.Commands
{
    public partial class WeddingRequestModel : IRequest<Result<int>>
    {
        [Required]
        public int Id { get; set; }
       public DateTime WeddingDate { get; set; }
        [MaxLength(250)]
        [Required]
        public string Title { get; set; }
        public int WeddingStyle { get; set; }
        public string IconUrl { get; set; }
        public int? TemplateId { get; set; }
        public bool IsLoveMarriage { get; set; }
        public int? UserID { get; set; }
        public bool IsActive { get; set; }
        public string BackgroundImage { get; set; }
        [MaxLength(500)]
        public string Quote { get; set; }
        [MaxLength(250)]
        public string FbPageUrl { get; set; }
        public string VideoUrl { get; set; }
    }
    internal class AddEditWeddingCommandHandler : IRequestHandler<WeddingRequestModel, Result<int>>
    {
        private readonly IMapper _mapper;
        private readonly IStringLocalizer<AddEditWeddingCommandHandler> _localizer;
        private readonly IUnitOfWork<int> _unitOfWork;

        public AddEditWeddingCommandHandler(IUnitOfWork<int> unitOfWork, IMapper mapper, IStringLocalizer<AddEditWeddingCommandHandler> localizer)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _localizer = localizer;
        }

        public async Task<Result<int>> Handle(WeddingRequestModel command, CancellationToken cancellationToken)
        {
            if (command.Id == 0)
            {
                var Wedding = _mapper.Map<Wedding>(command);
                Wedding.BackgroundImage = $"assets/images/wedding/{Wedding.Id}/background.jpg";
                Wedding.IconUrl = $"assets/images/wedding/{Wedding.Id}/logo.jpg";
                await _unitOfWork.Repository<Wedding>().AddAsync(Wedding);
                await _unitOfWork.CommitAndRemoveCache(cancellationToken, ApplicationConstants.Cache.GetWeddingCache);
                return await Result<int>.SuccessAsync(Wedding.Id, _localizer["Wedding Added"]);
            }
            else
            {
                var Wedding = await _unitOfWork.Repository<Wedding>().GetByIdAsync(command.Id);
                if (Wedding != null)
                {
                    Wedding.WeddingDate = command.WeddingDate;
                    Wedding.Title = command.Title;
                    Wedding.Quote = command.Quote;
                    Wedding.WeddingStyle = command.WeddingStyle;
                    Wedding.BackgroundImage = $"assets/images/wedding/{Wedding.Id}/background.jpg";
                    Wedding.IconUrl = $"assets/images/wedding/{Wedding.Id}/logo.jpg";
                    Wedding.VideoUrl = command.VideoUrl;

                    await _unitOfWork.Repository<Wedding>().UpdateAsync(Wedding);
                    await _unitOfWork.CommitAndRemoveCache(cancellationToken, ApplicationConstants.Cache.GetWeddingCache);
                    return await Result<int>.SuccessAsync(Wedding.Id, _localizer["Wedding Updated"]);
                }
                else
                {
                    return await Result<int>.FailAsync(_localizer["Wedding Not Found!"]);
                }
            }
        }
    }
}
