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
    public partial class GroomManRequestModel : IRequest<Result<int>>
    {
        public int Id { get; set; }
        [MaxLength(100)]
        public string FirstName { get; set; }
        [Required]
        [MaxLength(100)]
        public string LastName { get; set; }
        public DateTime? DateofBirth { get; set; }
        public string ImageUrl { get; set; }
        public int WeddingId { get; set; }
        public bool IsGroom { get; set; } = false;
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

    internal class AddEditGroomMenCommandHandler : IRequestHandler<GroomManRequestModel, Result<int>>
    {
        private readonly IMapper _mapper;
        private readonly IStringLocalizer<AddEditBrideMaidsCommandHandler> _localizer;
        private readonly IUnitOfWork<int> _unitOfWork;

        public AddEditGroomMenCommandHandler(IUnitOfWork<int> unitOfWork, IMapper mapper, IStringLocalizer<AddEditBrideMaidsCommandHandler> localizer)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _localizer = localizer;
        }

        public async Task<Result<int>> Handle(GroomManRequestModel command, CancellationToken cancellationToken)
        {
            string imageUrl = $"assets/images/wedding/{command.WeddingId}/{command.FirstName.ToLower()}_{command.LastName.ToLower()}.jpg";
            if (command.Id == 0)
            {
                var GroomMan = _mapper.Map<GroomAndMan>(command);
                GroomMan.ImageUrl = imageUrl;
                await _unitOfWork.Repository<GroomAndMan>().AddAsync(GroomMan);
                await _unitOfWork.CommitAndRemoveCache(cancellationToken, ApplicationConstants.Cache.GetWeddingCache);
                return await Result<int>.SuccessAsync(GroomMan.Id, _localizer["GroomAndMan Added"]);
            }
            else
            {
                var GroomMan = await _unitOfWork.Repository<GroomAndMan>().GetByIdAsync(command.Id);
                if (GroomMan != null)
                {
                    GroomMan.DateofBirth = command.DateofBirth;
                    GroomMan.FirstName = command.FirstName;
                    GroomMan.LastName = command.LastName;
                    GroomMan.ImageUrl = imageUrl;
                    GroomMan.IsGroom = command.IsGroom;
                    GroomMan.RelationWithGroom = command.RelationWithBride;
                    GroomMan.FbUrl = command.FbUrl;
                    GroomMan.GoogleUrl = command.GoogleUrl;
                    GroomMan.InstagramUrl = command.InstagramUrl;

                    await _unitOfWork.Repository<GroomAndMan>().UpdateAsync(GroomMan);
                    await _unitOfWork.CommitAndRemoveCache(cancellationToken, ApplicationConstants.Cache.GetWeddingCache);
                    return await Result<int>.SuccessAsync(GroomMan.Id, _localizer["GroomAndMan Updated"]);
                }
                else
                {
                    return await Result<int>.FailAsync(_localizer["GroomAndMan Not Found!"]);
                }
            }
        }
    }
}
