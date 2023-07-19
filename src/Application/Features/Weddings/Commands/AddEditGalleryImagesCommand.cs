using AutoMapper;
using BlazorHero.CleanArchitecture.Application.Interfaces.Repositories;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Localization;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using System.Threading;
using System.Threading.Tasks;
using BlazorHero.CleanArchitecture.Shared.Wrapper;
using System;
using BlazorHero.CleanArchitecture.Domain.Entities.DreamWedds;
using BlazorHero.CleanArchitecture.Shared.Constants.Application;

namespace BlazorHero.CleanArchitecture.Application.Features.Weddings.Commands
{

    public class WeddingGalleryRequestModel : IRequest<Result<int>>
    {
        public List<IFormFile> Files { get; set; }
    }
        public class GalleryRequestModel : IRequest<Result<int>>
    {
        public int Id { get; set; }
        [Required]
        [MaxLength(100)]
        public string Title { get; set; }
        public string Name { get; set; }
        public int WeddingId { get; set; }
        public DateTime? DateTaken { get; set; }
        [MaxLength(100)]
        public string Place { get; set; }
        [Display(Name = "Choose the gallery images of your wedding")]
        public IFormFile File { get; set; }

    }

    internal class AddEditGalleryCommandHandler : IRequestHandler<GalleryRequestModel, Result<int>>
    {
        private readonly IMapper _mapper;
        private readonly IStringLocalizer<AddEditGalleryCommandHandler> _localizer;
        private readonly IUnitOfWork<int> _unitOfWork;

        public AddEditGalleryCommandHandler(IUnitOfWork<int> unitOfWork, IMapper mapper, IStringLocalizer<AddEditGalleryCommandHandler> localizer)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _localizer = localizer;
        }

        public async Task<Result<int>> Handle(GalleryRequestModel command, CancellationToken cancellationToken)
        {
            var name = command.Name.ToLower().Replace(' ', '-');
            command.Name = name;

            if (command.Id == 0)
            {
                var Gallery = _mapper.Map<WeddingGalleryImages>(command);
                Gallery.ImageUrl = $"assets/images/wedding/{command.WeddingId}/gallery/{name}.jpg";
                await _unitOfWork.Repository<WeddingGalleryImages>().AddAsync(Gallery);
                await _unitOfWork.CommitAndRemoveCache(cancellationToken, ApplicationConstants.Cache.GetWeddingCache);
                return await Result<int>.SuccessAsync(Gallery.Id, _localizer["Gallery Added"]);
            }
            else
            {
                var Gallery = await _unitOfWork.Repository<WeddingGalleryImages>().GetByIdAsync(command.Id);
                if (Gallery != null)
                {
                    Gallery.Name = name;
                    Gallery.Title = command.Title;
                    Gallery.Place = command.Place;
                    Gallery.ImageUrl = $"assets/images/wedding/{command.WeddingId}/gallery/{name}.jpg";

                    await _unitOfWork.Repository<WeddingGalleryImages>().UpdateAsync(Gallery);
                    await _unitOfWork.CommitAndRemoveCache(cancellationToken, ApplicationConstants.Cache.GetWeddingCache);
                    return await Result<int>.SuccessAsync(Gallery.Id, _localizer["Gallery Updated"]);
                }
                else
                {
                    return await Result<int>.FailAsync(_localizer["Gallery Not Found!"]);
                }
            }
        }
    }
}
