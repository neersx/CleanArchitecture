using BlazorHero.CleanArchitecture.Application.Features.Common.Queries;
using System;
using System.Collections.Generic;
using System.Linq;
using BlazorHero.CleanArchitecture.Shared.Wrapper;
using BlazorHero.CleanArchitecture.Domain.Entities.DreamWedds;
using BlazorHero.CleanArchitecture.Shared.Constants.Application;
using System.Threading;
using BlazorHero.CleanArchitecture.Application.Enums;
using BlazorHero.CleanArchitecture.Domain.Contracts;
using BlazorHero.CleanArchitecture.Application.Extensions;
using BlazorHero.CleanArchitecture.Application.Features.Common.Queries;
using BlazorHero.CleanArchitecture.Utilities;

namespace BlazorHero.CleanArchitecture.Application.Extensions
{
    public static class HtmlPageExtensions
    {
        public static string GetMetadataString(List<GetAllMetaTagsResponse> list, string title = "")
        {
            string metadata = "";
            if (!string.IsNullOrWhiteSpace(title))
            {
                metadata +=
                    $"<meta name=\"twitter:title\" content=\" {title}\">{Environment.NewLine}";
                metadata +=
                    $"<meta property=\"og:title\" content=\" {title}\">{Environment.NewLine}";
                metadata += $"<title>{title}</title>{Environment.NewLine}";
            }
            foreach (var item in list)
            {
                if (
                    item.TagPrefix == KnownValues.KnownTagPrefix.Title
                    && string.IsNullOrWhiteSpace(title)
                )
                {
                    metadata +=
                        $"<meta name=\"twitter:title\" content=\" {item.Content}\">{Environment.NewLine}";
                    metadata +=
                        $"<meta property=\"og:title\" content=\" {item.Content}\">{Environment.NewLine}";
                    metadata += $"<title>{item.Content}</title>{Environment.NewLine}";
                }

                if (item.TagPrefix == KnownValues.KnownTagPrefix.Image)
                {
                    metadata +=
                        $"<meta name=\"twitter:image\" content=\"{item.Content}\">{Environment.NewLine}";
                    metadata +=
                        $"<meta name=\"twitter:card\" content=\"summary_large_image\">{Environment.NewLine}";

                    metadata +=
                        $"<meta property=\"og:image\" content=\"{item.Content}\">{Environment.NewLine}";
                    metadata +=
                        $"<meta property=\"og:image:width\" content=\"1000\">{Environment.NewLine}";
                    metadata +=
                        $"<meta property=\"og:image:height\" content=\"500\">{Environment.NewLine}";
                }

                if (item.TagPrefix == KnownValues.KnownTagPrefix.Description)
                {
                    metadata +=
                        $"<meta name=\"twitter:description\" content=\" {item.Content}\">{Environment.NewLine}";
                    metadata +=
                        $"<meta property=\"og:description\" content=\" {item.Content}\"{Environment.NewLine}>";
                    metadata +=
                        $"<meta name=\"description\" content=\"{item.Content}\">{Environment.NewLine}";
                }

                if (item.TagPrefix == KnownValues.KnownTagPrefix.Keywords)
                {
                    metadata +=
                        $"<meta name=\"keywords\" content=\"{item.Content}\">{Environment.NewLine}";
                }

                if (item.TagPrefix == KnownValues.KnownTagPrefix.SiteName)
                {
                    metadata +=
                        $"<meta property=\"{item.Property}\" content=\"{item.Content}\">{Environment.NewLine}";
                    metadata +=
                        $"<meta name=\"{item.Name}\" content=\"{item.Content}\">{Environment.NewLine}";
                }
            }

            if (!metadata.Contains("twitter:site"))
            {
                metadata +=
                    $"<meta property=\"twitter:site\" content=\"@thedreamwedds\">{Environment.NewLine}";
                metadata +=
                    $"<meta name=\"og:site_name\" content=\"Dream Wedds | Your personal wedding website\">{Environment.NewLine}";
            }

            metadata += $"<link rel=\"canonical\" href=\"https:dreamwedds.com\">{Environment.NewLine}";
            metadata += $"<meta name=\"twitter:card\" content=\"summary_large_image\">{Environment.NewLine}";
            metadata += $"<meta property=\"og:type\" content=\"article\">{Environment.NewLine}";
            metadata += $"<meta name=\"robots\" content=\"max-image-preview:large\">{Environment.NewLine}";
            metadata += $"<meta name=\"og:locale\" content=\"en-US\">{Environment.NewLine}";
            metadata += $"<meta name=\"og:type\" content=\"website\">{Environment.NewLine}";
            metadata += $"<meta property=\"article:tag\" content=\"Elegance, Beautiful Woman, Bridal Lehanga, Dream Wedding, Culture, Wedding Website, Beautiful Wedding Images, India, Ornamented, Square Format Image, Tradition, Wedding, Wedding Dress, Online Wedding, Personal Wedding Website\">";
            metadata += $"<meta name=\"og:url\" content=\"https:dreamwedds.com\">{Environment.NewLine}";
            return metadata;
        }

        public static string GetMetadataString(QuickMetaTags tag)
        {
            string metadata = "";
            if (!string.IsNullOrWhiteSpace(tag.Title))
            {
                metadata += $"<meta name=\"twitter:title\" content=\" {tag.Title}\">{Environment.NewLine}";
                metadata += $"<meta property=\"og:title\" content=\" {tag.Title}\">{Environment.NewLine}";
                metadata += $"<title>{tag.Title}</title>{Environment.NewLine}";
            }

            metadata += $"<meta name=\"twitter:image\" content=\"{tag.Image}\">{Environment.NewLine}";
            metadata += $"<meta name=\"twitter:card\" content=\"summary_large_image\">{Environment.NewLine}";

            metadata += $"<meta property=\"og:image\" content=\"{tag.Image}\">{Environment.NewLine}";
            metadata += $"<meta property=\"og:image:width\" content=\"1000\">{Environment.NewLine}";
            metadata += $"<meta property=\"og:image:height\" content=\"500\">{Environment.NewLine}";

            metadata += $"<meta name=\"twitter:description\" content=\" {tag.Description}\">{Environment.NewLine}";
            metadata += $"<meta property=\"og:description\" content=\" {tag.Description}\"{Environment.NewLine}";
            metadata += $"<meta name=\"description\" content=\"{tag.Description}\">{Environment.NewLine}";

            var keywords = string.Join(", ", tag.Description.Split(" ").Where(x => x.Length > 5).ToList());
            metadata += $"<meta name=\"keywords\" content=\"{keywords}\">{Environment.NewLine}";

            metadata += $"<meta property=\"twitter:site\" content=\"@thedreamwedds\">{Environment.NewLine}";
            metadata += $"<meta name=\"og:site_name\" content=\"Dream Wedds | Your personal wedding website\">{Environment.NewLine}";

            metadata += $"<meta name=\"og:url\" content=\"{tag.Url}\">{Environment.NewLine}";
            metadata += $"<link rel=\"canonical\" href=\"https:dreamwedds.com\">{Environment.NewLine}";
            metadata += $"<meta name=\"twitter:card\" content=\"summary_large_image\">{Environment.NewLine}";
            metadata += $"<meta property=\"og:type\" content=\"article\">{Environment.NewLine}";
            metadata += $"<meta name=\"robots\" content=\"max-image-preview:large\">{Environment.NewLine}";
            metadata += $"<meta name=\"og:locale\" content=\"en-US\">{Environment.NewLine}";
            metadata += $"<meta name=\"og:type\" content=\"website\">{Environment.NewLine}";
            metadata += $"<meta property=\"article:tag\" content=\"Elegance, Beautiful Woman, Bridal Lehanga, Dream Wedding, Culture, Wedding Website, Beautiful Wedding Images, India, Ornamented, Square Format Image, Tradition, Wedding, Wedding Dress, Online Wedding, Personal Wedding Website\">";
            return metadata;
        }
    }
}
