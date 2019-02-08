using System.Threading.Tasks;
using B2BApi.Models;
using B2BApi.ViewModels;

namespace B2BApi.Interfaces
{
    public interface IBrandRepository : IRepository
    {
        Task<Brand> GetBrandAsync(int brandId);
        Task<Brand> GetBrandAsync(string brandName);
    }
}