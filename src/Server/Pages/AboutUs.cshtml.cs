
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
    public class AboutUsModel : BasePageModel<AboutUsModel>
    {
        public async Task OnGet()
        {
            var MetaTags = await _mediator.Send(new GetAllMetaTagsByPageNameQuery(KnownValues.KnownHtmlPage.AboutUs));
            string MetagTagsString = HtmlPageExtensions.GetMetadataString(MetaTags.Data) ;
            ViewData["LoadMetaTag"] = MetagTagsString;
        }
    }
}
