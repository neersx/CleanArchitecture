using BlazorHero.CleanArchitecture.Application.Features.Products.Queries.GetProductImage;
using BlazorHero.CleanArchitecture.Application.Features.Templates.Queries;
using BlazorHero.CleanArchitecture.Application.Features.Templates.Queries.GetById;
using BlazorHero.CleanArchitecture.Shared.Constants.Permission;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Polly;
using System.Threading.Tasks;

namespace BlazorHero.CleanArchitecture.Server.Controllers.v1.Template
{
    [ApiController]
    public class ThemeController : BaseApiController<ThemeController>
    {

        /// <summary>
        /// Get all theme templats
        /// </summary>
        /// <returns>Status 200 OK</returns>
        [Authorize(Policy = Permissions.Themes.View)]
        [HttpGet("")]
        public async Task<IActionResult> GetAllWeddingThemes()
        {
            var result = await _mediator.Send(new GetAllTemplatesQuery());
            return Ok(result);
        }

        /// <summary>
        /// Get a template details by Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Status 200 OK</returns>
        [Authorize(Policy = Permissions.Themes.View)]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetThemeTemplateById(int id)
        {
            var result = await _mediator.Send(new GetTemplateByIdQuery(id));
            return Ok(result);
        }
    }
}
