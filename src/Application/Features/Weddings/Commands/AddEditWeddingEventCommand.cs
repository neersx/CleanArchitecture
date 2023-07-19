using AutoMapper;
using BlazorHero.CleanArchitecture.Application.Interfaces.Repositories;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;
using System;
using System.ComponentModel.DataAnnotations;
using System.Threading;
using System.Threading.Tasks;
using BlazorHero.CleanArchitecture.Shared.Wrapper;
using System;
using BlazorHero.CleanArchitecture.Domain.Entities.DreamWedds;
using BlazorHero.CleanArchitecture.Shared.Constants.Application;

namespace BlazorHero.CleanArchitecture.Application.Features.Weddings.Commands
{
    public partial class WeddingEventsRequestModel : IRequest<Result<int>>
    {
        public int Id { get; set; }
        public DateTime EventDate { get; set; }
        [MaxLength(150)]
        public string OrganiserName { get; set; }
        [MaxLength(20)]
        public string OrganiserMobile { get; set; }
        [MaxLength(150)]
        public string Title { get; set; }
        public int WeddingId { get; set; }
        public string ImageUrl { get; set; }
        public bool IsActive { get; set; } = true;
        public DateTime? StartTime { get; set; }
        public DateTime? EndTime { get; set; }
        [MaxLength(1500)]
        public string AboutEvent { get; set; }
        public string BackGroundImage { get; set; }
        public EventVenueRequestModel? Venue { get; set; }
    }

    public class EventVenueRequestModel
    {
        [Required]
        [MaxLength(150)]
        public string Name { get; set; }
        public string ImageUrl { get; set; }
        public string BannerImageUrl { get; set; }
        [MaxLength(500)]
        public string Website { get; set; }
        [MaxLength(150)]
        public string OwnerName { get; set; }
        [MaxLength(15)]
        public string Phone { get; set; }
        [MaxLength(15)]
        public string Mobile { get; set; }
        public int? WeddingEventId { get; set; }
        [MaxLength(150)]
        public string Address { get; set; }
        [MaxLength(30)]
        public string City { get; set; }
        [MaxLength(50)]
        public string State { get; set; }
        [MaxLength(10)]
        public string PinCode { get; set; }
    }
    internal class AddEditWeddingEventsCommandHandler : IRequestHandler<WeddingEventsRequestModel, Result<int>>
    {
        private readonly IMapper _mapper;
        private readonly IStringLocalizer<AddEditWeddingEventsCommandHandler> _localizer;
        private readonly IUnitOfWork<int> _unitOfWork;

        public AddEditWeddingEventsCommandHandler(IUnitOfWork<int> unitOfWork, IMapper mapper, IStringLocalizer<AddEditWeddingEventsCommandHandler> localizer)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _localizer = localizer;
        }

        public async Task<Result<int>> Handle(WeddingEventsRequestModel command, CancellationToken cancellationToken)
        {
            string imageUrl = $"assets/images/wedding/{command.WeddingId}/events/{command.Title.ToLower()}.jpg";
            if (command.Id == 0)
            {
                var WeddingEvents = _mapper.Map<WeddingEvent>(command);
                WeddingEvents.ImageUrl = imageUrl;
                await _unitOfWork.Repository<WeddingEvent>().AddAsync(WeddingEvents);
                await _unitOfWork.CommitAndRemoveCache(cancellationToken, ApplicationConstants.Cache.GetWeddingCache);
                return await Result<int>.SuccessAsync(WeddingEvents.Id, _localizer["WeddingEvents Added"]);
            }
            else
            {
                var weddingEvent = await _unitOfWork.Repository<WeddingEvent>().Entities.Include(x => x.Venue).FirstOrDefaultAsync(x => x.Id == command.Id);

                if (weddingEvent != null)
                {
                    weddingEvent.EventDate = command.EventDate;
                    weddingEvent.OrganiserMobile = command.OrganiserMobile;
                    weddingEvent.OrganiserName = command.OrganiserName;
                    weddingEvent.Title = command.Title;
                    weddingEvent.AboutEvent = command.AboutEvent;
                    weddingEvent.ImageUrl = imageUrl;
                    weddingEvent.BackGroundImage = command.BackGroundImage;
                    weddingEvent.StartTime = command.StartTime;
                    weddingEvent.EndTime = command.EndTime;

                    weddingEvent.Venue.Name = command.Venue.Name;
                    weddingEvent.Venue.Address = command.Venue?.Address;
                    weddingEvent.Venue.WeddingEventId = weddingEvent?.Id;
                    weddingEvent.Venue.State = command.Venue?.State;
                    weddingEvent.Venue.Mobile = command.Venue?.Mobile;
                    weddingEvent.Venue.City = command.Venue?.City;
                    weddingEvent.Venue.ImageUrl = command.Venue?.ImageUrl;
                    weddingEvent.Venue.OwnerName = command.Venue?.OwnerName;
                    weddingEvent.Venue.PinCode = command.Venue?.PinCode;


                    await _unitOfWork.Repository<WeddingEvent>().UpdateAsync(weddingEvent);

                    if (weddingEvent.Venue.Id == 0)
                        await _unitOfWork.Repository<EventVenue>().AddAsync(weddingEvent.Venue);
                    else
                        await _unitOfWork.Repository<EventVenue>().UpdateAsync(weddingEvent.Venue);

                    await _unitOfWork.CommitAndRemoveCache(cancellationToken, ApplicationConstants.Cache.GetWeddingCache);
                    return await Result<int>.SuccessAsync(weddingEvent.Id, _localizer["WeddingEvents Updated"]);
                }
                else
                {
                    return await Result<int>.FailAsync(_localizer["WeddingEvents Not Found!"]);
                }
            }
        }
    }
}
