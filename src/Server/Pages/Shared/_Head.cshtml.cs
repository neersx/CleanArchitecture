using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace BlazorHero.CleanArchitecture.Server.Pages.Shared
{
    public class _HeadModel : PageModel
    {
        public void OnGet()
        {
            ViewData["title"] = "This is test title for meta data check.";
        }
    }
}
