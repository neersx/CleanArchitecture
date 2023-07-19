using System.Threading.Tasks;

namespace BlazorHero.CleanArchitecture.Application.Interfaces.Repositories
{
    public interface IWeddingRepository
    {
        Task<bool> IsTemplateUsed(int brandId);
    }
}
