using BlazorHero.CleanArchitecture.Application.Features.Blogs.Queries;
using BlazorHero.CleanArchitecture.Application.Features.Faqs.Queries;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace BlazorHero.CleanArchitecture.Server.Controllers.v1.DreamWedds
{
    public class WebBlogsController :  BaseApiController<WebBlogsController>
    {
        /// <summary>
        /// Get All Blogs
        /// </summary>
        /// <param name="pageNumber"></param>
        /// <param name="pageSize"></param>
        /// <param name="searchString"></param>
        /// <param name="orderBy"></param>
        /// <returns>Status 200 OK</returns>
        [HttpGet]
        public async Task<IActionResult> GetAllBlogs(int pageNumber, int pageSize, string searchString, string orderBy = null)
        {
            var blogs = await _mediator.Send(new GetAllBlogsQuery());
            return Ok(blogs);
        }
    }
}
