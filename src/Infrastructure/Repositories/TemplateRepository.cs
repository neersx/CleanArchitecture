using BlazorHero.CleanArchitecture.Application.Interfaces.Repositories;
using BlazorHero.CleanArchitecture.Domain.Entities.Catalog;
using BlazorHero.CleanArchitecture.Domain.Entities.DreamWedds;

namespace BlazorHero.CleanArchitecture.Infrastructure.Repositories
{
    public class TemplateRepository : ITemplateRepository
    {
        private readonly IRepositoryAsync<TemplateMaster, int> _repository;

        public TemplateRepository(IRepositoryAsync<TemplateMaster, int> repository)
        {
            _repository = repository;
        }
    }
}