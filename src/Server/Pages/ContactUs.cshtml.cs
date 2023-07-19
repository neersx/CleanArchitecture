using BlazorHero.CleanArchitecture.Application.Features.Common.Queries;
using BlazorHero.CleanArchitecture.Application.Features.Contact;
using Microsoft.AspNetCore.Mvc;
using System.Threading;
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

namespace BlazorHero.CleanArchitecture.Server.Pages
{
    public class ContactUsModel : BasePageModel<ContactUsModel>
    {

        [BindProperty]
        public AddEditContactUsRequest Request { get; set; } = default!;
        public async Task OnGet()
        {
            var MetaTags = await _mediator.Send(new GetAllMetaTagsByPageNameQuery(KnownValues.KnownHtmlPage.ContactUs));
            string MetagTagsString = HtmlPageExtensions.GetMetadataString(MetaTags.Data);
            ViewData["LoadMetaTag"] = MetagTagsString;
        }

        public async Task<IActionResult> OnPostAsync(CancellationToken cancellationToken)
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }
            Request.ContactFor = "Wedding";
            await _mediator.Send(Request, cancellationToken);
            ViewData["Success"] = "Success";
            return Page();
        }
    }
}
