using AutoMapper;
using BlazorHero.CleanArchitecture.Application.Interfaces.Repositories;
using BlazorHero.CleanArchitecture.Domain.Entities.DreamWedds;
using BlazorHero.CleanArchitecture.Shared.Constants.Application;
using BlazorHero.CleanArchitecture.Shared.Wrapper;
using MediatR;
using Microsoft.Extensions.Localization;
using System.ComponentModel.DataAnnotations;
using System.Threading;
using System.Threading.Tasks;

namespace BlazorHero.CleanArchitecture.Application.Features.Contact
{
    public partial class AddEditContactUsRequest : IRequest<Result<int>>
    {
        [Required]
        public int Id { get; set; }
        [MaxLength(50)]
        public string FirstName { get; set; }
        [MaxLength(50)]
        public string LastName { get; set; }
        [MaxLength(150)]
        public string Email { get; set; }
        [MaxLength(250)]
        public string Subject { get; set; }

        [MaxLength(15)]
        public string Mobile { get; set; }
        [MaxLength(1000)]
        public string Message { get; set; }
        [MaxLength(20)]
        public string ContactFor { get; set; }
    }
    internal class AddEditContactUsCommandHandler : IRequestHandler<AddEditContactUsRequest, Result<int>>
    {
        private readonly IMapper _mapper;
        private readonly IStringLocalizer<AddEditContactUsCommandHandler> _localizer;
        private readonly IUnitOfWork<int> _unitOfWork;

        public AddEditContactUsCommandHandler(IUnitOfWork<int> unitOfWork, IMapper mapper, IStringLocalizer<AddEditContactUsCommandHandler> localizer)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _localizer = localizer;
        }

        public async Task<Result<int>> Handle(AddEditContactUsRequest command, CancellationToken cancellationToken)
        {
            if (command.Id == 0)
            {
                var Contact = _mapper.Map<ContactUs>(command);
                await _unitOfWork.Repository<ContactUs>().AddAsync(Contact);
                return await Result<int>.SuccessAsync(Contact.Id, _localizer["Contact Inquiry Sent."]);
            }
            else
            {
                var Model = await _unitOfWork.Repository<ContactUs>().GetByIdAsync(command.Id);
                if (Model != null)
                {
                    Model.Subject = command.Subject;
                    Model.Message = command.Message;
                    Model.Mobile = command.Mobile;
                    Model.Email = command.Email;

                    await _unitOfWork.Repository<ContactUs>().UpdateAsync(Model);
                    await _unitOfWork.CommitAndRemoveCache(cancellationToken, ApplicationConstants.Cache.GetAllBlogsCacheKey);
                    return await Result<int>.SuccessAsync(Model.Id, _localizer["Contact Request"]);
                }
                else
                {
                    return await Result<int>.FailAsync(_localizer["Contact Us Not Found!"]);
                }
            }
        }
    }
}
