using BlazorHero.CleanArchitecture.Application.Interfaces.Repositories;
using LazyCache;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BlazorHero.CleanArchitecture.Shared.Wrapper;
using BlazorHero.CleanArchitecture.Domain.Entities.DreamWedds;
using BlazorHero.CleanArchitecture.Shared.Constants.Application;
using System.Threading;
using BlazorHero.CleanArchitecture.Application.Enums;
using BlazorHero.CleanArchitecture.Domain.Contracts;
using BlazorHero.CleanArchitecture.Application.Extensions;
using BlazorHero.CleanArchitecture.Application.Features.Common.Queries;
using BlazorHero.CleanArchitecture.Utilities;

namespace BlazorHero.CleanArchitecture.Application.Features.Weddings.Queries
{
    public class GetWeddingSummaryQuery : IRequest<Result<List<WeddingSummaryResponse>>>
    {
        public int Id { get; set; }
        public GetWeddingSummaryQuery(int id = 0)
        {
            Id = id;
        }
    }

    internal class GetWeddingSummaryQueryHandler : IRequestHandler<GetWeddingSummaryQuery, Result<List<WeddingSummaryResponse>>>
    {
        private readonly IUnitOfWork<int> _unitOfWork;
        private readonly IAppCache _cache;

        public GetWeddingSummaryQueryHandler(IUnitOfWork<int> unitOfWork, IAppCache cache)
        {
            _unitOfWork = unitOfWork;
            _cache = cache;
        }

        public async Task<Result<List<WeddingSummaryResponse>>> Handle(GetWeddingSummaryQuery query, CancellationToken cancellationToken)
        {
            List<WeddingSummaryResponse> result = new List<WeddingSummaryResponse>();
            var userWeddings = await _unitOfWork.Repository<Wedding>().Entities.Where(x => x.UserId == query.Id).ToListAsync();
            foreach (var item in userWeddings)
            {
                var wedding = await _unitOfWork.Repository<Wedding>().Entities.Include(x => x.BrideAndMaids).Include(x => x.GroomAndMen)
                .Include(x => x.TimeLines).Include(x => x.WeddingEvents).ThenInclude(x => x.Venue)
                .FirstAsync(x => x.Id == query.Id);

                WeddingSummaryResponse response = new WeddingSummaryResponse()
                {
                     Id = query.Id,
                     Title= item.Title,
                     IconUrl= item.IconUrl,
                     BackgroundImage = item.BackgroundImage,
                     WeddingDate= item.WeddingDate,
                     TemplateId = item.TemplateId,
                     CreatedOn= item.CreatedOn,
                     Status = wedding.WeddingEvents.Count() > 0 ? "Ready To Live" : "In Progress",
                     BrideImage = wedding.BrideAndMaids?.FirstOrDefault(x =>x.IsBride)?.ImageUrl,
                     GroomImage = wedding.GroomAndMen?.FirstOrDefault(x => x.IsGroom)?.ImageUrl,
                };

                result.Add(response);

            }

            return await Result<List<WeddingSummaryResponse>>.SuccessAsync(result);
        }
    }

    public class WeddingSummaryResponse : AuditableEntity<int>
    {
        public DateTime WeddingDate { get; set; }
        public string Title { get; set; }
        public string Status { get; set; }
        public string IconUrl { get; set; }
        public int? TemplateId { get; set; }
        public bool IsLoveMarriage { get; set; }
        public string BrideImage { get; set; }
        public string GroomImage { get; set; }
        public string BackgroundImage { get; set; }
      
    }
}
