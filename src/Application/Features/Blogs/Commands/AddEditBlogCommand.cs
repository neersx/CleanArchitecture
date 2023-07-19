using AutoMapper;

using BlazorHero.CleanArchitecture.Application.Interfaces.Repositories;
using BlazorHero.CleanArchitecture.Shared.Wrapper;
using MediatR;
using Microsoft.Extensions.Localization;
using System.ComponentModel.DataAnnotations;
using System.Threading;
using System.Threading.Tasks;
using BlazorHero.CleanArchitecture.Domain.Entities.DreamWedds;
using BlazorHero.CleanArchitecture.Shared.Constants.Application;

namespace BlazorHero.CleanArchitecture.Application.Features.Blogs.Commands
{
    public partial class AddEditBlogRequest : IRequest<Result<int>>
    {
        [Required]
        public int Id { get; set; }
        [Required]
        [MaxLength(150)]
        public string BlogName { get; set; }
        [MaxLength(150)]
        public string Title { get; set; }
        [MaxLength(250)]
        public string BlogSubject { get; set; }
        [MaxLength(500)]
        public string Quote { get; set; }
        [MaxLength(150)]
        public string AuthorName { get; set; }
        public string Content { get; set; }
        [MaxLength(500)]
        public string ImageUrl { get; set; }
        public int BlogType { get; set; } = 0;
    }
    internal class AddEditBlogCommandHandler : IRequestHandler<AddEditBlogRequest, Result<int>>
    {
        private readonly IMapper _mapper;
        private readonly IStringLocalizer<AddEditBlogCommandHandler> _localizer;
        private readonly IUnitOfWork<int> _unitOfWork;

        public AddEditBlogCommandHandler(IUnitOfWork<int> unitOfWork, IMapper mapper, IStringLocalizer<AddEditBlogCommandHandler> localizer)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _localizer = localizer;
        }

        public async Task<Result<int>> Handle(AddEditBlogRequest command, CancellationToken cancellationToken)
        {
            if (command.Id == 0)
            {
                var Blog = _mapper.Map<Blog>(command);
                await _unitOfWork.Repository<Blog>().AddAsync(Blog);
                await _unitOfWork.CommitAndRemoveCache(cancellationToken, ApplicationConstants.Cache.GetAllBlogsCacheKey);
                return await Result<int>.SuccessAsync(Blog.Id, _localizer["Blog Saved"]);
            }
            else
            {
                var Blog = await _unitOfWork.Repository<Blog>().GetByIdAsync(command.Id);
                if (Blog != null)
                {
                    Blog.BlogName = command.Title.Replace(" ", "-").ToLower();
                    Blog.BlogSubject = command.Title;
                    Blog.Title = command.Title;
                    Blog.Quote = command.Quote;
                    Blog.BlogType = command.BlogType;
                    Blog.AuthorName = command.AuthorName;
                    Blog.Content = command.Content;
                    Blog.ImageUrl = command.ImageUrl;

                    await _unitOfWork.Repository<Blog>().UpdateAsync(Blog);
                    await _unitOfWork.CommitAndRemoveCache(cancellationToken, ApplicationConstants.Cache.GetAllBlogsCacheKey);
                    return await Result<int>.SuccessAsync(Blog.Id, _localizer["Blog Updated"]);
                }
                else
                {
                    return await Result<int>.FailAsync(_localizer["Blog Not Found!"]);
                }
            }
        }
    }
}
