// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using BlazorHero.CleanArchitecture.Application.Interfaces.Caching;
using LazyCache;
using MediatR;
using Microsoft.Extensions.Logging;
using System.Threading;
using System.Threading.Tasks;

namespace BlazorHero.CleanArchitecture.Application.Common.Behaviours;

public class CachingBehaviour<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    where TRequest : IRequest<TResponse>, ICacheable
{
    private readonly IAppCache _cache;
    private readonly ILogger<CachingBehaviour<TRequest, TResponse>> _logger;

    public CachingBehaviour(
        IAppCache cache,
        ILogger<CachingBehaviour<TRequest, TResponse>> logger
        )
    {
        _cache = cache;
        _logger = logger;
    }
    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        _logger.LogTrace("{Name} is caching with {@Request}.", nameof(request), request);
        var response = await _cache.GetOrAddAsync(
            request.CacheKey,
            async () =>
            await next(),
            request.Options);

        return response;
    }
}

