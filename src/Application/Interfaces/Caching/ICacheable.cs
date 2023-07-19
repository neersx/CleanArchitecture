using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorHero.CleanArchitecture.Application.Interfaces.Caching;

public interface ICacheable
{
    string CacheKey { get => String.Empty; }
    MemoryCacheEntryOptions? Options { get; }
}
