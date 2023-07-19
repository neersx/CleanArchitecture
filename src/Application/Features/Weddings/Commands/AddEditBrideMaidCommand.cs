using AutoMapper;

using BlazorHero.CleanArchitecture.Application.Interfaces.Repositories;
using MediatR;
using Microsoft.Extensions.Localization;
using System.ComponentModel.DataAnnotations;
using System.Threading;
using System.Threading.Tasks;
using BlazorHero.CleanArchitecture.Shared.Wrapper;
using System;
using BlazorHero.CleanArchitecture.Domain.Entities.DreamWedds;
using BlazorHero.CleanArchitecture.Shared.Constants.Application;

namespace BlazorHero.CleanArchitecture.Application.Features.Weddings.Commands
{
    public partial class BrideMaidsRequestModel : IRequest<Result<int>>
    {
        public int Id {get; set;}
        [MaxLength(100)]
        public string FirstName { get; set; }
        [Required]
        [MaxLength(100)]
        public string LastName { get; set; }
        public DateTime? DateofBirth { get; set; }
        public string ImageUrl { get; set; }
        public int WeddingId { get; set; }
        public bool IsBride { get; set; } = false;
        public int? RelationWithBride { get; set; }
        [MaxLength(1250)]
        public string About { get; set; }
        [MaxLength(250)]
        public string FbUrl { get; set; }
        [MaxLength(500)]
        public string GoogleUrl { get; set; }
        [MaxLength(250)]
        public string InstagramUrl { get; set; }
        [MaxLength(250)]
        public string LinkedinUrl { get; set; }
    }
    internal class AddEditBrideMaidsCommandHandler : IRequestHandler<BrideMaidsRequestModel, Result<int>>
    {
        private readonly IMapper _mapper;
        private readonly IStringLocalizer<AddEditBrideMaidsCommandHandler> _localizer;
        private readonly IUnitOfWork<int> _unitOfWork;

        public AddEditBrideMaidsCommandHandler(IUnitOfWork<int> unitOfWork, IMapper mapper, IStringLocalizer<AddEditBrideMaidsCommandHandler> localizer)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _localizer = localizer;
        }

        public async Task<Result<int>> Handle(BrideMaidsRequestModel command, CancellationToken cancellationToken)
        {
            string imageUrl = $"assets/images/wedding/{command.WeddingId}/{command.FirstName.ToLower()}_{command.LastName.ToLower()}.jpg";
            if (command.Id == 0)
            {
                var BrideMaids = _mapper.Map<BrideAndMaid>(command);
                BrideMaids.ImageUrl = imageUrl;
                await _unitOfWork.Repository<BrideAndMaid>().AddAsync(BrideMaids);
                await _unitOfWork.CommitAndRemoveCache(cancellationToken, ApplicationConstants.Cache.GetWeddingCache);
                return await Result<int>.SuccessAsync(BrideMaids.Id, _localizer["BrideMaids Added"]);
            }
            else
            {
                var BrideMaids = await _unitOfWork.Repository<BrideAndMaid>().GetByIdAsync(command.Id);
                if (BrideMaids != null)
                {
                    BrideMaids.DateofBirth= command.DateofBirth;
                    BrideMaids.FirstName = command.FirstName;
                    BrideMaids.LastName = command.LastName;
                    BrideMaids.ImageUrl = imageUrl;
                    BrideMaids.IsBride = command.IsBride;
                    BrideMaids.RelationWithBride = command.RelationWithBride;
                    BrideMaids.FbUrl = command.FbUrl;
                    BrideMaids.GoogleUrl = command.GoogleUrl;
                    BrideMaids.InstagramUrl = command.InstagramUrl;

                    await _unitOfWork.Repository<BrideAndMaid>().UpdateAsync(BrideMaids);
                    await _unitOfWork.CommitAndRemoveCache(cancellationToken, ApplicationConstants.Cache.GetWeddingCache);
                    return await Result<int>.SuccessAsync(BrideMaids.Id, _localizer["BrideMaids Updated"]);
                }
                else
                {
                    return await Result<int>.FailAsync(_localizer["BrideMaids Not Found!"]);
                }
            }
        }
    }
}
