using AutoMapper.QueryableExtensions;
using AutoMapper;
using BlazorHero.CleanArchitecture.Application.Application.Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace BlazorHero.CleanArchitecture.Application.Common.Mappings
{
    public static class MappingExtensions
    {
        public static Task<PaginatedList<TDestination>> PaginatedListAsync<TDestination>(this IQueryable<TDestination> queryable, int pageNumber, int pageSize)
            => PaginatedList<TDestination>.CreateAsync(queryable, pageNumber, pageSize);

        public static Task<PaginatedData<TDestination>> PaginatedDataAsync<TDestination>(this IQueryable<TDestination> queryable, int pageNumber, int pageSize)
                => PaginatedData<TDestination>.CreateAsync(queryable, pageNumber, pageSize);
        public static Task<List<TDestination>> ProjectToListAsync<TDestination>(this IQueryable queryable, IConfigurationProvider configuration)
                => queryable.ProjectTo<TDestination>(configuration).ToListAsync();
    }
}
