using BlazorHero.CleanArchitecture.Application.Features.Faqs.Queries;
using BlazorHero.CleanArchitecture.Application.Features.Products.Queries.GetAllPaged;
using BlazorHero.CleanArchitecture.Shared.Constants.Permission;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Polly;
using System.Threading.Tasks;

namespace BlazorHero.CleanArchitecture.Server.Controllers.v1.DreamWedds
{
    public class HomeController : BaseApiController<HomeController>
    {
        /// <summary>
        /// Get All Faqs
        /// </summary>
        /// <param name="pageNumber"></param>
        /// <param name="pageSize"></param>
        /// <param name="searchString"></param>
        /// <param name="orderBy"></param>
        /// <returns>Status 200 OK</returns>
        [HttpGet]
        public async Task<IActionResult> GetAllFaqs(int pageNumber, int pageSize, string searchString, string orderBy = null)
        {
            var faqs = await _mediator.Send(new GetAllFaqsQuery());
            return Ok(faqs);
        }
    }
}
