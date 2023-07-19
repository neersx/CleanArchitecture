using AutoMapper;
using BlazorHero.CleanArchitecture.Application.Interfaces.Repositories;
using LazyCache;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using BlazorHero.CleanArchitecture.Shared.Wrapper;
using BlazorHero.CleanArchitecture.Domain.Entities.DreamWedds;
using BlazorHero.CleanArchitecture.Shared.Constants.Application;
using System.Threading;
using BlazorHero.CleanArchitecture.Application.Enums;
using BlazorHero.CleanArchitecture.Domain.Contracts;

namespace BlazorHero.CleanArchitecture.Application.Features.Weddings.Queries
{
    public class GetWeddingByIdQuery : IRequest<Result<WeddingByIdResponse>>
	{
		public int Id { get; set; }
        public GetWeddingByIdQuery(int id = 0)
		{
            Id = id;
        }
    }

	internal class GetWeddingByIdQueryHandler : IRequestHandler<GetWeddingByIdQuery, Result<WeddingByIdResponse>>
	{
		private readonly IUnitOfWork<int> _unitOfWork;
		private readonly IMapper _mapper;
		private readonly IAppCache _cache;

		public GetWeddingByIdQueryHandler(IUnitOfWork<int> unitOfWork, IMapper mapper, IAppCache cache)
		{
			_unitOfWork = unitOfWork;
			_mapper = mapper;
			_cache = cache;
		}

		public async Task<Result<WeddingByIdResponse>> Handle(GetWeddingByIdQuery query, CancellationToken cancellationToken)
		{
			var wedding = await _unitOfWork.Repository<Wedding>().Entities.Include(x => x.BrideAndMaids).Include(x => x.GroomAndMen)
				.Include(x =>x.TimeLines).Include(x => x.WeddingEvents).ThenInclude(x =>x.Venue)
                .FirstAsync(x => x.Id == query.Id);
			var mappedBrand = _mapper.Map<WeddingByIdResponse>(wedding);
			return await Result<WeddingByIdResponse>.SuccessAsync(mappedBrand);
		}
	}

	public class WeddingByIdResponse : AuditableEntity<int>
	{
		public WeddingByIdResponse()
		{
			BrideAndMaids = new HashSet<BrideAndMaid>();
			GroomAndMen = new HashSet<GroomAndMan>();
			RsvpDetails = new HashSet<RsvpDetail>();
			TimeLines = new HashSet<TimeLine>();
			UserWeddingSubscriptions = new HashSet<UserWeddingSubscription>();
			WeddingEvents = new HashSet<WeddingEvent>();
			WeddingGalleries = new HashSet<WeddingGalleryImages>();
		}
		public DateTime WeddingDate { get; set; }
		[MaxLength(250)]
		[Required]
		public string Title { get; set; }
		public int WeddingStyle { get; set; }
		[MaxLength(500)]
		public string IconUrl { get; set; }
		public int? TemplateID { get; set; }
		public bool IsLoveMarriage { get; set; }
		public int? UserID { get; set; }
		public bool IsActive { get; set; }
		[MaxLength(500)]
		public string BackgroundImage { get; set; }
		[MaxLength(500)]
		public string Quote { get; set; }
		[MaxLength(250)]
		public string FbPageUrl { get; set; }
		[MaxLength(250)]
		public string VideoUrl { get; set; }

		public TemplateMaster Template { get; set; }
		public ICollection<BrideAndMaid> BrideAndMaids { get; set; }
		public ICollection<GroomAndMan> GroomAndMen { get; set; }
		public ICollection<RsvpDetail> RsvpDetails { get; set; }
		public ICollection<TimeLine> TimeLines { get; set; }
		public ICollection<UserWeddingSubscription> UserWeddingSubscriptions { get; set; }
		public ICollection<WeddingEvent> WeddingEvents { get; set; }
		public ICollection<WeddingGalleryImages> WeddingGalleries { get; set; }
	}
}
